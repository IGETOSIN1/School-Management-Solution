<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WEB_SCONET_MANAGEMENT.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title runat="server">School-MS Solutions- Schoolnigeria.com.ng</title>

         <link rel="stylesheet" href="styles/portal.css" type="text/css" />
     <link rel="stylesheet" type="text/css" media="only screen and (min-width: 601px)" href="Styles/main.css" />
    <link rel="stylesheet" type="text/css" media="only screen and (max-width: 600px)" href="Styles/device-480.css" />


    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta name="viewport" content="width=device-width, initial-scale=1.0"/>

<script type="text/javascript" src="Scripts/respond.js"></script>

</head>
<body style="background-color: midnightblue;  background-image:url('images/background1.jpg'); background-repeat: no-repeat; background-size: 100% 100%; background-position: center top; background-attachment: fixed;  background-size: cover; color: Window;">
    <form id="form1" runat="server">

        <asp:TextBox runat="server" ID="xxxx" Visible="false" ></asp:TextBox>
         <asp:TextBox runat="server" ID="xxx1" Visible="false" ></asp:TextBox>

    <marquee id="marquee-style" behavior="alternate" scrollamount="3" style=" color: window; margin-top: 80px; margin-bottom: -3px;"> *Fill in all the Field and Click Enter to Check Your Result, Staff Can also Login to Submit Result Online  ...</marquee>
   


    <p id="login-div1" style=" width: 600px; background-color: deeppink; margin-bottom: 5px; font-family: Times New Roman; margin-left:250px;">
        <asp:RadioButton runat="server" Text="Result Checker" AutoPostBack="True" 
            ID="radiobutton_resultchecker" 
            oncheckedchanged="radiobutton_resultchecker_CheckedChanged" 
            ViewStateMode="Enabled" >
        </asp:RadioButton> 
        <asp:RadioButton ID="radiobutton_stafflogin" runat="server" 
            Text="Staff Login" AutoPostBack="True" 
            oncheckedchanged="radiobutton_stafflogin_CheckedChanged" 
            ViewStateMode="Enabled">
        </asp:RadioButton>  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        <asp:CheckBox ID="Chk_mid_term" runat="server" Text="Mid-Term Result" 
            oncheckedchanged="Chk_mid_term_CheckedChanged" AutoPostBack="True" />     
        <asp:CheckBox ID="chk_terminal" runat="server" Text="Terminal Result" 
            style=" margin-left: 20px;" 
            oncheckedchanged="chk_terminal_CheckedChanged" AutoPostBack="True" 
            Checked="True"/>
        
        </p>

