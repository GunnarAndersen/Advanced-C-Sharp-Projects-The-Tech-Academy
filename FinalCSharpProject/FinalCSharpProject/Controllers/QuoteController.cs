using FinalCSharpProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalCSharpProject.Controllers
{
    public class QuoteController : Controller
    {
        // GET: Quote
        public ActionResult Index()
        {
            
                using (InsuranceEntities db = new InsuranceEntities())
                {
                    db.Clients.Max(u => u.Id);
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