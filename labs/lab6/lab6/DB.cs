using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.OracleClient;

namespace lab1_3
{
    class DB
    {
        OracleConnection conn;
        public void openConnection(string connStr)
        {
            conn = new OracleConnection(connStr);
            conn.Open();
        }

        public void closeConnection()
        {
            conn.Close();
        }

        //CLIENTS
        public void newClient(string phone, string adress)
        {
            using (OracleCommand cmd = new OracleCommand("newClient", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@adress", adress);
                cmd.ExecuteNonQuery();
            }
        }
        public void drop_client(int id)
        {
            using (OracleCommand cmd = new OracleCommand("delClient", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idclient", id);
                cmd.ExecuteNonQuery();
            }

        }
        public void changeClient_Click(int id, string phone)
        {
            using (OracleCommand cmd = new OracleCommand("updatePhone", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idclient", id);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.ExecuteNonQuery();
            }
        }
        //EMPLOYEE
        public void newEmployee(string name, string lastname)
        {
            using (OracleCommand cmd = new OracleCommand("addNewEmployee", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@lastname", lastname);
                cmd.ExecuteNonQuery();
            }
        }

        public void del_employee(string id)
        {
            using (OracleCommand cmd = new OracleCommand("deleteEmployee", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        //orders
        public void add_order(int id_employee, int id_client)
        {
            using (OracleCommand cmd = new OracleCommand("newOrder", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@order_employee_id", id_employee);
                cmd.Parameters.AddWithValue("@order_client_id", id_client);
                cmd.ExecuteNonQuery();
            }
        }//TODO
        public void add_product_to_order(int id_order, int id_product, int count_product)
        {
            using (OracleCommand cmd = new OracleCommand("addProductinOrder", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@order_id", id_order);
                cmd.Parameters.AddWithValue("@product_id", id_product);
                cmd.Parameters.AddWithValue("@product_count", count_product);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
