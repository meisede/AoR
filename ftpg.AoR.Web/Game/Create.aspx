 <%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="ftpg.AoR.Web.Game.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Create Game</title>
</head>
<body>
    <form id="frmGame" runat="server">
        <h2>Create Game</h2>

        Game type: 
        <select name="selGameType">
            <option value="Training">Training</option>
            <option value="Casual">Casual</option>
            <option value="Competetive">Competetive</option>
        </select>
        <br/>Training game: May play 1, more, or all nations
        <br/>Casual game: Friendly, possibly slow games
        <br/>Competetive game: Fast game, players should reply fast when their turn.
        <br/>
        <br/>
        Number of players 
        <select name="selNoPlayers" >
            <option>3</option>
            <option>4</option>
            <option>5</option>
            <option selected="selected">6</option>

        </select>
        <br/><br/>
        Game name <input type="text" name="tbName"/>
        <br/><br/>
        <input type="submit" value="Create game"/>
    </form>
</body>
</html>
