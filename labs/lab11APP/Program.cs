using Lab2.classes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security;
using Microsoft.Data.Sqlite;


namespace Lab2
{
    class Program
    {
        static List<Employee> employee = new List<Employee>();
        static List<Products> products = new List<Products>();
        static List<Clients> clients = new List<Clients>();


        static string connectionString = @"Data Source=D:\GitHub\6_Sem_DB\labs\lab11\lab11.db";
        public static void Main(string[] args)
        {
            SqliteConnection connection = new SqliteConnection(connectionString);
            try
            {
                connection.Open();
                Console.WriteLine("Подключение открыто");
                if (!chooseTable(connection))
                    throw new Exception("Error: invalid value");
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        static bool chooseTable(SqliteConnection connection)
        {
            int value;
            while (true)
            {
                Console.WriteLine("Choose table:\n1. employee\n2. products\n3. clients\n4. to exit");
                value = Int32.Parse(Console.ReadLine());
                switch (value)
                {
                    case 1:
                        employeeMethods(connection);
                        break;
                    case 2:
                        productsMethods(connection);
                        break;
                    case 3:
                        clientsMethods(connection);
                        break;
                    case 4:
                        return true;
                    default:
                        continue;
                }
            }
        }
        static void employeeMethods(SqliteConnection connection)
        {
            string method = "";
            while (!method.Equals("exit"))
            {
                method = ChooseMethod();
                switch (method)
                {
                    case "select":
                        employee = selectEmployee(connection);
                        foreach (Employee item in employee)
                        {
                            Console.Write("----------------------------------------\n");
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write(item.ToString());
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        Console.Write("----------------------------------------\n");
                        break;
                    case "insert":
                        Console.WriteLine("Write insert employee: name_employee/lastname_employee");
                        string[] str = Console.ReadLine().Split('/');
                        insertEmployee(connection, new Employee(str[0],str[1]));
                        break;
                    case "update":
                        Console.WriteLine("Write update employee: id_employee/name_employee/lastname_employee");
                        string[] upd = Console.ReadLine().Split('/');
                        updateEmployee(connection, new Employee(Convert.ToInt32(upd[0]), upd[1], upd[2]));
                        break;
                    case "delete":
                        Console.WriteLine("Write delete id_employee: ");
                        string del = Console.ReadLine();
                        deleteEmployee(connection, Convert.ToInt32(del));
                        break;
                    default:
                        continue;
                }
            }
        }
        static void productsMethods(SqliteConnection connection)
        {
            string method = "";
            while (!method.Equals("exit"))
            {
                method = ChooseMethod();
                switch (method)
                {
                    case "select":
                        products = selectProducts(connection);
                        foreach (Products item in products)
                        {
                            Console.Write("----------------------------------------\n");
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write(item.ToString());
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        Console.Write("----------------------------------------\n");
                        break;
                    case "insert":
                        Console.WriteLine("Write value products: product_name/price");
                        string[] str = Console.ReadLine().Split('/');
                        insertProducts(connection, new Products(str[0],Convert.ToInt32(str[1])));
                        break;
                    case "update":
                        Console.WriteLine("Write value products: product_id/product_name/price");
                        string[] upd = Console.ReadLine().Split('/');
                        updateProducts(connection, new Products(Convert.ToInt32(upd[0]), upd[1], Convert.ToInt32(upd[2])));
                        break;
                    case "delete":
                        Console.WriteLine("Write id delete product_id: ");
                        string del = Console.ReadLine();
                        deleteProducts(connection, Convert.ToInt32(del));
                        break;
                    default:
                        continue;
                }
            }
        }
  
        static void clientsMethods(SqliteConnection connection)
        {
            string method = "";
            while (!method.Equals("exit"))
            {
                method = ChooseMethod();
                switch (method)
                {
                    case "select":
                        clients = selectClients(connection);
                        foreach (Clients item in clients)
                        {
                            Console.Write("----------------------------------------\n");
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write(item.ToString());
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        Console.Write("----------------------------------------\n");
                        break;
                    case "insert":
                        Console.WriteLine("Write clients: phone/adress");
                        string[] str = Console.ReadLine().Split('/');
                        insertClients(connection, new Clients(Convert.ToString(str[0]), Convert.ToString(str[1])));
                        break;
                    case "update":
                        Console.WriteLine("Write clients: id_clients/phone/adress");
                        string[] upd = Console.ReadLine().Split('/');
                        updateClients(connection, new Clients(Convert.ToInt32(upd[0]), Convert.ToString(upd[1]), Convert.ToString(upd[2])));
                        break;
                    case "delete":
                        Console.WriteLine("Write id delete client: ");
                        string del = Console.ReadLine();
                        deleteClients(connection, Convert.ToInt32(del));
                        break;
                    default:
                        continue;
                }
            }
        }

        static List<Employee> selectEmployee(SqliteConnection connection)
        {
            List<Employee> list = new List<Employee>();
            string expression = "select * from Employee";
            SqliteCommand command = new SqliteCommand(expression, connection);
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Employee employee = new Employee();
                    employee.id_employee = reader.GetInt32(0);
                    employee.name_employee = reader.GetString(1);
                    employee.lastname_employee = reader.GetString(2);
                    list.Add(employee);
                }
            }
            return list;
        }
        static List<Products> selectProducts(SqliteConnection connection)
        {
            List<Products> list = new List<Products>();
            string expression = "select * from Products";
            SqliteCommand command = new SqliteCommand(expression, connection);
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Products product = new Products();
                    product.id_product = reader.GetInt32(0);
                    product.product_name = reader.GetString(1);
                    product.price = reader.GetInt32(2);
                    list.Add(product);
                }
            }
            return list;
        }
      
        static List<Clients> selectClients(SqliteConnection connection)
        {
            List<Clients> list = new List<Clients>();
            string expression = "select * from Clients";
            SqliteCommand command = new SqliteCommand(expression, connection);
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Clients clients = new Clients();
                    clients.id_client = reader.GetInt32(0);
                    clients.phone = reader.GetString(1);
                    clients.adress = reader.GetString(2);
                    list.Add(clients);
                }
            }
            return list;
        }
       

