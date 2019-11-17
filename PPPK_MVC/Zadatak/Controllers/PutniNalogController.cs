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
    public class PutniNalogController : Controller
    {
        PutniNalogRepository putniNalogRepository = new PutniNalogRepository();
        IRepository<Vozac> vozacRepository = new VozacRepository();
        IRepository<Vozilo> voziloRepository = new VoziloRepository();
        IRepository<Drzava> drzavaRepository = new DrzavaRepository();
        GradRepository gradRepository = new GradRepository();
        // GET: PutniNalog
        public ActionResult Index()
        {
            List<PutniNalog> nalozi = putniNalogRepository.List().ToList();
            List<PutniNalogViewModel> model = new List<PutniNalogViewModel>();
            foreach (PutniNalog item in nalozi)
            {
                PutniNalogViewModel putniNalogVM = new PutniNalogViewModel
                {
                    Vozac = vozacRepository.GetById(item.VozacID),
                    Vozilo = voziloRepository.GetById(item.VoziloID),
                    BrojDnevnica = item.BrojDnevnica,
                    BrojSati = item.BrojSati,
                    IDPutniNalog = item.IDPutniNalog.Value,
                    IznosDnevnice = item.IznosDnevnice,
                    Opis = item.Opis,
                    DatumDolaska = item.DatumDolaska,
                    DatumOdlaska = item.DatumOdlaska
                };
                putniNalogVM.NamjestiStanje();
                model.Add(putniNalogVM);
            }
            //var putniNalozi = db.PutniNalozi.Include(p => p.Vozac);
            return View(model);
        }

        // GET: PutniNalog/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PutniNalog putniNalog = null; //OVDJE IDE FIND
            if (putniNalog == null)
            {
                return HttpNotFound();
            }
            return View(putniNalog);
        }

        public ActionResult Create()
        {
            var model = new PutniNalogViewModel
            {
                ListaVozaca = vozacRepository.List(),
                ListaVozila = voziloRepository.List(),
                ListaDrzava = drzavaRepository.List(),
                ListaGradova = gradRepository.List()
            };
            return View(model);
        }

        public JsonResult GetGradovi(int drzava)
        {
            return Json(gradRepository.GetByDrzava(drzava).ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PutniNalogViewModel putniNalog)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                PutniNalog nalog = new PutniNalog
                {
                    VozacID = putniNalog.VozacID,
                    VoziloID = putniNalog.VoziloID,
                    BrojSati = putniNalog.BrojSati,
                    BrojDnevnica = putniNalog.BrojDnevnica,
                    DatumDolaska = putniNalog.DatumDolaska,
                    DatumOdlaska = putniNalog.DatumOdlaska,
                    IznosDnevnice = putniNalog.IznosDnevnice,
                    Opis = putniNalog.Opis
                };
                putniNalogRepository.Add(nalog);
                //db.PutniNalozi.Add(putniNalog);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            putniNalog.ListaVozaca = vozacRepository.List();
            putniNalog.ListaVozila = voziloRepository.List();
            putniNalog.ListaGradova = gradRepository.List();
            putniNalog.ListaDrzava = drzavaRepository.List();
            return View(putniNalog);
        }

        // GET: PutniNalog/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PutniNalog putniNalog = putniNalogRepository.GetById(id);
            if (putniNalog == null)
            {
                return HttpNotFound();
            }
            var model = new PutniNalogViewModel
            {
                ListaVozaca = vozacRepository.List(),
                ListaVozila = voziloRepository.List(),
                BrojDnevnica = putniNalog.BrojDnevnica,
                BrojSati = putniNalog.BrojSati,
                DatumDolaska = putniNalog.DatumDolaska,
                DatumOdlaska = putniNalog.DatumOdlaska,
                Opis = putniNalog.Opis,
                IznosDnevnice = putniNalog.IznosDnevnice,
                VozacID = putniNalog.VozacID,
                VoziloID = putniNalog.VoziloID,
                Vozac = putniNalog.Vozac,
                IDPutniNalog = putniNalog.IDPutniNalog.Value

            };
            //ViewBag.VozacID = new SelectList(Models.SqlHandler.GetVozaci(), "IDVozac", "FirstName", putniNalog.VozacID);
           // ViewBag.VoziloID = new SelectList(Models.SqlHandler.GetVozila(), "IDVozilo", "FirstName", putniNalog.VozacID);
            return View(model);
        }

        // POST: PutniNalog/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PutniNalogViewModel putniNalog)
        {
            if (ModelState.IsValid)
            {
                PutniNalog nalog = new PutniNalog
                {
                    VozacID = putniNalog.VozacID,
                    IDPutniNalog = putniNalog.IDPutniNalog,
                    VoziloID = putniNalog.VoziloID,
                    BrojSati = putniNalog.BrojSati,
                    BrojDnevnica = putniNalog.BrojDnevnica,
                    DatumDolaska = putniNalog.DatumDolaska,
                    DatumOdlaska = putniNalog.DatumOdlaska,
                    IznosDnevnice = putniNalog.IznosDnevnice,
                    Opis = putniNalog.Opis
                };
                putniNalogRepository.Update(nalog);
                return RedirectToAction("Index");
            }
            return View(putniNalog);
        }

        // GET: PutniNalog/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PutniNalog putniNalog = putniNalogRepository.GetById(id);
            if (putniNalog == null)
            {
                return HttpNotFound();
            }
            PutniNalogViewModel model = new PutniNalogViewModel
            {
                Vozac = vozacRepository.GetById(putniNalog.VozacID),
                Vozilo = voziloRepository.GetById(putniNalog.VoziloID),
                BrojDnevnica = putniNalog.BrojDnevnica,
                BrojSati = putniNalog.BrojSati,
                IDPutniNalog = putniNalog.IDPutniNalog.Value,
                IznosDnevnice = putniNalog.IznosDnevnice,
                Opis = putniNalog.Opis,
                DatumDolaska = putniNalog.DatumDolaska,
                DatumOdlaska = putniNalog.DatumOdlaska
            };
            return View(model);
        }

        // POST: PutniNalog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            putniNalogRepository.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
