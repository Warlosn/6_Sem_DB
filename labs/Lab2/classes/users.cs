using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.classes
{
    class users
    {
        public int id { get; set; }
        public string fullname { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string passports_No { get; set; }
        public string visa { get; set; }
        public users() { }
        public users(int id, string fullname, string email, string password, string passports_No, string visa)
        {
            this.id = id;
            this.fullname = fullname;
            this.email = email;
            this.password = password;
            this.passports_No = passports_No;
            this.visa = visa;
        }
        public users(string fullname, string email, string password, string passports_No, string visa)
        {
            this.fullname = fullname;
            this.email = email;
            this.password = password;
            this.passports_No = passports_No;
            this.visa = visa;
        }
        public override string ToString()
        {
            return "id: " + id + "\nfullname: " + fullname + "\nemail: " + email + "\npassword: " + password
                + "\npassports: " + passports_No + "\nvisa: " + visa + "\n";
        }
    }
}