<div >
    <div id="login-div2" style=" background-color: Window; color: Black; border-radius: 6px; border-color: firebrick;">

        <asp:Label ID="result" runat="server" Text="" style=" text-align: center; color: deeppink; font-family: Times New Roman; font-weight: bold; font-size: 12px;"></asp:Label><br/>
  
   <!-- ############################# START OF PANEL FOR LOGIN RESULT ###############################3 -->
     <asp:panel ID="Panel_login_result" runat="server" Visible="true" >
    <!-- ############################# START OF FIRST ROW ###############################3 -->
   
     <p class="pzz1" >
    <font class="lab-title5">Name of School </font>
    <asp:DropDownList ID="school" runat="server" class="dd-title"  ForeColor="firebrick" >
        <asp:ListItem>SCONET SOLUTIONS</asp:ListItem>
         </asp:DropDownList>
    <hr class="hr1"/>
         
         <!-- ############################# END OF FIRST ROW ###############################3 -->
         <!-- ############################# START OF SECOND ROW ###############################3 -->
         <div class="divert">
             <p id="p-float-left">
                 <label style=" font-family: Segoe UI Semibold; font-size: 14px; "> Pin Number</label><br/>
                 <asp:TextBox ID="pin_number" runat="server" 
                     style=" margin-bottom: 5px; font-family: Segoe UI; font-weight: bold;" 
                     TextMode="Password" Width="234px"></asp:TextBox>
                 <br/>
                 <label style=" font-family: Segoe UI Semibold; font-size: 14px;">
                 Session</label><br/>
                 <asp:DropDownList ID="session" runat="server" Height="22" 
                     style=" font-family: Segoe UI;" Width="192px" 
                     onselectedindexchanged="session_SelectedIndexChanged">
                     <asp:ListItem>Session</asp:ListItem>
                     <asp:ListItem>2016/2017</asp:ListItem>
                     <asp:ListItem>2017/2018</asp:ListItem>
                     <asp:ListItem>2018/2019</asp:ListItem>
                     <asp:ListItem>2019/2020</asp:ListItem>
                     <asp:ListItem>2020/2021</asp:ListItem>
                 </asp:DropDownList>
             </p>
             <p id="p-float-right">
                 <font style=" font-family: Segoe UI Semibold; font-size: 14px;">
                 Student ID</font><br/>
                 <asp:TextBox ID="student_id" runat="server" 
                     style=" margin-right: 5px; margin-bottom: 5px; font-family: Segoe UI; font-weight: bold;" 
                     Width="210px"></asp:TextBox>
                 <br/>
                 <font style=" font-family: Segoe UI Semibold; font-size: 14px;">
                 Term</font><br/>
                 <asp:DropDownList ID="term" runat="server" Height="22" 
                     style=" font-family: Segoe UI;" Width="172px" 
                     onselectedindexchanged="term_SelectedIndexChanged">
                     <asp:ListItem>Term</asp:ListItem>
                     <asp:ListItem>First</asp:ListItem>
                     <asp:ListItem>Second</asp:ListItem>
                     <asp:ListItem>Third</asp:ListItem>
                 </asp:DropDownList>
                  
             </p>
         </div>
         <hr class="hr2" />
         <asp:Button ID="Button2" runat="server" Text="Reset" onclick="Button2_Click" class=" button_reset" style=" margin-left: 10px;" />
         <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Enter" class=" button_submit" />  
         <asp:Button ID="Button7" runat="server" Text="Enter" class=" button_submit" 
             Visible="False" onclick="Button7_Click1" />
         <p>
         </p>
         <p>
         </p>
         <p>
         </p>
         <p>
         </p>
         <p>
         </p>
         <p>
         </p>
         <p>
         </p>
         <p>
         </p>
   </p>

          </asp:panel>

          <!-- ############################# END OF PANEL FOR LOGIN RESULT ###############################3 -->


           <!-- ############################# START OF PANEL FOR STAFF LOGIN ###############################3 -->
     <asp:panel ID="Panel_login_staff" runat="server" Visible="false" >
    <!-- ############################# START OF FIRST ROW ###############################3 -->
    
     <p  class="pzz1" >
    <font class="lab-title5">Name of School </font>
    <asp:DropDownList ID="school_login" runat="server" class="dd-title" 
             ForeColor="firebrick" >
        <asp:ListItem>SCONET SOLUTIONS</asp:ListItem>
         </asp:DropDownList>
    <hr/>
         
         <!-- ############################# END OF FIRST ROW ###############################3 -->
         <!-- ############################# START OF SECOND ROW ###############################3 -->
         <div style=" margin-top: -28px;">
             <p class="para1">
                 <br/>
                 <font class="lab-title" style=" color: Black;" > User Name/ Login ID</font><br/>
                 <asp:TextBox ID="username" runat="server" class="txt-panel2" ></asp:TextBox>
             </p>
             <p class="lab-title1" >
                 <font style=" font-family: Segoe UI Semibold; font-size: 16px;">
                 Password</font><br/>
                 <asp:TextBox ID="password" runat="server" class="txt-password" 
                     TextMode="Password" ></asp:TextBox>
             </p>
              
         </div>
         <hr style=" margin-top: 10px;"/>
         <asp:Button ID="Button3" runat="server" Height="30px" class=" button_reset" style=" margin-left: 10px;" 
             Text="Reset" onclick="Button3_Click" />
         <asp:Button ID="Button4" runat="server" class=" button_submit" 
             Text="Enter" onclick="Button4_Click" />
         <p>
         </p>
         <p>
         </p>
         <p>
         </p>
         <p>
         </p>
         <p>
         </p>
         <p>
         </p>
         <p>
         </p>
         <p>
         </p>
         <p>
         </p>
   </p>

          </asp:panel>

          <!-- ############################# END OF PANEL FOR STAFF LOGIN ###############################3 -->


            <!-- ############################# START OF PANEL FOR STUDENT-PARENT LOGIN ###############################3 -->
     <asp:panel ID="Panel_login_student_parent" runat="server" Visible="false" >
    <!-- ############################# START OF FIRST ROW ###############################3 -->
     <p  class="pzz1" >
    <font class="lab-title5">Name of School </font>
    <asp:DropDownList ID="DropDownList1" runat="server" class="dd-title" ForeColor="firebrick" >
        <asp:ListItem>SCONET SOLUTIONS</asp:ListItem>
         </asp:DropDownList>
    <hr/>
         
         <!-- ############################# END OF FIRST ROW ###############################3 -->
         <!-- ############################# START OF SECOND ROW ###############################3 -->
         <div style=" margin-top: -28px;">
             <p class="para1">
                 <br/>
                 <font class="lab-title" style=" color: Black;" > Student ID</font><br/>
                 <asp:TextBox ID="student_login_id" runat="server" class="txt-panel2" ></asp:TextBox>
             </p>
             <p class="lab-title1" >
                 <font style=" font-family: Segoe UI Semibold; font-size: 16px;">
                 Password</font><br/>
                 <asp:TextBox ID="student_login_password" runat="server" class="txt-password" 
                     TextMode="Password" ></asp:TextBox>
             </p>
         </div>
         <hr style=" margin-top: 10px;"/>
         <asp:Button ID="Button5" runat="server" Height="30px" class=" button_reset" 
             Text="Reset" onclick="Button5_Click" style=" margin-left: 200px;" />
         <asp:Button ID="Button6" runat="server" class=" button_submit" 
             Text="Enter" onclick="Button6_Click" />
         <p>
         </p>
         <p>
         </p>
         <p>
         </p>
         <p>
         </p>
         <p>
         </p>
         <p>
         </p>
         <p>
         </p>
         <p>
         </p>
         <p>
         </p>
         <p>
         </p>
   </p>

          </asp:panel>

          <!-- ############################# END OF PANEL FOR STUDENT-TEACHERS LOGIN ###############################3 -->



</div>
    </div>



    </form>
    <br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/>
    <div style=" background-color: firebrick; text-align: center; color: Window; margin-bottom: 0px; font-size: 13px;"> &copy; <% Response.Write(DateTime.Now.Year.ToString()); %>.Schoolnigeria.com.ng. All Right Reserve ...</div>

    <!--#############################################################################################-->
<iframe src="/set_grade_remark.aspx" style="width:50%;height:50%; visibility:hidden;"></iframe>
<!--#############################################################################################-->



</body>
</html>
