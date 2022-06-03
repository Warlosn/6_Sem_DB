using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.classes
{
    class Employee
    {
        public int id_employee { get; set; }
        public string name_employee { get; set; }
        public string lastname_employee { get; set; }
        public Employee() { }
        public Employee(int id_employee, string name_employee, string lastname_employee)
        {
            this.id_employee = id_employee;
            this.name_employee = name_employee;
            this.lastname_employee = lastname_employee;
        }
        public Employee(string name_employee, string lastname_employee)
        {
            this.name_employee = name_employee;
            this.lastname_employee = lastname_employee;
        }
        public override string ToString()
        {
            return "id_employee: " + id_employee + "\nname_employee: " + name_employee + "\nlastname_employee: " + lastname_employee +"\n";
        }
    }
}
