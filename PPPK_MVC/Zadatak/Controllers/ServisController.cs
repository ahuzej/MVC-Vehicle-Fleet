using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zadatak.Models;
using Zadatak.Models.Database;

namespace Zadatak.Controllers
{
    public class ServisController : Controller
    {
        ServisRepository servisRepository = new ServisRepository();
        IRepository<KategorijaServis> kategorijaServisRepository = new KategorijaServisRepository();
        IRepository<Vozilo> voziloRepository = new VoziloRepository();

        public ActionResult Index()
        {
            return View(servisRepository.List().OrderByDescending(servis => servis.Datum));
        }

        public ActionResult Create()
        {
            ViewBag.Vozila = voziloRepository.List();
            ViewBag.Kategorije = kategorijaServisRepository.List();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDServis,VoziloID,Cijena,Opis,Datum,KategorijaServisID")] Servis servis)
        {
            if (ModelState.IsValid)
            {
                servisRepository.Add(servis);
                return RedirectToAction("Index");
            }
            //if (ModelState["Cijena"].Errors.Count > 0)
            //{
            //    ModelState["Cijena"].Errors.Clear();
            //    ModelState["Cijena"].Errors.Add("Unesite numericki broj");
            //}


            ViewBag.Vozila = voziloRepository.List();
            ViewBag.Kategorije = kategorijaServisRepository.List();
            return View(servis);
        }

        public ActionResult Edit(int? id)
        {
            Servis servis = servisRepository.GetById(id);
            ViewBag.Kategorije = kategorijaServisRepository.List();
            ViewBag.Vozila = voziloRepository.List();
            return View(servis);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Servis servis)
        {
            if (ModelState.IsValid)
            {
                servisRepository.Update(servis);
                //db.Entry(vozac).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(servis);
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            Servis servis = servisRepository.GetById(id);
            return View(servis);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            Servis servis = servisRepository.GetById(id);
            return View(servis);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            servisRepository.Delete(id);
            //Vozac vozac = db.Vozaci.Find(id);
            //db.Vozaci.Remove(vozac);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}