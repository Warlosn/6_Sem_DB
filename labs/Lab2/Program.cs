using Lab2.classes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security;
using Oracle.DataAccess.Client;
using Microsoft.Data.Sqlite;


namespace Lab2
{
    class Program
    {
        static List<users> users = new List<users>();
        static List<trip> trips = new List<trip>();
        static List<booking> bookings = new List<booking>();
        static List<room_hotel> rooms = new List<room_hotel>();
        static List<book_hotel> hotels = new List<book_hotel>();

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
                Console.WriteLine("Choose table:\n1. users\n2. trip\n3. book_hotel\n4. room_hotel\n5. booking\n6. to exit");
                value = Int32.Parse(Console.ReadLine());
                switch (value)
                {
                    case 1:
                        userMethods(connection);
                        break;
                    case 2:
                        tripMethods(connection);
                        break;
                    case 3:
                        hotelMethods(connection);
                        break;
                    case 4:
                        roomMethods(connection);
                        break;
                    case 5:
                        bookingMethods(connection);
                        break;
                    case 6:
                        return true;
                    default:
                        continue;
                }
            }
        }
        static void userMethods(SqliteConnection connection)
        {
            string method = "";
            while (!method.Equals("exit"))
            {
                method = ChooseMethod();
                switch (method)
                {
                    case "select":
                        users = selectUser(connection);
                        foreach (users item in users)
                        {
                            Console.Write("----------------------------------------\n");
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write(item.ToString());
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        Console.Write("----------------------------------------\n");
                        break;
                    case "insert":
                        Console.WriteLine("Write insert user: fullname/email/password/passport/visa");
                        string[] str = Console.ReadLine().Split('/');
                        insertUser(connection, new users(str[0], str[1], str[2], str[3], str[4]));
                        break;
                    case "update":
                        Console.WriteLine("Write update user: id/fullname/email/password/passport/visa");
                        string[] upd = Console.ReadLine().Split('/');
                        updateUser(connection, new users(Convert.ToInt32(upd[0]), upd[1], upd[2], upd[3], upd[4], upd[5]));
                        break;
                    case "delete":
                        Console.WriteLine("Write delete user: ");
                        string del = Console.ReadLine();
                        deleteUser(connection, Convert.ToInt32(del));
                        break;
                    default:
                        continue;
                }
            }
        }
        static void tripMethods(SqliteConnection connection)
        {
            string method = "";
            while (!method.Equals("exit"))
            {
                method = ChooseMethod();
                switch (method)
                {
                    case "select":
                        trips = selectTrip(connection);
                        foreach (trip item in trips)
                        {
                            Console.Write("----------------------------------------\n");
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write(item.ToString());
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        Console.Write("----------------------------------------\n");
                        break;
                    case "insert":
                        Console.WriteLine("Write value trip: from/to/numb_people/date_start/date_finish/transport/duration/cost");
                        string[] str = Console.ReadLine().Split('/');
                        insertTrip(connection, new trip(str[0], str[1], Convert.ToByte(str[2]), str[3], str[4], str[5], str[6], Convert.ToDouble(str[7])));
                        break;
                    case "update":
                        Console.WriteLine("Write value trip: id/from/to/numb_people/date_start/date_finish/transport/duration/cost");
                        string[] upd = Console.ReadLine().Split('/');
                        updateTrip(connection, new trip(Convert.ToInt32(upd[0]), upd[1], upd[2], Convert.ToByte(upd[3]), upd[4], upd[5], upd[6], upd[7], Convert.ToDouble(upd[8])));
                        break;
                    case "delete":
                        Console.WriteLine("Write id delete trip: ");
                        string del = Console.ReadLine();
                        deleteTrip(connection, Convert.ToInt32(del));
                        break;
                    case "select_trips":
                        Console.Write("Write country to: ");
                        string country = Console.ReadLine();
                        trips = select_CountryTrips(connection, country);
                        foreach (trip item in trips)
                        {
                            Console.Write("----------------------------------------\n");
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write(item.ToString());
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        Console.Write("----------------------------------------\n");
                        break;
                    default:
                        continue;
                }
            }
        }
        static void hotelMethods(SqliteConnection connection)
        {
            string method = "";
            while (!method.Equals("exit"))
            {
                method = ChooseMethod();
                switch (method)
                {
                    case "select":
                        hotels = selectHotels(connection);
                        foreach (book_hotel item in hotels)
                        {
                            Console.Write("----------------------------------------\n");
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write(item.ToString());
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        Console.Write("----------------------------------------\n");
                        break;
                    case "insert":
                        Console.WriteLine("Write insert hotel: name/stars/address/date_start/date_finish");
                        string[] str = Console.ReadLine().Split('/');
                        insertHotel(connection, new book_hotel(str[0], Convert.ToByte(str[1]), str[2], str[3], str[4]));
                        break;
                    case "update":
                        Console.WriteLine("Write update hotel: id/name/stars/address/date_start/date_finish");
                        string[] upd = Console.ReadLine().Split('/');
                        updateHotel(connection, new book_hotel(Convert.ToInt32(upd[0]), upd[1], Convert.ToByte(upd[2]), upd[3], upd[4], upd[5]));
                        break;
                    case "delete":
                        Console.WriteLine("Write id delete hotel: ");
                        string del = Console.ReadLine();
                        deleteHotel(connection, Convert.ToInt32(del));
                        break;
                    default:
                        continue;
                }
            }
        }
        static void roomMethods(SqliteConnection connection)
        {
            string method = "";
            while (!method.Equals("exit"))
            {
                method = ChooseMethod();
                switch (method)
                {
                    case "select":
                        rooms = selectRooms(connection);
                        foreach (room_hotel item in rooms)
                        {
                            Console.Write("----------------------------------------\n");
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write(item.ToString());
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        Console.Write("----------------------------------------\n");
                        break;
                    case "insert":
                        Console.WriteLine("Write value trip: id_hotel/number/is_free/cost/room_type");
                        string[] str = Console.ReadLine().Split('/');
                        insertRoom(connection, new room_hotel(Convert.ToInt32(str[0]), Convert.ToInt32(str[1]), Convert.ToBoolean(str[2]), Convert.ToDouble(str[3]), str[4]));
                        break;
                    case "update":
                        Console.WriteLine("Write value hotel: name/stars/address/date_start/date_finish");
                        string[] upd = Console.ReadLine().Split('/');
                        updateRoom(connection, new room_hotel(Convert.ToInt32(upd[0]), Convert.ToInt32(upd[1]), Convert.ToByte(upd[2]), Convert.ToBoolean(upd[3]), Convert.ToInt32(upd[4]), upd[5]));
                        break;
                    case "delete":
                        Console.WriteLine("Write id delete room: ");
                        string del = Console.ReadLine();
                        deleteRoom(connection, Convert.ToInt32(del));
                        break;
                    default:
                        continue;
                }
            }
        }
        static void bookingMethods(SqliteConnection connection)
        {
            string method = "";
            while (!method.Equals("exit"))
            {
                method = ChooseMethod();
                switch (method)
                {
                    case "select":
                        bookings = selectBooking(connection);
                        foreach (booking item in bookings)
                        {
                            Console.Write("----------------------------------------\n");
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write(item.ToString());
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        Console.Write("----------------------------------------\n");
                        break;
                    case "insert":
                        Console.WriteLine("Write booking: id_trip/id_user/id_hotel");
                        string[] str = Console.ReadLine().Split('/');
                        insertBooking(connection, new booking(Convert.ToInt32(str[0]), Convert.ToInt32(str[1]), Convert.ToInt32(str[2])));
                        break;
                    case "update":
                        Console.WriteLine("Write booking: id_trip/id_user/id_hotel");
                        string[] upd = Console.ReadLine().Split('/');
                        updateBooking(connection, new booking(Convert.ToInt32(upd[0]), Convert.ToInt32(upd[1]), Convert.ToInt32(upd[2], Convert.ToInt32(upd[3]))));
                        break;
                    case "delete":
                        Console.WriteLine("Write id delete booking: ");
                        string del = Console.ReadLine();
                        deleteBooking(connection, Convert.ToInt32(del));
                        break;
                    default:
                        continue;
                }
            }
        }

        static List<users> selectUser(SqliteConnection connection)
        {
            List<users> list = new List<users>();
            string expression = "select * from users";
            SqliteCommand command = new SqliteCommand(expression, connection);
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    users users = new users();
                    users.id = reader.GetInt32(0);
                    users.fullname = reader.GetString(1);
                    users.email = reader.GetString(2);
                    users.password = reader.GetString(3);
                    users.passports_No = reader.GetString(4);
                    users.visa = reader.GetString(5);
                    list.Add(users);
                }
            }
            return list;
        }
        static List<trip> selectTrip(SqliteConnection connection)
        {
            List<trip> list = new List<trip>();
            string expression = "select * from trip";
            SqliteCommand command = new SqliteCommand(expression, connection);
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    trip trip = new trip();
                    trip.id = reader.GetInt32(0);
                    trip.from_county = reader.GetString(1);
                    trip.to_county = reader.GetString(2);
                    trip.numb_people = reader.GetByte(3);
                    trip.date_start = reader.GetDateTime(4).ToString();
                    trip.date_finish = reader.GetDateTime(5).ToString();
                    trip.transport = reader.GetString(6);
                    trip.duration = reader.GetTimeSpan(7).ToString();
                    trip.cost = reader.GetInt64(8);
                    list.Add(trip);
                }
            }
            return list;
        }
        static List<book_hotel> selectHotels(SqliteConnection connection)
        {
            List<book_hotel> list = new List<book_hotel>();
            string expression = "select * from book_hotel";
            SqliteCommand command = new SqliteCommand(expression, connection);
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    book_hotel hotel = new book_hotel();
                    hotel.id = reader.GetInt32(0);
                    hotel.name = reader.GetString(1);
                    hotel.stars = reader.GetByte(2);
                    hotel.address = reader.GetString(3);
                    hotel.date_start = reader.GetDateTime(4).ToString();
                    hotel.date_finish = reader.GetDateTime(5).ToString();
                    list.Add(hotel);
                }
            }
            return list;
        }
        static List<booking> selectBooking(SqliteConnection connection)
        {
            List<booking> list = new List<booking>();
            string expression = "select * from booking";
            SqliteCommand command = new SqliteCommand(expression, connection);
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    booking booking = new booking();
                    booking.id = reader.GetInt32(0);
                    booking.id_trip = reader.GetInt32(1);
                    booking.id_user = reader.GetInt32(2);
                    booking.id_hotel = reader.GetInt32(3);
                    list.Add(booking);
                }
            }
            return list;
        }
        static List<room_hotel> selectRooms(SqliteConnection connection)
        {
            List<room_hotel> list = new List<room_hotel>();
            string expression = "select * from room_hotel";
            SqliteCommand command = new SqliteCommand(expression, connection);
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    room_hotel room = new room_hotel();
                    room.id = reader.GetInt32(0);
                    room.id_hotel = reader.GetInt32(1);
                    room.number = reader.GetInt32(2);
                    room.is_free = reader.GetBoolean(3);
                    room.cost = reader.GetInt64(4);
                    room.room_type = reader.GetString(5);
                    list.Add(room);
                }
            }
            return list;
        }

        static void insertUser(SqliteConnection connection, users user)
        {
            string query = "insert into users(fullname, email, password, passports_No, visa) " +
                "values(@fullname, @email, @password, @passport, @visa)";
            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@fullname", user.fullname));
            command.Parameters.Add(new SqliteParameter("@email", user.email));
            command.Parameters.Add(new SqliteParameter("@password", user.password));
            command.Parameters.Add(new SqliteParameter("@passport", user.passports_No));
            command.Parameters.Add(new SqliteParameter("@visa", user.visa));
            int result = command.ExecuteNonQuery();
            Console.WriteLine($"Added {result} rows");
        }
        static void insertTrip(SqliteConnection connection, trip trip)
        {
            string query = "insert into trip(from_country, to_country, numb_people, date_start, date_finish, transport, duration, cost) " +
                "values(@from_country, @to_country, @numb_people, @date_start, @date_finish, @transport, @duration, @cost)";
            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@from_country", trip.from_county));
            command.Parameters.Add(new SqliteParameter("@to_country", trip.to_county));
            command.Parameters.Add(new SqliteParameter("@numb_people", trip.numb_people));
            command.Parameters.Add(new SqliteParameter("@date_start", trip.date_start));
            command.Parameters.Add(new SqliteParameter("@date_finish", trip.date_finish));
            command.Parameters.Add(new SqliteParameter("@transport", trip.transport));
            command.Parameters.Add(new SqliteParameter("@duration", trip.duration));
            command.Parameters.Add(new SqliteParameter("@cost", trip.cost));
            int result = command.ExecuteNonQuery();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"Added {result} rows");
            Console.ForegroundColor = ConsoleColor.White;

        }
        static void insertHotel(SqliteConnection connection, book_hotel hotel)
        {
            string query = "insert into book_hotel(name, stars, address, date_start, date_finish) " +
                "values(@name, @stars, @address, @date_start, @date_finish)";
            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@name", hotel.name));
            command.Parameters.Add(new SqliteParameter("@stars", hotel.stars));
            command.Parameters.Add(new SqliteParameter("@address", hotel.address));
            command.Parameters.Add(new SqliteParameter("@date_start", hotel.date_start));
            command.Parameters.Add(new SqliteParameter("@date_finish", hotel.date_finish));
            int result = command.ExecuteNonQuery();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"Added {result} rows");
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void insertBooking(SqliteConnection connection, booking booking)
        {
            string query = "insert into booking(id_trip, id_user, id_hotel) " +
                "values(@id_trip, @id_user, @id_hotel)";
            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@id_trip", booking.id_trip));
            command.Parameters.Add(new SqliteParameter("@id_user", booking.id_user));
            command.Parameters.Add(new SqliteParameter("@id_hotel", booking.id_hotel));
            int result = command.ExecuteNonQuery();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"Added {result} rows");
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void insertRoom(SqliteConnection connection, room_hotel room)
        {
            string query = "insert into room_hotel(id_hotel, number, is_free, cost, room_type) " +
                "values(@id_hotel, @number, @is_free, @cost, @room_type)";
            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@id_hotel", room.id_hotel));
            command.Parameters.Add(new SqliteParameter("@number", room.number));
            command.Parameters.Add(new SqliteParameter("@is_free", room.is_free));
            command.Parameters.Add(new SqliteParameter("@cost", room.cost));
            command.Parameters.Add(new SqliteParameter("@room_type", room.room_type));
            int result = command.ExecuteNonQuery();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"Added {result} rows");
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void updateUser(SqliteConnection connection, users user)
        {
            string query = "update users set fullname = @fullname, email = @email, password = @password, passports_No = @passport, visa = @visa " +
                " where id = @id";
            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@id", user.id));
            command.Parameters.Add(new SqliteParameter("@fullname", user.fullname));
            command.Parameters.Add(new SqliteParameter("@email", user.email));
            command.Parameters.Add(new SqliteParameter("@password", user.password));
            command.Parameters.Add(new SqliteParameter("@passport", user.passports_No));
            command.Parameters.Add(new SqliteParameter("@visa", user.visa));
            int result = command.ExecuteNonQuery();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"Updated {result} rows");
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void updateTrip(SqliteConnection connection, trip trip)
        {
            string query = "update trip set from_country = @from, to_country = @to, numb_people = @numb, date_start= @start," +
                " date_finish = @finish, transport = @transport, duration = @duration, cost = @cost " +
                " where id = @id";
            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@id", trip.id));
            command.Parameters.Add(new SqliteParameter("@from", trip.from_county));
            command.Parameters.Add(new SqliteParameter("@to", trip.to_county));
            command.Parameters.Add(new SqliteParameter("@numb", trip.numb_people));
            command.Parameters.Add(new SqliteParameter("@start", trip.date_start));
            command.Parameters.Add(new SqliteParameter("@finish", trip.date_finish));
            command.Parameters.Add(new SqliteParameter("@transport", trip.transport));
            command.Parameters.Add(new SqliteParameter("@duration", trip.duration));
            command.Parameters.Add(new SqliteParameter("@cost", trip.cost));
            int result = command.ExecuteNonQuery();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"Updated {result} rows");
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void updateHotel(SqliteConnection connection, book_hotel hotel)
        {
            string query = "update book_hotel set name = @name, stars= @stars, address= @address, date_start= @date_start, date_finish= @date_finish " +
                " where id = @id";
            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@id", hotel.id));
            command.Parameters.Add(new SqliteParameter("@name", hotel.name));
            command.Parameters.Add(new SqliteParameter("@stars", hotel.stars));
            command.Parameters.Add(new SqliteParameter("@address", hotel.address));
            command.Parameters.Add(new SqliteParameter("@date_start", hotel.date_start));
            command.Parameters.Add(new SqliteParameter("@date_finish", hotel.date_finish));
            int result = command.ExecuteNonQuery();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"Updated {result} rows");
            Console.ForegroundColor = ConsoleColor.White;

        }
        static void updateBooking(SqliteConnection connection, booking booking)
        {
            string query = "update booking set id_hotel = @id_hotel, id_trip = @id_trip, id_user= @id_user " +
                " where id = @id";
            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@id", booking.id));
            command.Parameters.Add(new SqliteParameter("@id_hotel", booking.id_hotel));
            command.Parameters.Add(new SqliteParameter("@id_trip", booking.id_trip));
            command.Parameters.Add(new SqliteParameter("@id_user", booking.id_user));
            int result = command.ExecuteNonQuery();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"Updated {result} rows");
            Console.ForegroundColor = ConsoleColor.White;

        }
        static void updateRoom(SqliteConnection connection, room_hotel room)
        {
            string query = "update room_hotel set id_hotel = @id_hotel, numebr = @number, is_free = @is_free, cost= @cost, room_type = @room_type " +
                " where id = @id";
            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@id", room.id));
            command.Parameters.Add(new SqliteParameter("@id_hotel", room.id_hotel));
            command.Parameters.Add(new SqliteParameter("@number", room.number));
            command.Parameters.Add(new SqliteParameter("@is_free", room.is_free));
            command.Parameters.Add(new SqliteParameter("@cost", room.cost));
            command.Parameters.Add(new SqliteParameter("@room_type", room.room_type));
            int result = command.ExecuteNonQuery();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"Updated {result} rows");
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void deleteUser(SqliteConnection conneciton, int id)
        {
            string query = "delete from users where id = @id";
            SqliteCommand command = new SqliteCommand(query, conneciton);
            command.Parameters.Add(new SqliteParameter("@id", id));
            int result = command.ExecuteNonQuery();
            Console.WriteLine($"Deleted {result} rows");
        }
        static void deleteTrip(SqliteConnection conneciton, int id)
        {
            string query = "delete from trip where id = @id";
            SqliteCommand command = new SqliteCommand(query, conneciton);
            command.Parameters.Add(new SqliteParameter("@id", id));
            int result = command.ExecuteNonQuery();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"Deleted {result} rows");
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void deleteHotel(SqliteConnection conneciton, int id)
        {
            string query = "delete from book_hotel where id = @id";
            SqliteCommand command = new SqliteCommand(query, conneciton);
            command.Parameters.Add(new SqliteParameter("@id", id));
            int result = command.ExecuteNonQuery();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"Deleted {result} rows");
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void deleteRoom(SqliteConnection conneciton, int id)
        {
            string query = "delete from room_hotel where id = @id";
            SqliteCommand command = new SqliteCommand(query, conneciton);
            command.Parameters.Add(new SqliteParameter("@id", id));
            int result = command.ExecuteNonQuery();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"Deleted {result} rows");
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void deleteBooking(SqliteConnection conneciton, int id)
        {
            string query = "delete from booking where id = @id";
            SqliteCommand command = new SqliteCommand(query, conneciton);
            command.Parameters.Add(new SqliteParameter("@id", id));
            int result = command.ExecuteNonQuery();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"Deleted {result} rows");
            Console.ForegroundColor = ConsoleColor.White;
        }
        static List<trip> select_CountryTrips(SqliteConnection connection, string country)
        {
            List<trip> list = new List<trip>();
            string expression = "select * from trip where to_country like @country";
            SqliteCommand command = new SqliteCommand(expression, connection);
            command.Parameters.AddWithValue("@country", "%" + country + "%");
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    trip trip = new trip();
                    trip.id = reader.GetInt32(0);
                    trip.from_county = reader.GetString(1);
                    trip.to_county = reader.GetString(2);
                    trip.numb_people = reader.GetByte(3);
                    trip.date_start = reader.GetDateTime(4).ToString();
                    trip.date_finish = reader.GetDateTime(5).ToString();
                    trip.transport = reader.GetString(6);
                    trip.duration = reader.GetDateTime(7).ToString();
                    trip.cost = reader.GetInt64(8);
                    list.Add(trip);
                }
            }
            return list;
        }
        static string ChooseMethod()
        {
            Console.WriteLine("Choose method(write method):\n1. select\n2. insert\n3. update\n4. delete\n5. select_trips\n6. exit");
            return Console.ReadLine().ToLower();
        }
    }
}