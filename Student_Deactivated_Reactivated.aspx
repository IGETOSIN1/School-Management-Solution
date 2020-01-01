<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Student_Deactivated_Reactivated.aspx.cs" Inherits="WEB_SCONET_MANAGEMENT.Student_Deactivated_Reactivated" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Label ID="result_output" runat="server" class="result"></asp:Label><br/><br/>

    <asp:textbox ID="school" runat="server" ForeColor="Black" 
        Visible="False"></asp:textbox><asp:textbox ID="school_query" runat="server" 
        ForeColor="Black" Visible="False"></asp:textbox><asp:textbox ID="student_id" runat="server" 
        ForeColor="Black" Visible="False"></asp:textbox>

    <asp:Label ID="Label5" runat="server" Text="List of Current Students" class="lab"></asp:Label><br/>
                 <asp:DropDownList ID="student_activated" runat="server" AutoPostBack="True" class="ddp_long" style=" width: 250px; height: 22px; margin-left: 6px; margin-right: 6px;" ForeColor="Firebrick" > </asp:DropDownList>    
                  <asp:Button ID="Button3" runat="server" Text="Deactivate" style=" margin-top: 0px; color: Window; background-color: firebrick; font-family: Times New Roman; font-size:13px;" Height="28px" Width="120px" OnClick="Button3_Click" />
    <br/><br/>             
     <asp:Label ID="Label1" runat="server" Text="List of Old Students" class="lab"></asp:Label><br/>
                 <asp:DropDownList ID="student_deactivated" runat="server" AutoPostBack="True" class="ddp_long" style=" width: 250px; height: 22px; margin-left: 6px; margin-right: 6px;" ForeColor="Firebrick" > </asp:DropDownList>    
                  <asp:Button ID="Button1" runat="server" Text="Activate" style=" margin-top: 0px; color: Window; background-color: firebrick; font-family: Times New Roman; font-size:13px;" Height="28px" Width="120px" OnClick="Button1_Click" />
                  

               
                  


</asp:Content>
