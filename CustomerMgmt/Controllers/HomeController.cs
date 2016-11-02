using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CustomerMgmt.Models;

namespace CustomerMgmt.Controllers
{
	public class HomeController : Controller
	{
		private CustomerEntities db = new CustomerEntities();

		public ActionResult Index()
		{
			var data = db.vw_CustomerList;
			return View(data);
		}
	}
}