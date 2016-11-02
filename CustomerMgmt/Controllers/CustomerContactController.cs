using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CustomerMgmt.Models;

namespace CustomerMgmt.Controllers
{
    public class CustomerContactController : Controller
    {
        private CustomerEntities db = new CustomerEntities();

        // GET: CustomerContact
        public ActionResult Index(string search, int? id)
        {
			var customerContact = db.客戶聯絡人.ToList();
			if (id != null)
			{
				customerContact = customerContact.Where(contact => contact.客戶Id.Equals(id)).ToList();
			}

			if (!string.IsNullOrEmpty(search))
			{
				customerContact = customerContact.Where(contact => contact.姓名.Contains(search)).ToList();
			}

			TempData["id"] = id;
			return View(customerContact);
            
        }

        // GET: CustomerContact/Details/5
        public ActionResult Details(int? id)
        {
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
			if (客戶聯絡人 == null)
			{
				return HttpNotFound();
			}
			return View(客戶聯絡人);
		}

        // GET: CustomerContact/Create
        public ActionResult Create(int? id)
        {
			var customerData = db.客戶資料.ToList();
			if (id != null)
			{
				customerData = customerData.Where(x => x.Id.Equals(id)).ToList();
			}
			ViewBag.客戶Id = new SelectList(customerData, "Id", "客戶名稱");

			return View();
        }

        // POST: CustomerContact/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話,是否已刪除")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
				var duplicateCount = db.客戶聯絡人
					.Where
					( contact => 
						contact.客戶Id.Equals(客戶聯絡人.客戶Id) && 
						contact.Email.Equals(客戶聯絡人.Email)
					).ToList().Count;

				if (duplicateCount.Equals(0))
				{
					db.客戶聯絡人.Add(客戶聯絡人);
					db.SaveChanges();
					return RedirectToAction("Index");
				}
				else
				{
					ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
					TempData["DuplicateEmail"] = "true";
					return View(客戶聯絡人);
				}
            }

            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: CustomerContact/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // POST: CustomerContact/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話,是否已刪除")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                db.Entry(客戶聯絡人).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: CustomerContact/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // POST: CustomerContact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
			客戶聯絡人.是否已刪除 = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
