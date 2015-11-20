using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YleanaCodingExercise.Models;
using YleanaCodingExercise.Models.ViewModels;

namespace YleanaCodingExercise.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetCustomers()
        {
            using (var db = new YleanaCEEntities())
            {
                try
                {
                    var customerList = (from customers in db.Customers
                                        select customers).ToList();
                    return Json(customerList);
                }
                catch(Exception e)
                {
                    return Json(new ResultMessageViewModel() { Result = "Error", Message = String.Format("An error occured: ({0}) {1}", e.Source, e.Message) });
                }
                
            }
            
        }
        [HttpPost]
        public JsonResult AddNewCustomer(CustomerViewModel customer)
        {
            using (var db = new YleanaCEEntities())
            {
                try
                {
                    db.Customers.Add(new Customer()
                    {
                        CustomerName = customer.CustomerName,
                        ContactName = customer.ContactName,
                        Address = customer.Address,
                        City = customer.City,
                        PostalCode = customer.PostalCode,
                        Country = customer.Country
                    });
                    db.SaveChanges();
                }
                catch(Exception e)
                {
                    return Json(new ResultMessageViewModel() { Result = "Error", Message = String.Format("An error occured: ({0}) {1}", e.Source, e.Message) });
                }
            }
            return Json(new ResultMessageViewModel() { Result = "Success", Message = "Customer added succesfully" });
        }

    }
}