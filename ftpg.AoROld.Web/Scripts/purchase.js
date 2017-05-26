var addMRCheckBox = true;

function updateAllCost() {
    Purchase.Cash = Purchase.OriginalCash;
    var modifiedAmount = document.getElementById("tbModifyTokenPurchase");
    if (modifiedAmount != null) {
        var plannedTokenPurchase = parseInt(modifiedAmount.value);
        if (plannedTokenPurchase > 0) {
            plannedTokenPurchase *= -1;
            modifiedAmount.value = plannedTokenPurchase;
        }
        Purchase.Cash += plannedTokenPurchase;
    }
    var modifiedAmount2 = document.getElementById("tbModifyOther");
    if (modifiedAmount2 != null) {
        var otherAmount = (modifiedAmount2.value === "") ? 0 : modifiedAmount2.value;
        Purchase.Cash += parseInt(otherAmount);
    }
    resetAdvanceCost();
    updateLeaders(); // deducts cash for leader patronage
    applyInstitutionalResearch(); 
    updateAdvances(); // deducts cash for purchase advances
    updateMiseryForAdvances();
    updateStabilizationPenalty();
    applyShipUgrade();
    updateTierLevel();
    updateCashFullAmount();
    refreshMisery();
    refreshMiseryRelief();
    disableNotAffordable();
}

// *** Tier level Misery Relief
function updateTierLevel() {
    var tierLevel = getTierLevel();
    if (tierLevel < Purchase.TierLevel) {
        Purchase.TierLevel = 0;
        Purchase.MiseryReliefSpent = 0;
        var tiers = document.getElementById("tiers");
        while (tiers.hasChildNodes()) {
            tiers.removeChild(tiers.lastChild);
        }
        addMRCheckBox = true;
    }

    Purchase.MiseryReliefSpent = 0;

    for (var i = 0; i < Purchase.TierLevel; i++) {
        var checkBox = document.getElementById("cbTier" + (i + 1));
        if (checkBox !== null && checkBox !== undefined) {
            if (checkBox.checked) {
                if (!checkBox.disabled) {
                    checkBox.disabled = true;
                }
                var mrRelief = parseInt(checkBox.value);
                var miseryReduce = getMiseryDecreaseByOne(Purchase.Misery);
                changeMisery(false);
                Purchase.Cash -= miseryReduce - mrRelief;
                Purchase.MiseryReliefSpent += mrRelief;
            }
        }
    }




    while (Purchase.TierLevel < tierLevel) {
        var reduceMiseryCost = getMiseryDecreaseByOne(Purchase.Misery);
        var cashCost = reduceMiseryCost;
        if (Purchase.MiseryReliefSpent < Purchase.MiseryRelief) {
            if (Purchase.MiseryRelief - Purchase.MiseryReliefSpent >= reduceMiseryCost) {
                Purchase.MiseryReliefSpent += reduceMiseryCost;
                cashCost = 0;
            } else {
                cashCost = reduceMiseryCost - Purchase.MiseryRelief - Purchase.MiseryReliefSpent;
                //Purchase.MiseryReliefSpent = Purchase.MiseryRelief;
            }
        }
        addTierHTML(tierLevel, cashCost, reduceMiseryCost - cashCost);
        addMRCheckBox = false;
        Purchase.TierLevel++;
    }
}

function tierClick() {
    // "\u00A0\u00A0\u00A0\u00A0More tiers available"
    
    var spanTier = document.getElementById("tiers");
    var count = 0;
    var node = spanTier.childNodes[spanTier.childNodes.length - 1];
    if (node.nodeName === "#text") {
        spanTier.removeChild(spanTier.childNodes[spanTier.childNodes.length - 1]);
        Purchase.TierLevel--;
        addMRCheckBox = true;
    }
    updateAllCost();
}

