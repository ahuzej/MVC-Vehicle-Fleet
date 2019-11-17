using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Zadatak.Models;
using Zadatak.Models.Database;

namespace Zadatak.Controllers
{
    public class VoziloController : Controller
    {
        private VoziloRepository voziloRepository = new VoziloRepository();
        private IRepository<Servis> servisRepository = new ServisRepository();

        // GET: Vozilo
        public ActionResult Index()
        {
            IEnumerable<Vozilo> popisVozila = voziloRepository.List();
            return View(popisVozila);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Vozilo vozilo)
        {
            if(ModelState.IsValid)
            {
                if(voziloRepository.Add(vozilo) > 0)
                {
                    return RedirectToAction("Index");
                }

            }
            return View(vozilo);
        }

        public ActionResult Details(int? id)
        {
            Vozilo vozilo = voziloRepository.GetById(id);
            ViewBag.Servisi = servisRepository.List().Where(servis => servis.VoziloID == id).OrderByDescending(servis => servis.Datum);
            return View(vozilo);
        }

        public ActionResult Edit(int? id)
        {
            Vozilo vozilo = voziloRepository.GetById(id);
            return View(vozilo);
        }

        // POST: Vozac/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Vozilo vozilo)
        {
            if (ModelState.IsValid)
            {
                voziloRepository.Update(vozilo);
                return RedirectToAction("Index");
            }
            return View(vozilo);
        }
        // GET: Vozac/Delete/5
        public ActionResult Delete(int? id)
        {
            Vozilo vozilo = voziloRepository.GetById(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (vozilo == null)
            {
                return HttpNotFound();
            }
            return View(vozilo);
        }

        // POST: Vozac/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            voziloRepository.Delete(id);
            //Vozac vozac = db.Vozaci.Find(id);
            //db.Vozaci.Remove(vozac);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}