using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.classes
{
    class Clients
    {
        public int id_client { get; set; }
        public string phone { get; set; }
        public string adress { get; set; }
        public Clients() { }
        public Clients(int id_client, string phone, string adress)
        {
            this.id_client = id_client; 
            this.phone = phone;
            this.adress = adress;
        }
        public Clients(string phone, string adress)
        {
            this.phone = phone;
            this.adress = adress;
        }
        public override string ToString()
        {
            return "id: " + id_client + "\nphone: " + phone + "\nadress: " + adress + "\n";
        }
    }
}
