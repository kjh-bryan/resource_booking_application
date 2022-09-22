using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    class Renter : User
    {
        private string nationality;

        public Renter()
        {
            
        }
        public Renter(string e, string p, string n, int c, bool r, bool o) : base(e,p,n,c,r,o)
        {

        }
    }
}
