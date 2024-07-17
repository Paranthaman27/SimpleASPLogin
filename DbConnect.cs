//using System;
//using Google.Protobuf.WellKnownTypes;
//using System.Xml.Linq;
//using MySql.Data.MySqlClient;
//using SimpleASPLogin.Models;
//using static System.Runtime.InteropServices.JavaScript.JSType;

//namespace SimpleASPLogin
//{
//	public class DbConnect
//	{
//        string connectionString = "server=localhost;userid=root;password=Noone1903;database=transport";
//        static MySqlConnection? connection;
//        public  void Register(User user)
//        {
//            using (connection = new MySqlConnection(connectionString))
//            {
//                connection.Open();
//                string query = "INSERT INTO Login Values(@value1,@value2,@value3,@value4)";

//                using (MySqlCommand cmd = new MySqlCommand(query, connection))
//                {
//                    cmd.Parameters.AddWithValue("@value1", user.id);
//                    cmd.Parameters.AddWithValue("@value2", user.Username);
//                    cmd.Parameters.AddWithValue("@value3", user.Email);
//                    cmd.Parameters.AddWithValue("@value4", user.Password);

//                    cmd.ExecuteNonQuery();


//                }

//            }
//        }
        
//        public bool Login(User user)
//        {
//            string pass=string.Empty;
//            using (connection = new MySqlConnection(connectionString))
//            {
//                connection.Open();
//                string query = "SELECT password FROM login WHERE id = @Value1";
                
//                using (MySqlCommand cmd = new MySqlCommand(query, connection))
//                {
//                    cmd.Parameters.AddWithValue("@value1", user.id);
//                    using var Reader = cmd.ExecuteReader();
//                    while (Reader.Read())
//                    {
//                        pass = Reader.GetString("password");

//                    }
//                }
//            }
//            return user.Password.Equals(pass);
//        }

//        public void AddDriver(Driver driver)
//        {
//            using (connection = new MySqlConnection(connectionString))
//            {
//                connection.Open();
//                string query1 = "INSERT INTO Driver Values(@value2,@value3,@value4,@value5)";

//                using (MySqlCommand cmd = new MySqlCommand(query1, connection))
//                {
//                    ;
//                    cmd.Parameters.AddWithValue("@value2", driver.D_Name);
//                    cmd.Parameters.AddWithValue("@value3", driver.D_DOB);
//                    cmd.Parameters.AddWithValue("@value4", driver.D_Pno);
//                    cmd.Parameters.AddWithValue("@value5", driver.DAddress);

//                    cmd.ExecuteNonQuery();


//                }

//            }
//        }
//        public List<Driver> GetDrivers()
//        {
//            List<Driver> drivers = new List<Driver>();

//            using (var connection = new MySqlConnection(connectionString))
//            {
//                connection.Open();
//                string query = "SELECT * FROM Driver";

//                using (var command = new MySqlCommand(query, connection))
//                {
//                    using (var reader = command.ExecuteReader())
//                    {
//                        while (reader.Read())
//                        {
//                            Driver driver = new Driver
//                            {
//                                Driver_id = Convert.ToInt32(reader["Driver_id"]),
//                                D_Name = reader["D_Name"].ToString(),
//                                D_DOB = Convert.ToDateTime(reader["D_DOB"]),
//                                D_Pno = reader["D_Pno"].ToString(),
//                                DAddress = reader["DAddress"].ToString()
//                            };
//                            drivers.Add(driver);
//                        }
//                    }
//                }
//            }

//            return drivers;
//        }
//        public void UpdateDriver(Driver driver)
//        {
//            using (var connection = new MySqlConnection(connectionString))
//            {
//                connection.Open();
//                string query = "UPDATE Driver SET D_Name=@D_Name, D_DOB=@D_DOB, D_Pno=@D_Pno, DAddress=@DAddress WHERE Email=@DEmail";

//                using (var command = new MySqlCommand(query, connection))
//                {
//                    command.Parameters.AddWithValue("@Driver_id", driver.Email);
//                    command.Parameters.AddWithValue("@D_Name", driver.D_Name);
//                    command.Parameters.AddWithValue("@D_DOB", driver.D_DOB);
//                    command.Parameters.AddWithValue("@D_Pno", driver.D_Pno);
//                    command.Parameters.AddWithValue("@DAddress", driver.DAddress);

