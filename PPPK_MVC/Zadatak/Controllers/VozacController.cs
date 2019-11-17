using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Zadatak.Models;
using Zadatak.Models.Database;

namespace Zadatak.Controllers
{
    public class VozacController : Controller
    {
        VozacRepository vozacRepository = new VozacRepository();
        public ActionResult Index(string filter)
        {
            List<Vozac> vozaci = vozacRepository.List().ToList();
            if (!String.IsNullOrEmpty(filter))
            {
                //vozaci = db.Vozaci.Where(v => (v.FirstName + v.LastName).Contains(filter)).ToList();
            }
            return View(vozaci);
        }

        public ActionResult Details(int? id)
        {
            Vozac vozac = new Vozac();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Vozac vozac = db.Vozaci.Find(id);
            if (vozac == null)
            {
                return HttpNotFound();
            }
            return View(vozac);
        }

        // GET: Vozac/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vozac/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDVozac,FirstName,LastName,Mobitel,VozackaDozvola")] Vozac vozac)
        {
            if (ModelState.IsValid)
            {
                vozacRepository.Add(vozac);
                return RedirectToAction("Index");
            }

            return View(vozac);
        }

        // GET: Vozac/Edit/5
        public ActionResult Edit(int? id)
        {
            Vozac vozac = vozacRepository.GetById(id);
            //ViewBag.popisVozaca = db.Vozaci.ToList();
            //Vozac vozac = db.Vozaci.Find(id);
            return View(vozac);
        }

        // POST: Vozac/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Vozac vozac)
        {
            if (ModelState.IsValid)
            {
                vozacRepository.UpdateVozac(vozac);
                //db.Entry(vozac).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vozac);
        }

        // GET: Vozac/Delete/5
        public ActionResult Delete(int? id)
        {
            Vozac vozac = vozacRepository.GetById(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (vozac == null)
            {
                return HttpNotFound();
            }
            return View(vozac);
        }

        // POST: Vozac/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            vozacRepository.Delete(id);
            //Vozac vozac = db.Vozaci.Find(id);
            //db.Vozaci.Remove(vozac);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