function addTierHTML(tierlevel, cost, miseryRelief) {
    var span = document.getElementById("tiers");
    if (!addMRCheckBox) {
        span.appendChild(document.createTextNode("\u00A0\u00A0\u00A0\u00A0More tiers available"));
        return;
    }
    var checkbox = document.createElement("input");
    checkbox.type = "checkbox";
    checkbox.checked = false;
    checkbox.id = "cbTier" + tierlevel;
    checkbox.title = "Relief for $" + cost;
    checkbox.value = miseryRelief;
    if (cost === 0) {
        checkbox.checked = true;
        checkbox.disabled = true;
    } else {
        checkbox.disabled = Purchase.Cash < cost;
        checkbox.addEventListener("click", function () {tierClick()});
    }
    span.appendChild(document.createTextNode("\u00A0\u00A0\u00A0\u00A0Tier " + tierlevel + " Misery Relief. Use (" + miseryRelief + "MR) and $" + cost));
    span.appendChild(checkbox);
    
}

function getTierLevel() {
    var array = new Array("Science", "Religion", "Commerce", "Communic", "Exploration", "Civics");
    var count = 4;
    for (var i = 0; i < array.length; i++) {
        var group = getGroupTierLevel(array[i]);
        count = (group < count) ? group : count;
    }
    return count;
}

function getGroupTierLevel(group) {
    var count = 0;
    for (var i = 0; i < Purchase.Advances.length; i++) {
        var advance = Purchase.Advances[i];
        if (advance.Group === group && (advance.PreOwned || advance.Purchased)) {
            count ++;
        }
    }
    return count;
}

// *** ShipUgrade
function shipUgradeChanged(checked) {
    Purchase.ShipUpgrade = checked;
    updateAllCost();
}
function applyShipUgrade() {
    if (shipAdvancePurchasedThisTurn()) {
        Purchase.ShipUpgrade = false;
    }
    if (Purchase.ShipUpgrade) {
        Purchase.Cash -= 10;
    }
    refreshShipUgrade();
}

function shipAdvancePurchasedThisTurn() {
    for (var i = 0; i < Purchase.Advances.length; i++) {
        var advance = Purchase.Advances[i];
        if ((advance.Letter === "S" || advance.Letter === "T" || advance.Letter === "U") && advance.Purchased) {
            return true;
        }
    }
    return false;
}

// *** Stabilization ***
function updateStabilizationPenalty() {
    miseryIncrease = getStabilizationMiseryPenalty();
    var stabilizationPaid = !document.getElementById("cbStabilization").checked;
    if (stabilizationPaid) {
        Purchase.Cash -= Purchase.StabilzationCost;
    } else {
        changeMisery(true);
    }
    refreshStabilizationMiseryPenalty(miseryIncrease);
}

function stabilizationChanged(checked) {
    Purchase.Stabilization = !checked;
    updateAllCost();
}

// *** Misery ***
function updateMiseryForAdvances() {
    Purchase.Misery = Purchase.OriginalMisery;
    for (var i = 0; i < Purchase.Advances.length; i++) {
        var advance = Purchase.Advances[i];
        if (advance.MiseryChange !== 0 && advance.Purchased) {
            changeMisery(advance.MiseryChange > 0);
        }
    }
    Purchase.Misery = (Purchase.Misery < 0) ? 0 : Purchase.Misery;
}

function changeMisery(add) {
    if (add) {
        Purchase.Misery += getMiseryIncreaseByOne(Purchase.Misery);
    } else {
        Purchase.Misery -= getMiseryDecreaseByOne(Purchase.Misery);
    }
}

function getMiseryIncreaseByOne (currentMisery){
    if (currentMisery > 450) {
        return 100;
    }
    if (currentMisery > 175) {
        return 50;
    }
    if (currentMisery > 90) {
        return 25;
    } 
    return 10;
}

function getMiseryDecreaseByOne(currentMisery) {
    if (currentMisery < 125) {
        return 10;
    }
    if (currentMisery < 250) {
        return 25;
    }
    if (currentMisery < 600) {
        return 50;
    }
    return 100;
}
function getStabilizationMiseryPenalty()
{
    var misery = Purchase.Misery;
    while (misery < Purchase.Misery + Purchase.StabilzationCost)
    {
        misery += getMiseryIncreaseByOne(misery);
    }
    return misery - Purchase.Misery;
}