//                    command.ExecuteNonQuery();
//                }
//            }
//        }

//        public void DeleteDriver(int id)
//        {
//            using (var connection = new MySqlConnection(connectionString))
//            {
//                connection.Open();
//                string query = "DELETE FROM Driver WHERE Driver_id=@Driver_id";

//                using (var command = new MySqlCommand(query, connection))
//                {
//                    command.Parameters.AddWithValue("@Driver_id", id);

//                    command.ExecuteNonQuery();
//                }
//            }
//        }
//        public void AddVehicle(Vehicle vehicle)
//        {
//            using (var connection = new MySqlConnection(connectionString))
//            {
//                connection.Open();
//                string query = "INSERT INTO Vehicle (Vehicle_id, Vehicle_Name, Vehicle_Reg, V_FC, V_Insc) " +
//                               "VALUES (@Vehicle_id, @Vehicle_Name, @Vehicle_Reg, @V_FC, @V_Insc)";

//                using (var command = new MySqlCommand(query, connection))
//                {
//                    command.Parameters.AddWithValue("@Vehicle_id", vehicle.Vehicle_id);
//                    command.Parameters.AddWithValue("@Vehicle_Name", vehicle.Vehicle_Name);
//                    command.Parameters.AddWithValue("@Vehicle_Reg", vehicle.Vehicle_Reg);
//                    command.Parameters.AddWithValue("@V_FC", vehicle.V_FC);
//                    command.Parameters.AddWithValue("@V_Insc", vehicle.V_Insc);

//                    command.ExecuteNonQuery();
//                }
//            }
//        }

//        public List<Vehicle> GetVehicles()
//        {
//            List<Vehicle> vehicles
//                = new List<Vehicle>();

//            using (var connection = new MySqlConnection(connectionString))
//            {
//                connection.Open();
//                string query = "SELECT * FROM Vehicle";

//                using (var command = new MySqlCommand(query, connection))
//                {
//                    using (var reader = command.ExecuteReader())
//                    {
//                        while (reader.Read())
//                        {
//                            Vehicle vehicle = new Vehicle
//                            {
//                                Vehicle_id = Convert.ToInt32(reader["Vehicle_id"]),
//                                Vehicle_Name = reader["Vehicle_Name"].ToString(),
//                                Vehicle_Reg = reader["Vehicle_Reg"].ToString(),
//                                V_FC = Convert.ToDateTime(reader["V_FC"]),
//                                V_Insc = Convert.ToDateTime(reader["V_Insc"]),
                                
                                
//                            };
//                            vehicles.Add(vehicle);
//                        }
//                    }
//                }
//            }

//            return vehicles;
//        }

//        public void UpdateVehicle(Vehicle vehicle)
//        {
//            using (var connection = new MySqlConnection(connectionString))
//            {
//                connection.Open();
//                string query = "UPDATE Vehicle SET Vehicle_Name=@Vehicle_Name, " +
//                               "Vehicle_Reg=@Vehicle_Reg, V_FC=@V_FC, V_Insc=@V_Insc WHERE Vehicle_id=@Vehicle_id";

//                using (var command = new MySqlCommand(query, connection))
//                {
//                    command.Parameters.AddWithValue("@Vehicle_id", vehicle.Vehicle_id);
//                    command.Parameters.AddWithValue("@Vehicle_Name", vehicle.Vehicle_Name);
//                    command.Parameters.AddWithValue("@Vehicle_Reg", vehicle.Vehicle_Reg);
//                    command.Parameters.AddWithValue("@V_FC", vehicle.V_FC);
//                    command.Parameters.AddWithValue("@V_Insc", vehicle.V_Insc);

//                    command.ExecuteNonQuery();
//                }
//            }
//        }

//        public void DeleteVehicle(int id)
//        {
//            using (var connection = new MySqlConnection(connectionString))
//            {
//                connection.Open();
//                string query = "DELETE FROM Vehicle WHERE Vehicle_id=@Vehicle_id";

//                using (var command = new MySqlCommand(query, connection))
//                {
//                    command.Parameters.AddWithValue("@Vehicle_id", id);

//                    command.ExecuteNonQuery();
//                }
//            }
//        }


//    }
//}

