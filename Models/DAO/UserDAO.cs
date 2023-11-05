using System;
using System.Collections.Generic;
using Examen.Models.DTO;
using MySql.Data.MySqlClient;

namespace Examen.Models.DAO
{
    public class UserDAO
    {
        /// <summary>
        /// CRUD
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string CreateUser(UserDTO user)
        {
            string response = "Failed";

            try
            {
                using (MySqlConnection connection = SecurityConfig.GetConnection())
                {
                    connection.Open();

                    string createUserQuery = "INSERT INTO Users (name, email) VALUES (@pName, @pEmail)";
                    using (MySqlCommand command = new MySqlCommand(createUserQuery, connection))
                    {
                        command.Parameters.AddWithValue("@pName", user.Name);
                        command.Parameters.AddWithValue("@pEmail", user.Email);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0) response = "Success";

                    }

                    connection.Close();
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Error in CreateUser: " + error.Message);
            }
            return response;
        }
        public List<UserDTO> ReadUsers()
        {
            List<UserDTO> users = new List<UserDTO>();
            try
            {
                using (MySqlConnection connection = SecurityConfig.GetConnection())
                {
                    connection.Open();
                    string readUsersQuery = "SELECT * FROM Users";
                    using (MySqlCommand command = new MySqlCommand(readUsersQuery, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                UserDTO user = new UserDTO();
                                user.Id = reader.GetInt32("id");
                                user.Name = reader.GetString("name");
                                user.Email = reader.GetString("email");
                                users.Add(user);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Error in ReadUsers: " + error.Message);
            }

            return users;
        }
        public UserDTO ReadUser(int id)
        {
            UserDTO user = new UserDTO();
            try
            {
                using (MySqlConnection connection = SecurityConfig.GetConnection())
                {
                    connection.Open();
                    string readUsersQuery = "SELECT * FROM Users WHERE id = @pID";
                    using (MySqlCommand command = new MySqlCommand(readUsersQuery, connection))
                    {
                        command.Parameters.AddWithValue("@pId", id);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                user.Id = reader.GetInt32("id");
                                user.Name = reader.GetString("name");
                                user.Email = reader.GetString("email");
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Error in ReadUser: " + error.Message);
            }

            return user;
        }
        public string UpdateUser(int id, UserDTO user)
        {
            string response = "Failed";

            try
            {
                using (MySqlConnection connection = SecurityConfig.GetConnection())
                {
                    connection.Open();

                    string updateUserQuery = "UPDATE Users SET name = @pName, email = @pEmail WHERE id = @pId";
                    using (MySqlCommand command = new MySqlCommand(updateUserQuery, connection))
                    {
                        command.Parameters.AddWithValue("@pId", id);
                        command.Parameters.AddWithValue("@pName", user.Name);
                        command.Parameters.AddWithValue("@pEmail", user.Email);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            response = "Success";
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Error in UpdateUser: " + error.Message);
            }

            return response;
        }
        public string DeleteUser(int id)
        {
            string response = "Failed";

            try
            {
                using (MySqlConnection connection = SecurityConfig.GetConnection())
                {
                    connection.Open();

                    string deleteUserQuery = "DELETE FROM Users WHERE id = @pId";
                    using (MySqlCommand command = new MySqlCommand(deleteUserQuery, connection))
                    {
                        command.Parameters.AddWithValue("@pId", id);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            response = "Success";
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Error in DeleteUser: " + error.Message);
            }

            return response;
        }
    }
}