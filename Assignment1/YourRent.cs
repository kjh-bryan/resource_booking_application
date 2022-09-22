using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    class YourRent : Flat
    {
        private string status;
        private int duration;
        private int totalPrice;

        public YourRent(int d, string s, string u,string o, string i, string f, string t, string a, string aname, string b, string str, string z, string desc, string n, string typ, string pa, string pr) : base(u,o,i,f,t,a,aname,b,str,z,desc,n,typ,pa,pr)
        {
            duration = d;
            status = s;
        }
        public YourRent()
        {

        }
        public int Duration
        {
            get { return this.duration; }
            set { this.duration = value; }
        }
        public string Status
        {
            get { return this.status; }
            set { this.status = value; }
        }
        public void setTotalPrice()
        {
            totalPrice = int.Parse(Price) * duration;
        }


    }
}
