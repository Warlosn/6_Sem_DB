using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.classes
{
    class booking
    {
        public int id { get; set; }
        public int id_trip { get; set; }
        public int id_user { get; set; }
        public int id_hotel { get; set; }
        public booking() { }
        public booking(int id, int id_trip, int id_user, int id_hotel)
        {
            this.id = id;
            this.id_user = id_user;
            this.id_trip = id_trip;
            this.id_hotel = id_hotel;
        }
        public booking(int id_trip, int id_user, int id_hotel)
        {
            this.id_user = id_user; 
            this.id_trip = id_trip;
            this.id_hotel = id_hotel;
        }
        public override string ToString()
        {
            return "id: " + id + "\nid_trip: " + id_trip + "\nid_user: " + id_user + "\nid_hotel: " + id_hotel + "\n";
        }
    }
}
