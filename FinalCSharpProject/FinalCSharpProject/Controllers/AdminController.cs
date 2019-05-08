
using FinalCSharpProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalCSharpProject.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            using (InsuranceEntities db = new InsuranceEntities())
            {
                var clients = db.Clients;
                var clientVms = new List<ClientsVm>();
                foreach (var client in clients)
                {
                    var clientVm = new ClientsVm();
                    clientVm.Id = client.Id;
                    clientVm.FirstName = client.FirstName;
                    clientVm.LastName = client.LastName;
                    clientVm.EmailAddress = client.EmailAddress;
                    clientVm.Price = client.Price;
                    clientVms.Add(clientVm);
                }

                return View(clientVms);
            }
        }
    }
}