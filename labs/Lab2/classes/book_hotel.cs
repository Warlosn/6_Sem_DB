using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.classes
{
    class book_hotel
    {
        public int id { get; set; }
        public string name { get; set; }
        public byte stars { get; set; }
        public string address { get; set; }
        public string date_start { get; set; }
        public string date_finish { get; set; }
        public book_hotel() { }
        public book_hotel(int id, string name, byte stars, string address, string date_start, string date_finish)
        {
            this.id = id;
            this.name = name;
            this.stars = stars;
            this.address = address;
            this.date_start = date_start;
            this.date_finish = date_finish;
        }
        public book_hotel(string name, byte stars, string address, string date_start, string date_finish)
        {
            this.name = name;
            this.stars = stars;
            this.address = address;
            this.date_start = date_start;
            this.date_finish = date_finish;
        }
        public override string ToString()
        {
            return "id: " + id + "\nname: " + name + "\nstars: " + stars + "\naddress: " + address
                + "\ndate_start: " + date_start + "\ndate_finish: " + date_finish + "\n";
        }
    }
}
