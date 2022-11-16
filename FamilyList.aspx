<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FamilyList.aspx.cs" Inherits="FamilyList" %>

<!DOCTYPE>

<html>
<head runat="server">
    <title></title>
    <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }
        table
        {
            border:1px solid #ccc;
            border-collapse:collapse;
        }
        table th
        {
            background-color: #F7F7F7;
            color: #333;
            font-weight: bold;
        }
        table th, table td
        {
            padding: 5px;
            border-color: #ccc;
        }
    </style>
</head>
<body>

    <h1><center>Lista de Familiares</center></h1>
    
    <form id="form1" runat="server">
    <center><asp:PlaceHolder  ID = "PlaceHolder1" runat="server" /> </center> 
    </form>
    <br/>
    <div class="d-flex justify-content-center mt-3 login_container">
        <center><button type="submit" name="button" class="btn login_btn">Shuffle</button></center>
    </div>

</body>
</html>