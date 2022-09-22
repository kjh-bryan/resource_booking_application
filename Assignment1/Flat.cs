using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    public class Flat
    {
        private string uid,owner,imageDirectory,from,to,area,areaname,blk,street,zipcode,description,noOfRoom,type,pax,price;
        public Flat()
        {

        }
        public Flat(string u,string o, string i, string f, string t,string a, string aname, string b, string s, string z,string d,string n,string typ, string pa, string pr)
        {
            uid = u;
            owner = o;
            imageDirectory = i;
            from = f;
            to = t;
            area = a;
            areaname = aname;
            blk = b;
            street = s;
            zipcode = z;
            description = d;
            noOfRoom = n;
            type = typ;
            pax = pa;
            price = pr;
        }
        public string Uid
        {
            get { return this.uid; }
            set { this.uid = value; }
        }
        public string Owner
        {
            get { return this.owner; }
            set { this.owner = value; }
        }
        public string ImageDirectory
        {
            get { return this.imageDirectory; }
            set { this.imageDirectory = value; }
        }
        public string From
        {
            get { return this.from; }
            set { this.from = value; }
        }
        public string To
        {
            get { return this.to; }
            set { this.to = value; }
        }

        public string Area
        {
            get { return this.area; }
            set { this.area = value; }
        }
        public string Areaname
        {
            get { return this.areaname; }
            set { this.areaname = value; }
        }

        public string Blk
        {
            get { return this.blk; }
            set { this.blk = value; }
        }

        public string Street
        {
            get { return this.street; }
            set { this.street = value; }
        }

        public string Zipcode
        {
            get { return this.zipcode; }
            set { this.zipcode = value; }
        }

        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        public string Noofroom
        {
            get { return this.noOfRoom; }
            set { this.noOfRoom = value; }
        }

        public string Type
        {
            get { return this.type; }
            set { this.type = value; }
        }
        public string Pax
        {
            get { return this.pax; }
            set { this.pax = value; }
        }

        public string Price
        {
            get { return this.price; }
            set { this.price = value; }
        }
    }
}
