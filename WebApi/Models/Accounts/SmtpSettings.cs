namespace WebApi.Services
{
    class SmtpSettings
    {
        public string from { get; set; }
        public string host { get; set; }
        public string password { get; set; }
        public string port { get; set; }
        public string sender { get; set; }
        public string user { get; set; }
        public string cc { get; set; }
    }
}


/*
Example as Environment variable:

{
    "from":"Timer App (no reply)", 
    "host": "mail.mymailhost.net", 
    "password": "c832BJasfl@a)*3yQ8oarpoir", 
    "port": "587", 
    "sender": "noreply@myaddress.net", 
    "user": "noreply@myaddress.net", 
    "cc":"copyaccount@myanotheraddress.com"
}

*/