﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report-Sheet.aspx.cs" Inherits="WEB_SCONET_MANAGEMENT.Report_Sheet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

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
<body style=" background-color: midnightblue;">

    <form id="form1" runat="server" >

    <asp:Panel ID="Panel_block" runat="server" style=" color: deeppink; font-family: Times New Roman; font-size: 22px; margin-top: 200px; margin-left: 50px; font-weight: bold;" visible="false">
     <asp:Label ID="Label1" runat="server" Text=" This result cannot be displayed presently. Please, contact the school to regularise your ward's account/ any amount owing. Thank you ..." ></asp:Label>
   </asp:Panel>


    <asp:Panel ID="Panel_result" runat="server">
       

   
    <asp:Button ID="Button1" runat="server" 
           Text=" Print " Width="92px" Height="27" 
           style=" background-color: blueviolet; color: Window; margin-left: 500px;" 
           onclientclick="printDiv('printablediv')" onclick="Button1_Click1" /> <asp:Button ID="Button2" 
           runat="server" Text=" Log Out" 
           style=" margin-left: 6px; background-color: crimson; color: Window;" Width="92px" 
           Height="27px" onclick="Button2_Click"/>

    <asp:textbox ID="student_id_query" runat="server" ForeColor="Black" 
        Visible="False"></asp:textbox><asp:textbox ID="school_query" runat="server" 
        ForeColor="Black" Visible="False"></asp:textbox>
          <asp:textbox ID="det" runat="server" ForeColor="Black" 
        Visible="False"></asp:textbox><asp:textbox ID="Textbox2" runat="server" 
        ForeColor="Black" Visible="False"></asp:textbox>
    <asp:textbox ID="session_query" runat="server" ForeColor="Black" 
        Visible="False"></asp:textbox>
        <asp:textbox ID="term_query" runat="server" 
        ForeColor="Black" Visible="False" ontextchanged="term_query_TextChanged"></asp:textbox>
        <asp:textbox ID="school_campus_query" runat="server" 
        ForeColor="Black" Visible="False"></asp:textbox>
        <asp:textbox ID="mid_terminal" runat="server" 
        ForeColor="Black" Visible="False"></asp:textbox>
        
        <asp:textbox ID="first_con" runat="server" ForeColor="Black" Visible="False"></asp:textbox>
        <asp:textbox ID="second_con" runat="server" ForeColor="Black" Visible="False"></asp:textbox>
        <asp:textbox ID="exam_con" runat="server" ForeColor="Black" Visible="False"></asp:textbox>
        <asp:textbox ID="total_con" runat="server" ForeColor="Black" Visible="False"></asp:textbox>
        <asp:textbox ID="mid_total_con" runat="server" ForeColor="Black" 
            Visible="False"></asp:textbox>
            <asp:textbox ID="txt_open" runat="server" ForeColor="Black" 
            Visible="False" ></asp:textbox>
        <asp:textbox ID="txt_present" runat="server" ForeColor="Black" Visible="False" ></asp:textbox>
        <asp:textbox ID="txt_com_det" runat="server" ForeColor="Black" Visible="False"></asp:textbox>
        <asp:textbox ID="student_of_name1" runat="server" ForeColor="Black" 
            Visible="False"></asp:textbox>
            <asp:textbox ID="rest_status" runat="server" ForeColor="Black" 
            Visible="False" ></asp:textbox>
        <br/><br/>


    <div style=" margin-left: auto; margin-right: auto; border: 3px solid purple; border-radius: 3px; background-color: Window;" id="printablediv">
    
    
    <div style=" border: 1px solid window; height: 120px; width: 850px; background-color: ;">
    <p style=" float: left; margin-top: 1px;  background-color: orange;">
   <asp:Image ID="Image_logo" runat="server" style=" width: 140px; margin-left: 1px; border: 2px solid deeppink; height: 115px; margin-left: 1px; background-color: ;" />
   <!-- <img src="images/GBGS_LOGO.PNG" style=" width: 110px; height: 115px; margin-left: -25px; background-color: ;" alt="God's Blessing Group of Schools" />-->
    </p>

     <p style=" float: right; margin-top: -7px; margin-right: 45px; background-color: ;">
        <asp:Label ID="school" runat="server" Text="SCONET MANAGEMENT SOLUTION" 
             style=" font-family: Cursive; font-weight: bold; font-size: 30px; color: Purple;  margin-bottom: 145px; "></asp:Label><br/>
    <!-- <asp:Label ID="label_school_to_display" runat="server" Text="COLLEGE" style=" font-family: Comic Sans MS; font-size: 37px;  margin-top: -18px; font-style: italic; color: firebrick; margin-left: 210px;"> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </asp:Label><br/>-->
    <asp:Label ID="address" runat="server" Text="Label" style=" font-family: Cursive; font-size: 17px;  color: orangered; "></asp:Label><br/>
         <asp:Label ID="email_website" runat="server" Text="Label" style=" font-family: Cursive; font-size: 17px;  color: blueviolet; "></asp:Label><br/>
   <!-- <label >EMAIL: sunshineinternational@yahoo.com || WEBSITE: www.sunshine.com.ng</label><br/>-->
    <asp:Label ID="phone" runat="server" Text="Label" style=" font-family: Cursive; font-size: 17px; color: orangered; "></asp:Label>
  
      </p>
        

    <!--<p style=" border-left: 2px solid orangered; height: 100%; margin-left: 100px; border-left-style: dashed; margin-top: 0px;">
        </p>-->

    </div>
    <hr style=" width: 100%; border: 2px solid black; float: left; margin-top: -13px; margin-bottom: -35px;"/>
      <!--<asp:Label ID="student_name" runat="server" Text="Student's Name"></asp:Label> for  
        <asp:Label ID="term" runat="server" Text="Term"></asp:Label>  in 
        <asp:Label ID="session" runat="server" Text="Session"></asp:Label> Academic Session  -->




        <p style=" float: left; margin-left: 0px; margin-top: -2px;">
        <label class="lb">REPORT SHEET FOR: </label> 
        <asp:Label ID="term_d" runat="server" 
                Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Term &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" 
                class="lb1" ></asp:Label><br/><br/>
       
        <label class="lb" style=" margin-left: 0px;">STUDENT NAME: </label> 
        <asp:Label ID="student_of_name" runat="server" 
                Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Name of Student       &nbsp;" class="lb1" ></asp:Label><br/><br/>
      
      <label class="lb" style=" margin-left: 0px;">STUDENT ID: </label> 
        <asp:Label ID="student_id" runat="server" 
                Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Student ID &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" 
                class="lb1" ></asp:Label>
       
       </p>
           
            <p style=" float: right; margin-right: 5px;  margin-top: -6px;">
                <asp:Image ID="Image3" runat="server" style=" height:115px; width:140px; border: 2px solid deeppink;" />
            </p>




            <p style=" margin-left: 370px; margin-top: 0px;">
            <br/> <label class="lb" >SESSION: </label> 
        <asp:Label ID="session_d" runat="server" 
                    Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; session &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" 
                    class="lb1" ></asp:Label><br/><br/>
       
       <label class="lb" >CLASS: </label> 
        <asp:Label ID="class_d" runat="server" 
                    Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Class &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" 
                    class="lb1" ></asp:Label><br/><br/>
       
       <label class="lb" style=" margin-left: -33px;">NO. ON ROLL: </label> 
        <asp:Label ID="no_of_student" runat="server" 
                    Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Class &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" 
                    class="lb1" ></asp:Label>
       
            </p>

            <hr style=" border: 2px solid black; width:100%; margin-top: 0px; "/>
        <div style="display:flex;position:relative; margin-bottom:0px;">
            <asp:GridView ID="GridView1" runat="server" BorderStyle="Double" 
                EmptyDataText="Your result had not been uploaded. Try again later ..." 
                Font-Bold="True" Font-Names="Times New Roman" Font-Size="13pt" 
                style=" margin-left: 13px; margin-right: 0px; text-align: center; font-family: Times New Roman; font-size: 13px;  height:500px; width: 940px; font-weight: bold;">
                <AlternatingRowStyle ForeColor="Blue" />
                <HeaderStyle BorderStyle="Double" Font-Italic="False" 
                    Font-Names="Times New Roman" Font-Size="13pt" ForeColor="crimson" />
                <RowStyle BorderStyle="Double" Font-Names="Times New Roman" Font-Size="13pt" 
                    ForeColor="purple" />
            </asp:GridView>
        <!-- <p style="transform: rotate(270deg); transform-origin: left top 0; float: left; margin-top:380px;flex:1; "><asp:Label ID="name_school_side" runat="server" Text="Label" style=" font-family: Cursive; font-size: 17px;  color: midnightblue; "></asp:Label> </p>-->
            </div>


            <div runat="server" id="div_first_second_mid" style=" font-family: Times New Roman; font-size: 18px; margin-top:-30px;"> 
           <p style=" float: left; margin-left: 60px; "> <br/>
            Total Mark Obtainable<br/>
            Total Mark Obtained<br/>
            Cummulative %<br/>
            Overall Grade<br/>
           Cummulative Remark
            </p>

             <p style=" float: right; margin-right: 360px;"> <br/>
           <asp:Label ID="total_mark_obtainable1" runat="server" Text="Label"></asp:Label><br/>
            <asp:Label ID="total_mark_obtained1" runat="server" Text="Label"></asp:Label><br/>
            <asp:Label ID="cummulative_percent1" runat="server" Text="Label"></asp:Label><br/>
            <asp:Label ID="cummulative_grade1" runat="server" Text="Label"></asp:Label><br/>
           <asp:Label ID="cummulative_remark1" runat="server" Text="Label"></asp:Label>
            </p>
            <P><br/><br/><br/><br/><br/><br/></P>
            </div>



            <div runat="server" id="div_third_only" style=" font-family: Times New Roman; font-size: 18px; margin-top:-20px;"> 

            <p style=" float: left; margin-left: 30px; font-family: Times New Roman; font-size: 17px; font-weight: bold;">
            <font>Total Mark Obtainable</font>  <asp:TextBox ID="total_mark_obtainable2" 
                    runat="server" 
                    style=" margin-left: 17px; border: 3px solid black; width: 80px; text-align: center; height: 19px;"></asp:TextBox><br/>
           <font> Total Mark Obtained </font>    <asp:TextBox ID="total_mark_obtained2" 
                    runat="server" 
                    style=" margin-left: 31px;border: 3px solid black; width: 80px; text-align: center; height: 19px;"></asp:TextBox><br/>
           <font> Cummulative (%)  </font>       <asp:TextBox ID="aggregate2" runat="server" 
                    style=" margin-left: 54px;border: 3px solid black; width: 80px; text-align: center; height: 19px;"></asp:TextBox>
            </p>

             <p style=" float: right;">
              <font>Session Average(%)</font>  <asp:TextBox ID="session_average2" runat="server" 
                     style=" margin-left: 18px; border: 3px solid black; width: 80px; text-align: center; height: 19px;"></asp:TextBox><br/>
           <font>Cumm. Grade</font>    <asp:TextBox ID="cumm_grade2" runat="server" 
                     style=" margin-left: 62px;border: 3px solid black; width: 80px; text-align: center; height: 19px;"></asp:TextBox><br/>
           <font>Cumm. Remark</font>       <asp:TextBox ID="cumm_remark2" runat="server" 
                     style=" margin-left: 49px;border: 3px solid black; width: 80px; text-align: center; height: 19px;"></asp:TextBox>
          
            </p>

             <p runat="server" id="vgv" style=" margin-left: 320px;">
             <br/>
              <font>First Term Cumm.(%)</font>  <asp:TextBox ID="cummulative_first" runat="server" 
                     style=" margin-left: 31px; border: 3px solid black; width: 80px; text-align: center; height: 19px;"></asp:TextBox><br/>
           <font>Second Term Cumm.(%)</font>    <asp:TextBox ID="cummulative_second" 
                     runat="server" 
                     style=" margin-left: 11px;border: 3px solid black; width: 80px; text-align: center; height: 19px;"></asp:TextBox><br/>
           <font>Third Term Cumm.(%)</font>       <asp:TextBox ID="cummulative_third" 
                     runat="server" 
                     style=" margin-left: 24px;border: 3px solid black; width: 80px; text-align: center; height: 19px;"></asp:TextBox>
          
            </p>
          

            </div>


             <hr runat="server" id="line_third" visible="false" style=" float: left; width:100%; border: 2px solid black; margin-top:-15px;"/>
            <hr runat="server" id="line_first_second" visible="false" style=" float: left; width:100%; border: 2px solid black;"/><br/>
            
            
           <br/>
            <div style="margin-left: 10px; ">
                <label class="lab" style=" font-weight: bold;" 
                    title="Class Teacher's Comment">
                 CLASS TEACHER&#39;S COMMENT:
                </label>
                <asp:Label ID="comment_teacher" runat="server" class="comment_class_teacher" 
                    style=" border-bottom: 1px solid black;margin-left: 5px; font-family: Times New Roman; font-size: 18px;" 
                    Text=" Not yet available from the class teacher . "></asp:Label>
                <br/>
                
                <asp:Label ID="label_comment_head" runat="server" class="lab" 
                    style=" font-weight: bold;margin-left: 0px; text-transform: uppercase;" 
                    Text="Principal's Comment:" title="Principal's Remark"></asp:Label>
                <asp:Label ID="comment_principal1" runat="server" class="comment_class_teacher" 
                    style=" border-bottom: 1px solid black;margin-left: 5px; font-family: Times New Roman; font-size: 18px;" 
                    Text=" Comment/ Remark not yet available from the School Principal .."></asp:Label>


                
                <asp:Image ID="Image1" runat="server" 
                    
                    style=" float: right; width: 155px; height: 35px; border: 0px; margin-right: 5px; margin-top: -16px;" />

                  <br/><br/>  <asp:Label ID="label_under_signature" runat="server" class="comment_principal" 
                    style=" border-top: 1px solid black; float: right; font-weight: bold; font-size: 18px; margin-right: 5px; margin-top: -16px; " 
                    Text="Text"></asp:Label>
                    </div>

                <br/>

                 <p runat="server" id="p_resumption_date" style="margin-left: 10px;  margin-top: -40px;"><asp:Label ID="label12" 
                        runat="server" class="lab" style=" font-weight: bold; text-transform: uppercase;" 
                    Text="RESUMPTION DATE:" title="Principal's Remark"></asp:Label>
                <asp:Label ID="next_term_begins" runat="server" class="comment_class_teacher" 
                    style=" border-bottom: 1px solid crimson; margin-left: 5px; font-family: Comic Sans MS; font-size: 17px; color: crimson;" 
                    Text=" &nbsp;&nbsp;&nbsp;&nbsp; Not yet available from the school authority . &nbsp;&nbsp;&nbsp;&nbsp;"></asp:Label>
                  </p>



                <p runat="server" id="p_promotion_status" style="margin-left: 10px;  margin-top: -15px;"><asp:Label ID="label2" 
                        runat="server" class="lab" style=" font-weight: bold; text-transform: uppercase;" 
                    Text="PROMOTION STATUS:" title="Principal's Remark"></asp:Label>
                <asp:Label ID="promotion_status" runat="server" class="comment_class_teacher" 
                    style=" border-bottom: 1px solid black; margin-left: 5px; font-family: Comic Sans MS; font-size: 17px;" 
                    Text=" &nbsp;&nbsp;&nbsp;&nbsp; PROMOTED . &nbsp;&nbsp;&nbsp;&nbsp;"></asp:Label>
                  </p>

                <asp:GridView ID="GridView_time_open" runat="server" BorderStyle="Double" 
                    Height="197px" style=" float: left; margin-left: 10px; width: 247px;" >
                    <HeaderStyle BorderStyle="Double" Font-Bold="True" Font-Names="Times New Roman" 
                        Font-Size="14pt" HorizontalAlign="Center" VerticalAlign="Middle" />
                    <RowStyle BorderStyle="Double" Font-Bold="True" Font-Names="Times New Roman" 
                        Font-Size="13pt" HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:GridView>
                <asp:GridView ID="GridView_trait" runat="server" AutoGenerateColumns="False" 
                    BorderStyle="Double" Height="197px" style=" float: right; margin-right: 5px;" 
                    Width="445px">
                    <Columns>
                        <asp:BoundField DataField="Qualities" HeaderText="Qualities" />
                        <asp:ImageField DataImageUrlField="Excellent" HeaderText="Excellent" 
                            NullImageUrl="~/images/click1.PNG">
                        </asp:ImageField>
                        <asp:ImageField DataImageUrlField="Good" HeaderText="Good">
                        </asp:ImageField>
                        <asp:ImageField DataImageUrlField="Fair" HeaderText="Fair">
                        </asp:ImageField>
                        <asp:ImageField DataImageUrlField="Poor" HeaderText="Poor">
                        </asp:ImageField>
                    </Columns>
                    <HeaderStyle BorderStyle="Double" Font-Bold="True" Font-Names="Times New Roman" 
                        Font-Size="14pt" HorizontalAlign="Center" VerticalAlign="Middle" />
                    <RowStyle BorderStyle="Double" Font-Bold="True" Font-Names="Times New Roman" 
                        Font-Size="13pt" HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:GridView>
                

                 <!-- <p style=" margin-top: 28%; ">
                

                <br/>
                <br/>
                <br/>
                <br/>
                
                </p>-->




               <!-- <br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/>


                <span style=" color: Window; margin-bottom: 0px; height: 20px; width: 100%;  ">
                    <font style="margin-left: 500px;  color: crimson; margin-top: 60px;">Powered By:</font>
                    <font style=" color:orangered ">schoolnigeria.com.ng</font></span>-->



                <asp:TextBox ID="class_student" runat="server" Visible="False"></asp:TextBox>
                <asp:TextBox ID="aggregate" runat="server" Visible="False"></asp:TextBox>
                  <asp:TextBox ID="a" runat="server" Visible="False"></asp:TextBox>
                <asp:TextBox ID="b" runat="server" Visible="False"></asp:TextBox>
                 <asp:TextBox ID="c" runat="server" Visible="False"></asp:TextBox>
                <asp:TextBox ID="TextBox3" runat="server" Visible="False"></asp:TextBox>
















                
                
                <!-- END SUB ENCLOSE DIV -->
                    </div>
   
   


   </asp:Panel>

    </form>

    <!-- START OF JAVASCRIPT FOR PRINTING -->
     <script language="javascript" type="text/javascript">
         function printDiv(divID) {
             //Get the HTML of div
             var divElements = document.getElementById(divID).innerHTML;
             //Get the HTML of whole page
             var oldPage = document.body.innerHTML;

             //Reset the page's HTML with div's HTML only
             document.body.innerHTML =
                 "<html><head><title></title></head><body>" +
                 divElements + "</body>";

             //Print Page
             window.print();

             //Restore orignal HTML
             document.body.innerHTML = oldPage;


         }
    </script>

</body>
</html>
