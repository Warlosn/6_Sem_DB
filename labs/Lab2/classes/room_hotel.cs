using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.classes
{
    class room_hotel
    {
        public int id { get; set; }
        public int id_hotel { get; set; }
        public int number { get; set; }
        public bool is_free { get; set; }
        public double cost { get; set; }
        public string room_type { get; set; }
        public room_hotel() { }
        public room_hotel(int id, int id_hotel, int number, bool is_free, double cost, string room_type)
        {
            this.id = id;
            this.id_hotel = id_hotel;
            this.number = number;
            this.is_free = is_free;
            this.cost = cost;
            this.room_type = room_type;
        }

        public room_hotel(int id_hotel, int number, bool is_free, double cost, string room_type)
        {
            this.id_hotel = id_hotel;
            this.number = number;
            this.is_free = is_free;
            this.cost = cost;
            this.room_type = room_type;
        }
        public override string ToString()
        {
            return "id: " + id + "\nid_hotel: " + id_hotel + "\nnumber: " + number
                + "\nis_free: " + is_free + "\ncost: " + cost + "\nroom_type: " + room_type + "\n";
        }
    }
}
