using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zadatak.Models;
using Zadatak.Models.Database;

namespace Zadatak.Controllers
{
    public class RelacijaController : Controller
    {
        IRepository<PutniNalog> putniNalogRepository = new PutniNalogRepository();
        IRepository<Vozac> vozacRepository = new VozacRepository();
        IRepository<Vozilo> voziloRepository = new VoziloRepository();
        IRepository<Relacija> relacijaRepository = new RelacijaRepository();
        IRepository<Drzava> drzavaRepository = new DrzavaRepository();
        IRepository<Grad> gradRepository = new GradRepository();
        // GET: Relacija
        public ActionResult Index()
        {
            List<Relacija> relacije = relacijaRepository.List().ToList();
            List<RelacijaViewModel> model = new List<RelacijaViewModel>();
            foreach (Relacija r in relacije)
            {
                RelacijaViewModel rvm = new RelacijaViewModel
                {
                    IDRelacija = r.IDRelacija.Value,
                    GradPolazak = gradRepository.GetById(r.GradPolazakID),
                    GradDolazak = gradRepository.GetById(r.GradDolazakID),
                    Kilometraza = r.Kilometraza,
                    PutniNalog = putniNalogRepository.GetById(r.PutniNalogID),
                    PrijevozIznos = r.PrijevozIznos
                };

                rvm.PutniNalog.Vozac = vozacRepository.GetById(rvm.PutniNalog.VozacID);
                model.Add(rvm);

            }
            return View(model);
        }

        // GET: Relacija/Details/5
        public ActionResult Details(int id)
        {
            //Relacija r = SqlHandler.GetRelacije().ToList().Find(rl => rl.IDRelacija == id);
            Relacija r = relacijaRepository.GetById(id);
            RelacijaViewModel rvm = new RelacijaViewModel
            {
                IDRelacija = r.IDRelacija.Value,
                GradPolazak = gradRepository.GetById(r.GradPolazakID),
                GradDolazak = gradRepository.GetById(r.GradDolazakID),
                Kilometraza = r.Kilometraza,
                PutniNalog = putniNalogRepository.GetById(r.PutniNalogID),
                PrijevozIznos = r.PrijevozIznos
            };
            return View(rvm);
        }

        // GET: Relacija/Create
        public ActionResult Create()
        {
            var model = new RelacijaViewModel
            {
                ListaDrzava = drzavaRepository.List(),
                ListaGradova = gradRepository.List(),
                ListaPutnihNaloga = putniNalogRepository.List(),
                ListaVozaca = vozacRepository.List(),
                ListaVozila = voziloRepository.List(),
            };
            foreach (var nalog in model.ListaPutnihNaloga)
            {
            nalog.Vozac = vozacRepository.GetById(nalog.VozacID);

            }
            return View(model);
        }

        // POST: Relacija/Create
        [HttpPost]
        public ActionResult Create(RelacijaViewModel model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    Relacija relacija = new Relacija
                    {
                        GradPolazakID = model.GradPolazakID,
                        GradDolazakID = model.GradDolazakID,
                        PutniNalogID = model.PutniNalogID,
                        Kilometraza = model.Kilometraza,
                        PrijevozIznos = model.PrijevozIznos,
                    };
                    relacijaRepository.Add(relacija);
                }
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Relacija/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Relacija/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, RelacijaViewModel model)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Relacija/Delete/5
        public ActionResult Delete(int id)
        {
            Relacija r = relacijaRepository.GetById(id);
            RelacijaViewModel rvm = new RelacijaViewModel
            {
                IDRelacija = r.IDRelacija.Value,
                GradPolazak = gradRepository.GetById(r.GradPolazakID),
                GradDolazak = gradRepository.GetById(r.GradDolazakID),
                Kilometraza = r.Kilometraza,
                PutniNalog = putniNalogRepository.GetById(r.PutniNalogID),
                PrijevozIznos = r.PrijevozIznos
            };
            return View(rvm);
        }

        // POST: Relacija/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                relacijaRepository.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