// *** Leaders *** 
function leaderChanged(name) {
    var leader = getLeaderByName(name);
    leader.Used = !leader.Used;
    updateAllCost();
}

function updateLeaders() {
    var ownsPatronage = hasAdvance("E");
    var leaderUse = 0;
    for (var i = 0; i < Purchase.CardPlays.length; i++) {
        var leader = Purchase.CardPlays[i];
        if (leader.Capital !== Player.Capital && !leader.Protected) {
            leader.Used = (!ownsPatronage) ? false : leader.Used;
            if (leader.Used) {
                leaderUse += leader.Fee;
                applyLeaderDiscounts(leader);
            }
            updateLeaderCheckBox(leader.Name, leader.Used, Purchase.Cash < leader.Fee || !ownsPatronage);
        }
        else if (leader.Capital === Player.Capital) {
            applyLeaderDiscounts(leader);
        }
    }
    Purchase.Cash -= leaderUse;
    refreshLeaderUse(leaderUse);
}

function applyLeaderDiscounts(leader) {
    for (var i = 0; i < leader.AdvanceLetters.length; i++) {
        var advanceLetter = leader.AdvanceLetters[i];
        var advance = getAdvanceByLetter(advanceLetter);
        if (advanceLetter !== "E" && (leader.Capital !== Player.Capital)) // A player cannot gain patronage and at the same time get Patronage credit in the same round 
        {                                                                  // unless he is card owner. Own leaders credit is already computed in the original advance cost
            advance.Cost -= leader.Discount;
        }
    }
}

// *** Advances ***
function resetAdvanceCost() {
    for (var i = 0; i < Purchase.Advances.length; i++) {
        var advance = Purchase.Advances[i];
        advance.Cost = advance.OriginalCost;
    }
}
function updateAdvances() {
    var advancesCost = 0;
    Purchase.MiseryRelief = 0;
    for (var i = 0; i < Purchase.Advances.length; i++) {
        var advance = Purchase.Advances[i];
        // only process advances not Preowned
        if (!advance.PreOwned) {
            var checkBox = document.getElementById("cb" + advance.Letter);
            var disabled = isAdvanceRestricted(advance);
            if (!disabled) {
                if (!advance.Purchased) {
                    disabled = advance.Cost > Purchase.Cash;
                }
            } else {
                restrictDependentAdvances(advance.Letter);
            }
            checkBox.disabled = disabled;
            advance.Cost = (advance.Cost < 0) ? 0 : advance.Cost;
            updateAdvanceCost(advance);
            if (advance.Purchased) {
                advancesCost += advance.Cost;
                Purchase.MiseryRelief += advance.MiseryRelief;
            }
        }
    }
    Purchase.Cash -= advancesCost;

    refreshAdvancesUse(advancesCost);
}

function disableNotAffordable() {
    for (var j = 0; j < Purchase.Advances.length; j++) {
        var adv = Purchase.Advances[j];
        // only process advances not owned
        if (!adv.PreOwned && !adv.Purchased) {
            if (adv.Cost > Purchase.Cash) {
                document.getElementById("cb" + adv.Letter).disabled = true;
            }
        }
    }

    for (var i = 0; i < Purchase.CardPlays.length; i++) {
        var leader = Purchase.CardPlays[i];
        if (leader.Capital !== Player.Capital && !leader.Protected && !leader.Used) {
            refresfLeaderCheckbox(leader);
        }
    }


}

function restrictDependentAdvances(letter) {
    for (var i = 0; i < Purchase.Advances.length; i++) {
        var advance = Purchase.Advances[i];
        if (advance.Letter.indexOf(letter) > -1) {
            if (advance.Purchased) {
                advance.isAdvanceRestricted = true;
                advance.Purchased = false;
            }
            updateAdvanceCheckBox(advance.Letter, false, true);
        }
    }
}
function isAdvanceRestricted(advance) {
    if (advance.Requisite === "") {
        return false;
    }
    var letter = advance.Requisite.substr(0, 1);
    if (!hasAdvance(letter)) {
        return true;
    }
    if (advance.Requisite.length === 1) {
        return false;
    }
    letter = advance.Requisite.substr(2, 1);
    if (!hasAdvance(letter)) {
        return true;
    }
    return false;
}

