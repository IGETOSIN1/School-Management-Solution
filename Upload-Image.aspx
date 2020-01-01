<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Upload-Image.aspx.cs" Inherits="WEB_SCONET_MANAGEMENT.Upload_Image" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


<!-- <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
    <progressTemplate>
    <img src="images/ajax1.gif" alt="Loader" />
    </progressTemplate>
    </asp:UpdateProgress>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <contentTemplate>-->
    <asp:Label ID="result_output" runat="server" class="result"></asp:Label><br/><br/>
    <asp:TextBox ID="school" runat="server" ForeColor="Black" 
    AutoPostBack="True" Visible="False"></asp:TextBox>
    <asp:TextBox ID="department" runat="server" AutoPostBack="True" 
        ForeColor="Black" Width="85px" Visible="False"></asp:TextBox>
    <asp:TextBox ID="users" runat="server" ForeColor="Black" Visible="False"></asp:TextBox>
    <asp:TextBox ID="user_name" runat="server" ForeColor="Black" Visible="False"></asp:TextBox>
    <asp:TextBox ID="display_subject" runat="server" ForeColor="Black" 
        Visible="False"></asp:TextBox> 
    <asp:TextBox ID="display_class" runat="server" ForeColor="Black" 
        Visible="False"></asp:TextBox>
    <asp:TextBox ID="display_student" runat="server" ForeColor="Black" 
        Visible="False"></asp:TextBox>
    <asp:TextBox ID="class_teacher" runat="server" ForeColor="Black" Width="129px" 
        Visible="False"></asp:TextBox>
    <asp:TextBox ID="student_id_query" runat="server" ForeColor="Black" 
        Width="129px" Visible="False"></asp:TextBox>
        <asp:TextBox ID="TextBox1" runat="server" ForeColor="Black" Visible="False" 
        Width="129px"></asp:TextBox>
        
        <div>
        <!-- START OF HEADERS -->

          <asp:Label ID="Label2" runat="server" Text="Class" class="lab" style=" margin-left: 6px;"></asp:Label>
                 <asp:DropDownList ID="CLASS" runat="server" 
                AutoPostBack="True" class="ddp_long"
                
                style=" width: 190px; height: 21px; margin-bottom: 8px;  margin-left: 6px; margin-right: 6px;" 
                ForeColor="Firebrick" 
                onselectedindexchanged="CLASS_SelectedIndexChanged1" > </asp:DropDownList>

                <asp:Label ID="Label5" runat="server" Text="Students' Name" class="lab"></asp:Label>
                 <asp:DropDownList ID="student_name_query" runat="server" class="ddp_long"
                style=" width: 250px; height: 21px; margin-left: 6px; margin-right: 6px;" 
                ForeColor="Firebrick" 
                onselectedindexchanged="student_name_query_SelectedIndexChanged" > </asp:DropDownList>
             
             <asp:TextBox ID="student_id" runat="server" class="txt_long" 
                ForeColor="Firebrick"
                style=" text-align: right; margin-left: 6px;" Width="196px" 
                Visible="False"></asp:TextBox>
                
                <hr style=" margin-top: 2px; margin-bottom: 3px;"/>
                <br/>

                 

                <asp:FileUpload ID="FileUpload1" runat="server" class="txt_long" Width="217px" style=" margin-left:6px; color: firebrick;"/> 

                <asp:Button ID="Button2" runat="server" Text=" Upload Student's Image " style=" margin-left: 5px; color: Window; background-color: deeppink; font-family: Times New Roman; font-size: 13px; height: 26px;" 
                onclick="Button2_Click" /><br/><br/>


                <asp:FileUpload ID="FileUpload2" runat="server" class="txt_long" Width="217px" style=" margin-left:6px; color: firebrick;"/> 

                <asp:Button ID="Button3" runat="server" Text=" Upload School Logo " 
                style=" margin-left: 5px; color: Window; background-color: deeppink; font-family: Times New Roman; font-size: 13px; height: 26px;" 
                onclick="Button3_Click" /><br/><br/>


                <asp:FileUpload ID="FileUpload3" runat="server" class="txt_long" Width="217px" style=" margin-left:6px; color: firebrick;"/> 

                <asp:Button ID="Button4" runat="server" Text=" Upload Signature " 
                style=" margin-left: 5px; color: Window; background-color: deeppink; font-family: Times New Roman; font-size: 13px; height: 26px; " 
                onclick="Button4_Click" />

                <br/><br/>

                 <hr style=" color: Window; width:100%;  font-family: Verdana; font-weight: bolder; "/> 
<br/>
              <asp:FileUpload ID="FileUpload4" runat="server" class="txt_long" Width="217px" style=" margin-left:6px; color: firebrick;"/> 

                <asp:Button ID="Button5" runat="server" Text=" Upload Login Image " 
                
                style=" margin-left: 5px; color: Window; background-color: deeppink; font-family: Times New Roman; font-size: 13px; height: 26px; " 
                onclick="Button5_Click" /><br/><br/>

                <asp:FileUpload ID="FileUpload5" runat="server" class="txt_long" Width="217px" style=" margin-left:6px; color: firebrick;"/> 

                <asp:Button ID="Button6" runat="server" Text=" Upload Portal Image " 
                
                style=" margin-left: 5px; color: Window; background-color: deeppink; font-family: Times New Roman; font-size: 13px; height: 26px; " 
                onclick="Button6_Click" /><br/>



            <asp:Panel ID="panel3" runat="server"> 


     
         <hr style=" color: Window; width:100%;  font-family: Verdana; font-weight: bolder; "/> 
       

       <br/>

                <br/><br/>


   <asp:Button ID="Button1" runat="server" Text="Clear" class="btn_save" 
                    style=" float: left; margin-left: 7px;" onclick="Button1_Click" 
                    Visible="False" />  
                    

            </asp:Panel>

            

        <br/><br/><br/><br/><br/>
             <br/><br/><br/><br/><br/>
            <br/><br/><br/><br/>
        </div>      

        <!-- </contentTemplate>
    </asp:UpdatePanel> -->



</asp:Content>
