using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WEB_SCONET_MANAGEMENT.Models
{
    public class MISC
    {
        [Display(Name = "List of Staff")]
        public string list_of_staff { get; set; }
        [Display(Name ="Manage Class")]
        public bool manage_class { get; set; }
        [Display(Name = "Manage Subject")]
        public bool manage_subject { get; set; }
        [Display(Name = "Manage Staff")]
        public bool manage_staff { get; set; }
        [Display(Name = "Configuration")]

        public bool configuration { get; set; }
        [Display(Name = "Default Principal Comment")]
        public bool default_principal_comment { get; set; }

        [Display(Name = "Input Result")]
        public bool input_result { get; set; }
        [Display(Name = "Input Comment")]
        public bool input_comment { get; set; }
        [Display(Name = "Set Resumption Date")]
        public bool set_resumption_date { get; set; }
        [Display(Name = "Set Promotion Range")]
        public bool set_promotion_quota { get; set; }
        [Display(Name = "Manage Student Status")]
        public bool manage_student_status { get; set; }
        [Display(Name = "Print Report Sheet")]
        public bool print_report_sheet { get; set; }
        [Display(Name = "Manage Fee")]
        public bool manage_fee { get; set; }
        [Display(Name = "Make Payment")]
        public bool make_payment { get; set; }
        [Display(Name = "Inventory")]
        public bool inventory { get; set; }
        [Display(Name = "Expenditure")]
        public bool expenditure { get; set; }
        [Display(Name = "Scholarship")]
        public bool scholarship { get; set; }
        [Display(Name = "Manage Payroll")]
        public bool manage_payroll { get; set; }
        [Display(Name = "View Payroll")]
        public bool view_payroll { get; set; }
        [Display(Name = "Generate Broadsheet")]
        public bool generate_broad_sheet { get; set; }
        [Display(Name = "Manage User")]
        public bool manage_user { get; set; }
        [Display(Name = "Manage Role")]
        public bool manage_role { get; set; }
        [Display(Name = "View Account Report")]
        public bool report_account { get; set; }
        [Display(Name = "Manage Student Record")]
        public bool manage_student_record { get; set; }
        [Display(Name = "Send SMS Notification")]
        public bool send_sms_notification { get; set; }
        [Display(Name = "Manage Time Table")]
        public bool manage_time_table { get; set; }
        public bool send_email { get; set; }
        public bool send_sms { get; set; }
        
    }
}