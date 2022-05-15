using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lab1_3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        string connStr = @"Data Source=DESKTOP-E4TFQEB\MSSQLSERVER02;Initial Catalog=DB6SEM1_3;Integrated Security=True";
        DataTable Clients = new DataTable();
        DataTable Orders = new DataTable();
        DataTable Products = new DataTable();
        DataTable Employee = new DataTable();
        DataTable Orders_products = new DataTable();
        //CLIENTS
        private void allClients_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string sqlExpression = "getAllClients";

                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    connection.Open();
                    SqlDataAdapter command = new SqlDataAdapter(sqlExpression, connection);
                    // указываем, что команда представляет хранимую процедуру
                    Clients.Clear();
                    // Заполняем Dataset
                    command.Fill(Clients);
                    // Отображаем данные
                    usersGrid.ItemsSource = Clients.DefaultView;
                    connection.Close();
                }
            }
            catch
            {
                MessageBox.Show("Ошибка запроса");
            }
        }

        private void addClient_Click(object sender, RoutedEventArgs e)
        {
            string phone = textBoxPhoneClient.Text;
            string adress = textBoxAdressClient.Text;

            if (phone.Length == 0 || adress.Length == 0 )
            {
                MessageBox.Show("Проверьте данные");
            }
            else
            {
                DB db = new DB();
                db.openConnection(connStr);
                db.newClient(phone, adress);
                db.closeConnection();
            }
        }
        private void dropClient_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(textBoxIdClient.Text);
            if (textBoxIdClient.Text == null)
            {
                MessageBox.Show("Введите ID клиента");
            }
            else
            {
                DB db = new DB();
                db.openConnection(connStr);
                db.drop_client(id);
                db.closeConnection();
            }
        }
        private void changeClient_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(textBoxIdClient.Text);
            string phone = textBoxPhoneClient.Text;
            if (phone.Length == 0)
            {
                MessageBox.Show("Проверьте данные");
            }
            else
            {
                DB db = new DB();
                db.openConnection(connStr);
                db.changeClient_Click(id, phone);
                db.closeConnection();
            }
        }
        //PRODUCTS
        private void allProducts_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string sqlExpression = "GetAllProducts";

                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    connection.Open();
                    SqlDataAdapter command = new SqlDataAdapter(sqlExpression, connection);
                    // указываем, что команда представляет хранимую процедуру
                    Products.Clear();
                    // Заполняем Dataset
                    command.Fill(Products);
                    // Отображаем данные
                    ProductsGrid.ItemsSource = Products.DefaultView;
                    connection.Close();
                }
            }
            catch
            {
                MessageBox.Show("Ошибка запроса");
            }
        }

        //EMPLOYEE
        private void addEmployee_Click(object sender, RoutedEventArgs e)
        {
            string name = textBoxNameEmployee.Text;
            string lastname = textBoxLastNameEmployee.Text;

            if (name.Length == 0 || lastname.Length == 0)
            {
                MessageBox.Show("Проверьте данные");
            }
            else
            {
                DB db = new DB();
                db.openConnection(connStr);
                db.newEmployee(name, lastname);
                db.closeConnection();
            }
        }
        private void allEmployee_click(object sender, RoutedEventArgs e)
        {
            try
            {
                string sqlExpression = "getAllEmployee";

                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    connection.Open();
                    SqlDataAdapter command = new SqlDataAdapter(sqlExpression, connection);
                    // указываем, что команда представляет хранимую процедуру
                    Employee.Clear();
                    // Заполняем Dataset
                    command.Fill(Employee);
                    // Отображаем данные
                    dilerGrid.ItemsSource = Employee.DefaultView;
                    connection.Close();
                }
            }
            catch
            {
                MessageBox.Show("Ошибка запроса");
            }
        }
        private void delEmployee_click(object sender, RoutedEventArgs e)
        {
            string id = textBoxIdEmployee.Text;

            if (id.Length == 0)
            {
                MessageBox.Show("Проверьте данные");
            }
            else
            {
                DB db = new DB();
                db.openConnection(connStr);
                db.del_employee(id);
                db.closeConnection();
            }
        }
        //ORDERS
        private void allOrders_Click(object sender, RoutedEventArgs e)
        {
            string sqlExpression = "getAllOrders";

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();
                SqlDataAdapter command = new SqlDataAdapter(sqlExpression, connection);
                // указываем, что команда представляет хранимую процедуру
                Orders.Clear();
                // Заполняем Dataset
                command.Fill(Orders);
                // Отображаем данные
                ordersGrid.ItemsSource = Orders.DefaultView;
                connection.Close();
            }
        }
        

        private void addOrder_Click(object sender, RoutedEventArgs e)
        {
            int id_employee = Convert.ToInt32(textBoxIdEmployee_order.Text);
            int id_client = Convert.ToInt32(textBoxIdClient_order.Text);
     
            DB db = new DB();
            db.openConnection(connStr);
            db.add_order(id_employee, id_client);
            db.closeConnection();
        }
        //TODO
        private void addProductToOrder_Click(object sender, RoutedEventArgs e)
        {
            int id_order = Convert.ToInt32(textBoxIdOrder.Text);
            int id_product = Convert.ToInt32(textBoxIdProduct_order.Text);
            int count_product =Convert.ToInt32(textBoxCountProduct.Text);

            DB db = new DB();
            db.openConnection(connStr);
            db.add_product_to_order(id_order, id_product, count_product);
            db.closeConnection();
        }

        private void spisokOrders_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("spisok_orders", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@datestart", SqlDbType.Date).Value = DateStart.SelectedDate.Value;
                    cmd.Parameters.AddWithValue("@dateend", SqlDbType.Date).Value = DateEnd.SelectedDate.Value;
                    con.Open();
                    cmd.ExecuteNonQuery();

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    dataAdapter.Fill(dt);
                    ordersGrid.ItemsSource = dt.DefaultView;
                    dataAdapter.Update(dt);

                    con.Close();
                }

            }
        }
    }
}
