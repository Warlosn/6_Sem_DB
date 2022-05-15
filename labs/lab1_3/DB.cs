using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace lab1_3
{
    class DB
    {
        SqlConnection conn;
        public void openConnection(string connStr)
        {
            conn = new SqlConnection(connStr);
            conn.Open();
        }

        public void closeConnection()
        {
            conn.Close();
        }

        //CLIENTS
        public void newClient(string phone, string adress)
        {
            using (SqlCommand cmd = new SqlCommand("newClient", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@adress", adress);
                cmd.ExecuteNonQuery();
            }
        }
        public void drop_client(int id)
        {
            using (SqlCommand cmd = new SqlCommand("delClient", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idclient", id);
                cmd.ExecuteNonQuery();
            }

        }
        public void changeClient_Click(int id, string phone)
        {
            using (SqlCommand cmd = new SqlCommand("updatePhone", conn))
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
            using (SqlCommand cmd = new SqlCommand("addNewEmployee", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@lastname", lastname);
                cmd.ExecuteNonQuery();
            }
        }
        
        public void del_employee(string id)
        {
            using (SqlCommand cmd = new SqlCommand("deleteEmployee", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        //orders
        public void add_order(int id_employee, int id_client)
        {
            using (SqlCommand cmd = new SqlCommand("newOrder", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@order_employee_id", id_employee);
                cmd.Parameters.AddWithValue("@order_client_id", id_client);
                cmd.ExecuteNonQuery();
            }
        }//TODO
        public void add_product_to_order(int id_order, int id_product, int count_product)
        {
            using (SqlCommand cmd = new SqlCommand("addProductinOrder", conn))
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