        static void insertEmployee(SqliteConnection connection, Employee employee)
        {
            string query = "insert into Employee(name_employee,lastname_employee) " +
                "values(@name_employee, @lastname_employee)";
            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@name_employee", employee.name_employee));
            command.Parameters.Add(new SqliteParameter("@lastname_employee", employee.lastname_employee));
            int result = command.ExecuteNonQuery();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"Added {result} rows");
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void insertProducts(SqliteConnection connection, Products products)
        {
            string query = "insert into Products(product_name,price) " +
                "values(@product_name, @price)";
            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@product_name", products.product_name));
            command.Parameters.Add(new SqliteParameter("@price", products.price));
            int result = command.ExecuteNonQuery();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"Added {result} rows");
            Console.ForegroundColor = ConsoleColor.White;

        }
      
        static void insertClients(SqliteConnection connection, Clients clients)
        {
            string query = "insert into Clients (phone,adress) " +
                "values(@id_phone, @id_adress)";
            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@id_phone", clients.phone));
            command.Parameters.Add(new SqliteParameter("@id_adress", clients.adress));
            int result = command.ExecuteNonQuery();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"Added {result} rows");
            Console.ForegroundColor = ConsoleColor.White;
        }
      

        static void updateEmployee(SqliteConnection connection, Employee employee)
        {
            string query = "update Employee set name_employee = @name_employee, lastname_employee = @lastname_employee" +
                " where id_employee = @id_employee";
            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@id_employee", employee.id_employee));
            command.Parameters.Add(new SqliteParameter("@name_employee", employee.name_employee));
            command.Parameters.Add(new SqliteParameter("@lastname_employee", employee.lastname_employee));
            int result = command.ExecuteNonQuery();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"Updated {result} rows");
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void updateProducts(SqliteConnection connection, Products products)
        {
            string query = "update Products set product_name = @product_name, price = @price" +
                " where id_product = @id_product";
            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@id_product", products.id_product));
            command.Parameters.Add(new SqliteParameter("@product_name", products.product_name));
            command.Parameters.Add(new SqliteParameter("@price", products.price));
            int result = command.ExecuteNonQuery();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"Updated {result} rows");
            Console.ForegroundColor = ConsoleColor.White;
        }
      
        static void updateClients(SqliteConnection connection, Clients clients)
        {
            string query = "update Clients set id_client = @id_client, phone = @phone, adress= @adress " +
                " where id_client = @id_client";
            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@id_client", clients.id_client));
            command.Parameters.Add(new SqliteParameter("@phone", clients.phone));
            command.Parameters.Add(new SqliteParameter("@adress", clients.adress));
            int result = command.ExecuteNonQuery();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"Updated {result} rows");
            Console.ForegroundColor = ConsoleColor.White;

        }

        static void deleteEmployee(SqliteConnection conneciton, int id_employee)
        {
            string query = "delete from Employee where id_employee = @id_employee";
            SqliteCommand command = new SqliteCommand(query, conneciton);
            command.Parameters.Add(new SqliteParameter("@id_employee", id_employee));
            int result = command.ExecuteNonQuery();
            Console.WriteLine($"Deleted {result} rows");
        }
        static void deleteProducts(SqliteConnection conneciton, int id_product)
        {
            string query = "delete from Products where id_product = @id_product";
            SqliteCommand command = new SqliteCommand(query, conneciton);
            command.Parameters.Add(new SqliteParameter("@id_product", id_product));
            int result = command.ExecuteNonQuery();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"Deleted {result} rows");
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void deleteClients(SqliteConnection conneciton, int id_client)
        {
            string query = "delete from Clients where id_client = @id_client";
            SqliteCommand command = new SqliteCommand(query, conneciton);
            command.Parameters.Add(new SqliteParameter("@id_client", id_client));
            int result = command.ExecuteNonQuery();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"Deleted {result} rows");
            Console.ForegroundColor = ConsoleColor.White;
        }
       
        static string ChooseMethod()
        {
            Console.WriteLine("Choose method(write method):\n1. select\n2. insert\n3. update\n4. delete\n5. exit");
            return Console.ReadLine().ToLower();
        }
    }
}