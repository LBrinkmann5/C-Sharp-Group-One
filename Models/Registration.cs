using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Southwest_Airlines.Models
{
    public class Registration
    {
        private string _user = "";
        private string _pass = "";
        private string _email = "";
        private string _phone = "";
        private string _address = "";
        private string _fname = "";
        private string _lname = "";


        [Required(ErrorMessage =" * Please enter a username.")]
        public string? TBuser
        {
            get { return _user; }
            set { _user = value; }
        }
        [Required(ErrorMessage = " * Please enter a password.")]
        [RegularExpression(@"^(?=.*\d)(?=.*[A-Z])(?=.*[!@#$%^&*()<>{}[\]-_=+,.?"":])(?!.*\s\s).{10,}$", ErrorMessage = " * Password must be at least 10 characters long, contain at least one uppercase letter, one number, and one special character.")]
        public string TBpass
        {
            get { return _pass; }
            set { _pass = value; }
        }
        [Required(ErrorMessage = " * Please enter an email.")]
        [RegularExpression(@"^((?!\.)[\w\-_.]*[^.])(@\w+)(\.\w+(\.\w+)?[^.\W])$", ErrorMessage = " * Please enter a valid email address")]
        public string TBemail
        {
            get { return _email; }
            set { _email = value; }
        }
        [Required(ErrorMessage = " * Please enter a phone number.")]
        [RegularExpression(@"^[\d]{10,15}$", ErrorMessage = " * Please enter a valid phone number")]
        public string TBphone
        {
            get { return _phone; }
            set { _phone = value; }
        }
        [Required(ErrorMessage = " * Please enter an address.")]
        public string TBaddress
        {
            get { return _address; }
            set { _address = value; }
        }
        [Required(ErrorMessage = " * Please enter a first name.")]
        public string TBfname
        {
            get { return _fname; }
            set { _fname = value; }
        }
        [Required(ErrorMessage = " * Please enter a last name.")]
        public string TBlname
        {
            get { return _lname; }
            set { _lname = value; }
        }



        //public bool validUser()
        //{
        //    string passwordvalid = @"^(?=.*\d)(?=.*[A-Z])(?=.*[!@#$%^&*()<>{}[\]-_=+,.?"":])(?!.*\s\s).{10,}$";
        //    string emailvalid = @"^((?!\.)[\w\-_.]*[^.])(@\w+)(\.\w+(\.\w+)?[^.\W])$";
        //    string phonevalid = @"^ [\d]{10,15}$";

        //    bool isValid = true;
        //    //Password validation
        //    if (string.IsNullOrEmpty(_pass))
        //    {
        //        isValid = false;
        //    }
        //    else if (!Regex.IsMatch(_pass, passwordvalid))
        //    {
        //        isValid = false;
        //    }
        //    //Email validation
        //    if (string.IsNullOrEmpty(_email))
        //    {
        //        isValid = false;
                
        //    }
        //    else if(!Regex.IsMatch(_email, emailvalid))
        //    {
        //        isValid = false;
        //    }
        //    //Phone validation
        //    if (string.IsNullOrEmpty(_phone))
        //    {
        //        isValid = false;
        //    }
        //    else if (!Regex.IsMatch(_phone, phonevalid))
        //    {
        //        isValid = false;
        //    }
        //    //Username validation
        //    if (string.IsNullOrEmpty(_user))
        //    {
        //        isValid = false;
        //    }
        //    //Address validation
        //    if (string.IsNullOrEmpty(_address))
        //    {
        //        isValid = false;
        //    }

        //    //First name validation
        //    if (string.IsNullOrEmpty(_fname))
        //    {
        //        isValid = false;
        //    }

        //    //Last name validation
        //    if (string.IsNullOrEmpty(_lname))
        //    {
        //        isValid = false;
        //    }

        //    return isValid;
        //}
    }
}
