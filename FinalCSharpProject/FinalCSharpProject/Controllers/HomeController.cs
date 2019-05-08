using FinalCSharpProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalCSharpProject.Controllers
{
    public class HomeController : Controller
    {
  
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Calculate(string firstName, string lastName, string emailAddress, DateTime dateOfBirth, int carYear, string carMake,
            string carModel, string DUI, int speedingTickets, string fullOrLiability) // if (text.tolower(Console.ReadLine()) = "liability" charge x 
        { // lATER ON NEED TO CHANGE FULLORLIABILTY AND DUI TO BOOLS OR SOMETHING, DONT KNOW HOW TO USE THEM NOW
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(emailAddress) || string.IsNullOrEmpty(carMake) || 
                string.IsNullOrEmpty(carModel))
                  //if bool .ischecked(smth like this) then (more money)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            else
            {
                using (InsuranceEntities db = new InsuranceEntities())
                {
                    var client = new Client();
                    client.FirstName = firstName;
                    client.LastName = lastName;
                    client.EmailAddress = emailAddress;
                    client.DateOfBirth = dateOfBirth;
                    client.CarYear = carYear;
                    client.CarMake = carMake;
                    client.CarModel = carModel;
                    client.DUI = DUI;
                    client.SpeedingTickets = speedingTickets;
                    client.FullOrLiability = fullOrLiability;

                    
                        int price = 50;
                        TimeSpan age = DateTime.Now.Subtract(client.DateOfBirth);
                        if (age.TotalDays < 9125) // 25 * 365
                        {
                            price = price + 25;
                        }
                        if (age.TotalDays < 6570)  //18 * 365 
                        {
                            price = price + 100; //uncertain slightly here, I am not sure if the user gets pegged twice for being under 26 and 18.
                        }
                        if (age.TotalDays > 36500)  // 100 * 365
                        {
                            price = price + 25;
                        }
                        if (client.CarYear < 2000) 
                        {
                            price = price + 25;
                        }
                        if (client.CarYear > 2015) 
                        {
                            price = price + 25;
                        }
                        if (client.CarMake == "Porsche")
                        {
                            price = price + 25;
                            if (client.CarModel == "911 Carrera")
                            {
                                price = price + 25;
                            }
                        }
                        if (client.SpeedingTickets > 0)
                        {
                            price = price + (client.SpeedingTickets * 10);
                        }
                        if (client.DUI == "yes")
                        {
                            price = (price * 5 / 4);
                        }
                        if (client.FullOrLiability == "full")
                        {
                            price = (price * 3 / 2);
                        }
                    client.Price = price;
                        db.Clients.Add(client);
                    db.SaveChanges();
                    return View("Quote");
                }

                   
            }
        }

        public ActionResult Admin()
        {

            using (InsuranceEntities db = new InsuranceEntities())
            {
                var clients = db.Clients;
                var clientVms = new List<ClientsVm>();
                foreach (var client in clients)
                {
                    var clientVm = new ClientsVm();
                    clientVm.FirstName = client.FirstName;
                    clientVm.LastName = client.LastName;
                    clientVm.EmailAddress = client.EmailAddress;
                    clientVms.Add(clientVm);
                }

                return View(clientVms);
            }


            //string queryString = @"SELECT Id, FirstName, LastName, EmailAddress, DateOfBirth,
            //                     CarYear, CarMake, CarModel, DUI, SpeedingTickets, FullOrLiability, from Clients"; //need to figure out how to put price into clients table
            //var clients = new List<InsuranceClients>();

            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    SqlCommand command = new SqlCommand(queryString, connection);

            //    connection.Open();

            //    SqlDataReader reader = command.ExecuteReader();

            //    while (reader.Read()) //this is giving me issues. I dont know how to make it so that 
            //        // it will display all of the clients in the database.
            //    {
            //        var client = new InsuranceClients();
            //        client.Id = Convert.ToInt32(reader["Id"]); 
            //        client.FirstName = reader["FirstName"].ToString();
            //        client.LastName = reader["LastName"].ToString();
            //        client.EmailAddress = reader["EmailAddress"].ToString();
            //        client.DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]); //got from stackoverflow lol
            //        client.CarYear = Convert.ToInt32(reader["CarYear"]);
            //        client.CarMake = reader["CarMake"].ToString();
            //        client.CarModel = reader["CarModel"].ToString();
            //        client.DUI = reader["DUI"].ToString();
            //        client.SpeedingTickets = Convert.ToInt32(reader["SpeedingTickets"]);
            //        client.FullOrLiability = reader["FullOrLiability"].ToString();

            //        clients.Add(client);
            //    }

            //}




        }
        public ActionResult Quote()
        {
            using (InsuranceEntities db = new InsuranceEntities())
            {
                var clients = db.Clients;
                var clientVms = new List<ClientsVm>();
                foreach (var client in clients)
                {
                    var clientVm = new ClientsVm();
                    clientVm.FirstName = client.FirstName;
                    clientVm.LastName = client.LastName;
                    clientVm.Price = client.Price;
                    clientVms.Add(clientVm);
                }

                return View(clientVms);
            }
        }

        }
        
    }
