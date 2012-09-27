using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SlaCalculation.Models;

namespace SlaCalculation.Controllers
{
    

    public class CreateUserController : Controller
    {
        
        private UserDBContext db = new UserDBContext();
        [HttpGet]
        public ActionResult Create()
        {
            var model = new CreateUserViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CreateUserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new User();
                user.Name = viewModel.Name;
                user.EmployeeId = viewModel.EmployeeId;
                user.Email = viewModel.Email;
                user.Dob = viewModel.Dob;
                user.Mobile = viewModel.Mobile;
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(viewModel);
        }


    }
}