function clickAdvance(letter) {
    // 1. Deduct cost from cash
    var advance = getAdvanceByLetter(letter);
    advance.Purchased = true;
    if (!document.getElementById("cb" + letter).checked) {
        advance.Purchased = false;
    }

    if (advance.Letter === "E") {
        updateLeaders();
    }
    if (advance.Letter === "X") {
        applyInstitutionalResearch();
    }
    updateAllCost();
}

function hasAdvance(letter) {
    for (var i = 0; i < Purchase.Advances.length; i++) {
        if (Purchase.Advances[i].Letter === letter && (Purchase.Advances[i].Purchased || Purchase.Advances[i].PreOwned)) {
            return true;
        }
    }
    return false;
}

function applyInstitutionalResearch() {
    if (!hasAdvance("X")) {
        return;
    }
    for (var i = 0; i < Purchase.Advances.length; i++) {
        var advance = Purchase.Advances[i];
        if (advance.Group !== "Religion" && advance.Group !== "Civics") {
            advance.Cost -= 10;
        }
    }
}

// Get functions
function getAdvanceByLetter(letter) {
    for (var i = 0; i < Purchase.Advances.length; i++) {
        if (Purchase.Advances[i].Letter === letter) {
            return Purchase.Advances[i];
        }
    }
    return null;
}

function getLeaderByName(name) {
    for (var i = 0; i < Purchase.CardPlays.length; i++) {
        var leader = Purchase.CardPlays[i];
        if (name === leader.Name) {
            return leader;
        }
    }
    return null;
}

// Page update functions
function updateCash(amount) {
    Purchase.Cash -= amount;
    updateCashFullAmount();
}

function updateCashFullAmount() {
    document.getElementById("cash").innerHTML = Purchase.Cash;
}

function updateAdvanceCheckBox(letter, isChecked, isDisabled) {
    var checkBox = document.getElementById("cb" + letter);
    checkBox.checked = isChecked;
    checkBox.disabled = isDisabled;
}

function updateAdvanceCost(advance) {
    var divCost = document.getElementById("cost" + advance.Letter);
    divCost.innerHTML = advance.Cost;
}

function refreshMisery() {
    var divMisery = document.getElementById("misery");
    divMisery.innerHTML = Purchase.Misery;
}

function refreshStabilizationMiseryPenalty(miPenalty) {
    document.getElementById("spanStabilizationCost").innerHTML = Purchase.StabilzationCost;
    document.getElementById("divStabilizationCost").innerHTML = (Purchase.Stabilization) ? Purchase.StabilzationCost : 0;
    document.getElementById("spanStabilizationMiseryPenalty").innerHTML = miPenalty;
}

function updateLeaderCheckBox(leader, checked, disabled) {
    var checkBox = document.getElementById("cb" + leader);
    checkBox.disabled = disabled;
    checkBox.checked = checked;
}

function refreshLeaderUse(amount) {
    document.getElementById("divLeaderUse").innerHTML = amount;
}

function refresfLeaderCheckbox(leader) {
    document.getElementById("cb" + leader.Name).disabled = Purchase.Cash < leader.Fee;
}

function refreshAdvancesUse(amount) {
    document.getElementById("divAdvancesPurchased").innerHTML = amount;
}

function refreshShipUgrade() {
    document.getElementById("cbShipUgrade").disabled = shipAdvancePurchasedThisTurn() || Purchase.Cash < 10;
    document.getElementById("cbShipUgrade").checked = (document.getElementById("cbShipUgrade").disabled) ? false : document.getElementById("cbShipUgrade").checked;
    document.getElementById("divShipUpgradeCost").innerHTML = (Purchase.ShipUpgrade) ? 10 : 0;
}

function refreshMiseryRelief() {
    document.getElementById("spanFreeMiseryRelief").innerHTML = "(" + Purchase.MiseryRelief + ") " + (Purchase.MiseryRelief - Purchase.MiseryReliefSpent);
}