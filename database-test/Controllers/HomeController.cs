using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using database_test.Models;
using MySql.Data.MySqlClient;

namespace database_test.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<ParticipantModel> participants = new List<ParticipantModel>();
            string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM `participants`";
                using (MySqlCommand command = new MySqlCommand(query))
                {
                    command.Connection = connection;
                    connection.Open();
                    using (MySqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            participants.Add(new ParticipantModel
                            {
                                Id = Convert.ToInt32(dataReader["id"]),
                                Name = dataReader["name"].ToString(),
                                Surname = dataReader["surname"].ToString(),
                                //DateOfBirth = Convert.ToDateTime(dataReader["dateofbirth"]),
                                //DateOfBirth = dataReader["dateofbirth"].ToString(),
                                Email = dataReader["email"].ToString(),
                                Intolerances = dataReader["intolerances"].ToString(),
                                Discipline = dataReader["discipline_name"].ToString(),
                                Session = dataReader["session_name"].ToString()
                            });
                        }
                    }
                    connection.Close();
                }
            }

            return View(participants);
        }

        public ActionResult Registrati(string uk)
        {
            if(String.IsNullOrWhiteSpace(uk))
            {

            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(ParticipantModel participant)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Registrati");
            }
            else
            {
                string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = "INSERT INTO `participants` Values(@Id, @Name, @Surname, @DateOfBirth, @Email, @Intolerances, @Discipline, @Session)";
                    using (MySqlCommand command = new MySqlCommand(query))
                    {
                        command.Connection = connection;

                        command.Parameters.Add("@Id", MySqlDbType.Int32).Value = participant.Id;
                        command.Parameters.Add("@Name", MySqlDbType.VarChar).Value = participant.Name;
                        command.Parameters.Add("@Surname", MySqlDbType.VarChar).Value = participant.Surname;
                        command.Parameters.Add("@DateOfBirth", MySqlDbType.DateTime).Value = participant.DateOfBirth;
                        command.Parameters.Add("@Email", MySqlDbType.VarChar).Value = participant.Email;
                        command.Parameters.Add("@Intolerances", MySqlDbType.VarChar).Value = participant.Intolerances;
                        command.Parameters.Add("@Discipline", MySqlDbType.VarChar).Value = participant.Discipline;
                        command.Parameters.Add("@Session", MySqlDbType.VarChar).Value = participant.Session;

                        connection.Open();
                        MySqlDataReader dataReader = command.ExecuteReader();
                        connection.Close();
                    }
                }

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(int id)
        {
            ParticipantModel participant = new ParticipantModel();

            string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT * from `participants` WHERE `id` = @Id";
                using (MySqlCommand command = new MySqlCommand(query))
                {
                    command.Connection = connection;

                    command.Parameters.Add("@Id", MySqlDbType.Int32).Value = id;

                    connection.Open();
                    MySqlDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while(dataReader.Read())
                        {
                            participant.Id = dataReader.GetInt32(0);
                            participant.Name = dataReader.GetString(1);
                            participant.Surname = dataReader.GetString(2);
                            if (participant.DateOfBirth != null)
                            {
                                participant.DateOfBirth = dataReader.GetDateTime(3);
                            }
                            participant.Email = dataReader.GetString(4);
                            participant.Intolerances = dataReader.GetString(5);
                            if (participant.Discipline != null)
                            {
                                participant.Discipline = dataReader.GetString(6);
                            }
                            participant.Session = dataReader.GetString(7);
                        }
                    }

                    connection.Close();
                }
            }

            if (participant == null || participant.Id < 0) return RedirectToAction("Index");

            return View(participant);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ParticipantModel participant)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Edit", new { id = participant.Id });
            }
            else
            {
                string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = "UPDATE `participants` SET `name` = @Name, `surname` = @Surname, `dateofbirth` = @DateOfBirth, `email` = @Email, `intolerances` = @Intolerances, `discipline_name` = @Discipline, `session_name` = @Session WHERE `id` = @Id";
                    using (MySqlCommand command = new MySqlCommand(query))
                    {
                        command.Connection = connection;

                        command.Parameters.Add("@Id", MySqlDbType.Int32).Value = participant.Id;
                        command.Parameters.Add("@Name", MySqlDbType.VarChar).Value = participant.Name;
                        command.Parameters.Add("@Surname", MySqlDbType.VarChar).Value = participant.Surname;
                        command.Parameters.Add("@DateOfBirth", MySqlDbType.DateTime).Value = participant.DateOfBirth;
                        command.Parameters.Add("@Email", MySqlDbType.VarChar).Value = participant.Email;
                        command.Parameters.Add("@Intolerances", MySqlDbType.VarChar).Value = participant.Intolerances;
                        command.Parameters.Add("@Discipline", MySqlDbType.VarChar).Value = participant.Discipline;
                        command.Parameters.Add("@Session", MySqlDbType.VarChar).Value = participant.Session;

                        connection.Open();
                        MySqlDataReader dataReader = command.ExecuteReader();
                        connection.Close();
                    }
                }
                return RedirectToAction("Index");
            }
        }

        public ActionResult Detail(int id)
        {
            ParticipantModel participant = new ParticipantModel();

            string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT * from `participants` WHERE `id` = @Id";
                using (MySqlCommand command = new MySqlCommand(query))
                {
                    command.Connection = connection;

                    command.Parameters.Add("@Id", MySqlDbType.Int32).Value = id;

                    connection.Open();
                    MySqlDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            participant.Id = dataReader.GetInt32(0);
                            participant.Name = dataReader.GetString(1);
                            participant.Surname = dataReader.GetString(2);
                            if (participant.DateOfBirth != null)
                            {
                                participant.DateOfBirth = dataReader.GetDateTime(3);
                            }
                            participant.Email = dataReader.GetString(4);
                            participant.Intolerances = dataReader.GetString(5);
                            if(participant.Discipline != null)
                            {
                                participant.Discipline = dataReader.GetString(6);
                            }
                            participant.Session = dataReader.GetString(7);
                        }
                    }

                    connection.Close();
                }
            }

            if (participant == null || participant.Id < 0) return RedirectToAction("Index");

            return View(participant);
        }

        public ActionResult Delete(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "DELETE from `participants` WHERE `id` = @Id";
                using (MySqlCommand command = new MySqlCommand(query))
                {
                    command.Connection = connection;

                    command.Parameters.Add("@Id", MySqlDbType.Int32).Value = id;

                    connection.Open();
                    MySqlDataReader dataReader = command.ExecuteReader();
                    connection.Close();
                }
            }

            return RedirectToAction("Index");
        }
    }
}