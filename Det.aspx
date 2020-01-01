<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Det.aspx.cs" Inherits="WEB_SCONET_MANAGEMENT.Det" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>School Management Solution (SCONET)-MS Internal Result Checker</title>

     <style type="text/css">
         
   .menu
   {
     background-color:DarkSlateGray; color: palegreen; height: 59px; width: 152px; color: White; border-radius: 2px; border: 2px solid firebrick; font-family: Times New Roman; font-weight: bold; font-size: 14px; margin: 5px;
   }
   .divider
   {
    width: 45px; height: auto; display:inline;   
   }
.lab
{
    font-family: Times New Roman; font-size: 15px;
}
.txt_short
{
   width: 100px; height: 19px; border-radius: 2px; color: Black; text-align: right; font-family: Verdana; font-size: 12px; margin-bottom: 4px;  
}
.txt_long
{
   width: 250px; height: 21px; border-radius: 2px; color: Black; text-align: center; font-family: Verdana; font-size: 12px; margin-bottom: 17px;  
}
.ddp_long
{
   width: 220px; height: 25px; color: Black; text-align: center; font-family: Verdana; font-size: 12px; margin-bottom: 17px;  
}
.ddp_vlong
{
   width: 350px; height: 25px; color: Black; text-align: center; font-family: Verdana; font-size: 12px; margin-bottom: 17px;  
}

.btn_save
{
    background-color: firebrick; color: Window; border: 2px solid mediumblue; font-family: Segoe UI; font-size: 12px; float: right; margin-right: 20px; height: 36px; width: 131px; margin-top: 12px; 
}
.linep
{
 color: Window; width: 1000px;  font-family: Verdana; font-weight: bolder;  margin-top: -58px; 
 }
 .result
 {
 margin-left: 100px; color: firebrick; font-family: verdana; font-size: 13px;
 }
 .rado
 {
  margin-right: 27px;    
 }
 .lb
 {
 font-family: Times New Roman; font-size: 17px; margin-top: 0px; margin-bottom: 0px; color: purple; text-transform: uppercase;        font-family: Comic Sans MS; font-size: 15px; margin-top: 0px; margin-bottom: 0px; color: purple; font-weight: bolder; text-transform: capitalize;
 }
  .lb1
 {
 font-family: Times New Roman; font-size: 17px; margin-top: 0px; margin-bottom: 0px; color: purple; text-transform: uppercase;border-bottom: 2px solid black;      font-family: Comic Sans MS; font-size: 15px; margin-top: 0px; margin-bottom: 0px; color: purple; font-weight: bolder; text-transform: capitalize; 
 }
    </style>

    <style media="print" type ="text/css">
    background-color: purple !important;
    -webkit-print-color-adjust: exact;
    </style>

</head>
<body>
    <form id="form1" runat="server">
    <div style=" float: none; margin-top: 10%; margin-left: 25%;">

        <asp:textbox ID="schl" runat="server" 
        ForeColor="Black" Visible="False"></asp:textbox>
        <asp:TextBox ID="schl1" runat="server" ForeColor="Black" 
        Visible="False"></asp:TextBox>
    <asp:TextBox ID="cat" runat="server" ForeColor="Black" 
        Visible="False"></asp:TextBox>
            <asp:TextBox ID="txt_det" runat="server" ForeColor="Black" 
        Visible="False"></asp:TextBox>
    
        <asp:Label ID="Label5" runat="server" Text="Select School to Navigate" class="lab"></asp:Label><br/>
                 <asp:DropDownList ID="school_query" runat="server" class="ddp_vlong" style=" width: 250px; height: 22px; margin-left: 6px; margin-right: 6px;" ForeColor="Firebrick" OnSelectedIndexChanged="school_query_SelectedIndexChanged" AutoPostBack="true" > </asp:DropDownList>  
                 
                  <asp:Button ID="Button3" runat="server" Text="Continue" style=" margin-top: 0px; color: Window; background-color: firebrick; font-family: Times New Roman; font-size:13px;"  Height="28px" Width="120px" OnClick="Button3_Click" />

    </div>
    </form>
</body>
</html>
