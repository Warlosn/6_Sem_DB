using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.classes
{
    class trip
    {
        public int id { get; set; }
        public string from_county { get; set; }
        public string to_county { get; set; }
        public byte numb_people { get; set; }
        public string date_start { get; set; }
        public string date_finish { get; set; }
        public string transport { get; set; }
        public string duration { get; set; }
        public double cost { get; set; }
        public trip() { }
        public trip(int id, string from, string to, byte numb_people, string start, string finish, string transport, string duration, double cost)
        {
            this.id = id;
            from_county = from;
            to_county = to;
            this.numb_people = numb_people;
            date_start = start;
            date_finish = finish;
            this.transport = transport;
            this.duration = duration;
            this.cost = cost;
        }

        public trip(string from, string  to, byte numb_people, string start, string finish, string transport, string duration, double cost)
        {
            from_county = from;
            to_county = to;
            this.numb_people = numb_people;
            date_start = start;
            date_finish = finish;
            this.transport = transport;
            this.duration = duration;
            this.cost = cost;
        }
        public override string ToString()
        {
            return "id: " + id + "\nfrom_country: " + from_county + "\nto_country: " + to_county
                + "\nnumb_people: " + numb_people + "\ndate_start: " + date_start + "\ndate_finish: " + date_finish 
                + "\ntransport: " + transport + "\nduration: " + duration + "\ncost: " + cost + "\n";
        }
    }
}
