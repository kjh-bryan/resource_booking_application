using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    public class User
    {
        private string email, password, name;
        private int contact;
        private bool renter;
        private bool owner;
        public User()
        {

        }
        public User(string e,string p, string n, int c, bool r,bool o)
        {
            email = e;
            password = p;
            name = n;
            contact = c;
            if(r)
            {
                owner = false;
                renter = true;
            }
            else if(o)
            {
                o = true;
                renter = false;
            }

        }
        public string Email
        {
            get { return this.email; }
            set { this.email = value; }
        }
        public string Password
        {
            get { return this.password; }
            set { this.password = value; }
        }
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        public int Contact
        {
            get { return this.contact; }
            set { this.contact = value; }
        }
        
    }
}
