<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ftpg.AoR.Web.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AoR Login</title>
</head>
<body>
    <form id="form1" runat="server">
        Ver 2
       <h2>Play Users</h2>
       <a href="Login.aspx?id=player1">Player1</a><br />
       <a href="Login.aspx?id=player2">Player2</a><br />
       <a href="Login.aspx?id=player3">Player3</a><br />
       <a href="Login.aspx?id=player4">Player4</a><br />
       <a href="Login.aspx?id=player5">Player5</a><br />
       <a href="Login.aspx?id=player6">Player6</a>
        
        <p><a href="/Game/Create.aspx">Create game</a></p>
    </form>
</body>
</html>
