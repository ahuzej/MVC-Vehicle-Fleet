using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Zadatak.Models;
using Zadatak.Models.Database;

namespace Zadatak.Controllers
{
    public class PostavkeController : Controller
    {
        IRepository<PutniNalog> putniNalogRepository = new PutniNalogRepository();
        IRepository<Vozac> vozacRepository = new VozacRepository();
        IRepository<Vozilo> voziloRepository = new VoziloRepository();
        IRepository<Drzava> drzavaRepository = new DrzavaRepository();
        IRepository<Grad> gradRepository = new GradRepository();
        IRepository<Relacija> relacijaRepository = new RelacijaRepository();
        IRepository<Servis> servisRepository = new ServisRepository();
        IRepository<KategorijaServis> kategorijaServisRepository = new KategorijaServisRepository();
        IRepository<KategorijaTroska> kategorijaTroska = new KategorijaTrosakRepository();

        public ActionResult Povrati()
        {
            return View();
        }

        public ActionResult Backup()
        {
            BackupModel model = new BackupModel();
            StringBuilder sb = new StringBuilder();
            model.Drzave = drzavaRepository.List().ToList();
            model.Gradovi = gradRepository.List().ToList();
            model.Vozaci = vozacRepository.List().ToList();
            model.Vozila = voziloRepository.List().ToList();
            model.PutniNalozi = putniNalogRepository.List().ToList();
            model.Relacije = relacijaRepository.List().ToList();
            model.KategorijeTroskova = kategorijaTroska.List().ToList();
            model.KategorijeServisa = kategorijaServisRepository.List().ToList();
            model.Servisi = servisRepository.List().ToList();
            sb.Append(Server.MapPath("/")).Append("DBBackup-").Append(DateTime.Now.ToLongDateString()).Append(".xml");
            model.Save(sb.ToString());
            return View();
        }
    }
}