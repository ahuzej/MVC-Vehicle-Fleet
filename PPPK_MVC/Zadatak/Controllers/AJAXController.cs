using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Xml;
using System.Xml.Serialization;
using Zadatak.Models;
using Zadatak.Models.Database;

namespace Zadatak.Controllers
{
    public class AJAXController : Controller
    {
        IRepository<PutniNalog> putniNalogRepository = new PutniNalogRepository();
        IRepository<Vozilo> voziloRepository = new VoziloRepository();
        IRepository<Vozac> vozacRepository = new VozacRepository();
        IRepository<Drzava> drzavaRepository = new DrzavaRepository();
        IRepository<Grad> gradRepository = new GradRepository();
        RelacijaRepository relacijaRepository = new RelacijaRepository();
        IRepository<KategorijaTroska> kategorijaTrosakRepository = new KategorijaTrosakRepository();
        IRepository<Servis> servisRepository = new ServisRepository();
        IRepository<KategorijaServis> kategorijaServisRepository = new KategorijaServisRepository();

        // GET: AJAX
        public ActionResult Index()
        {
            return View();
        }

        public void PovratiBazu()
        {

        }

        [HttpGet]
        public ActionResult Filter(string searchQuery, string filterProp)
        {
            
            List<PutniNalog> filtered = putniNalogRepository.List().ToList();
            if (searchQuery == null)
                filterProp = null;
            foreach (var item in filtered)
            {
                
            }
            foreach (PutniNalog nalog in putniNalogRepository.List().ToList())
            {
                switch(filterProp)
                {
                    case "Ime":
                    Vozac vozac = vozacRepository.GetById(nalog.VozacID);
                    if (!vozac.FirstName.Contains(searchQuery) && !vozac.LastName.Contains(searchQuery))
                    {
                        filtered.RemoveAll(pn => pn.IDPutniNalog == nalog.IDPutniNalog);
                    }
                    break;
                    case "Vozilo":
                        Vozilo vozilo = voziloRepository.GetById(nalog.VoziloID);
                        if (!vozilo.Marka.Contains(searchQuery) && !vozilo.Tip.Contains(searchQuery))
                        {
                            filtered.RemoveAll(pn => pn.IDPutniNalog == nalog.IDPutniNalog);
                        }
                        break;
                    case "Datum odlaska":

                        string[] dateParts = searchQuery.Split('.');
                        DateTime date = new DateTime(
                            int.Parse(dateParts[2]),
                            int.Parse(dateParts[1]),
                            int.Parse(dateParts[0]));
                        if(!(nalog.DatumOdlaska == date))
                        {
                            filtered.RemoveAll(pn => pn.IDPutniNalog == nalog.IDPutniNalog);
                        }
                        break;
                    case "Datum dolaska":
                        dateParts = searchQuery.Split('.');
                        date = new DateTime(
                            int.Parse(dateParts[2]),
                            int.Parse(dateParts[1]),
                            int.Parse(dateParts[0]));
                        if (!(nalog.DatumDolaska == date))
                        {
                            filtered.RemoveAll(pn => pn.IDPutniNalog == nalog.IDPutniNalog);
                        }
                        break;
                    case "Broj sati":
                        int brojSati;
                        bool success = int.TryParse(searchQuery, out brojSati);
                        if(success && !(brojSati == nalog.BrojSati))
                        {
                            filtered.RemoveAll(pn => pn.IDPutniNalog == nalog.IDPutniNalog);
                        }
                        break;
                    case "Broj dnevnica":
                        int brojDnevnica;
                        success = int.TryParse(searchQuery, out brojDnevnica);
                        if (success && !(brojDnevnica == nalog.BrojDnevnica))
                        {
                            filtered.RemoveAll(pn => pn.IDPutniNalog == nalog.IDPutniNalog);
                        }
                        break;
                    case "Iznos dnevnice":
                        int iznosDnevnice;
                        success = int.TryParse(searchQuery, out iznosDnevnice);
                        if (success && !(iznosDnevnice == nalog.IznosDnevnice))
                        {
                            filtered.RemoveAll(pn => pn.IDPutniNalog == nalog.IDPutniNalog);
                        }
                        break;
                }

            }
            List<PutniNalogViewModel> model = new List<PutniNalogViewModel>();
            foreach (PutniNalog item in filtered)
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
            return PartialView("_PutniNalogTablica", model);

        }

        public FileResult ExportVozilo(int idVozilo)
        {
            Vozilo vozilo = voziloRepository.GetById(idVozilo);
            IEnumerable<Servis> servisi = servisRepository.List().Where(servis => servis.VoziloID == idVozilo);
            if(vozilo != null)
            {
                StringWriter stringWriter = new StringWriter();
                using (HtmlTextWriter writer = new HtmlTextWriter(stringWriter))
                {

                    writer.RenderBeginTag(HtmlTextWriterTag.H1);
                    writer.Write("IZVJEŠTAJ VOZILA " + idVozilo);
                    writer.RenderEndTag();

                    writer.AddAttribute(HtmlTextWriterAttribute.Id, idVozilo.ToString());
                    writer.RenderBeginTag(HtmlTextWriterTag.Div); 


                    writer.RenderBeginTag(HtmlTextWriterTag.P);
                    writer.Write("Vozilo: " + vozilo.PunoIme);
                    writer.RenderEndTag();

                    writer.RenderBeginTag(HtmlTextWriterTag.P);
                    writer.Write("Kilometri: " + vozilo.StanjeKilometra);
                    writer.RenderEndTag();

                    writer.RenderBeginTag(HtmlTextWriterTag.P);
                    writer.Write("Servis odraden: " + servisi.Count() + " puta");
                    writer.RenderEndTag();

                    if(servisi.Count() > 0)
                    {
                        writer.RenderBeginTag(HtmlTextWriterTag.Table);
                        writer.RenderBeginTag(HtmlTextWriterTag.Thead);
                        writer.RenderBeginTag(HtmlTextWriterTag.Tr);

                        writer.RenderBeginTag(HtmlTextWriterTag.Th);
                        writer.Write("Datum");
                        writer.RenderEndTag();

                        writer.RenderBeginTag(HtmlTextWriterTag.Th);
                        writer.Write("Kategorija");
                        writer.RenderEndTag();

                        writer.RenderBeginTag(HtmlTextWriterTag.Th);
                        writer.Write("Cijena");
                        writer.RenderEndTag();

                        writer.RenderEndTag();

                        writer.RenderBeginTag(HtmlTextWriterTag.Tbody);

                        foreach (var servis in servisi)
                        {
                            writer.RenderBeginTag(HtmlTextWriterTag.Tr);


                            writer.RenderBeginTag(HtmlTextWriterTag.Td);
                            writer.Write(servis.Datum.Value.ToShortDateString());
                            writer.RenderEndTag();

                            writer.RenderBeginTag(HtmlTextWriterTag.Td);
                            writer.Write(servis.KategorijaServis.Naziv);
                            writer.RenderEndTag();

                            writer.RenderBeginTag(HtmlTextWriterTag.Td);
                            writer.Write(servis.Cijena + " kn");
                            writer.RenderEndTag();

                            writer.RenderEndTag();

                        }

                        writer.RenderEndTag();
                        writer.RenderEndTag();
                        writer.RenderEndTag();
                    }


                // TABLICA KRAJ

                    writer.RenderEndTag(); // End #1

                }
                StringBuilder path = new StringBuilder();

                path.Append(Server.MapPath("/")).Append("Reports\\").Append("REP-").Append(idVozilo).Append(".html");
                using (StreamWriter file = new StreamWriter(path.ToString()))
                {
                    file.WriteLine(stringWriter.ToString()); // "sb" is the StringBuilder

                }
                byte[] fileBytes = System.IO.File.ReadAllBytes(path.ToString());
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, Path.GetFileName(path.ToString()));
            }
            return null;
        }

        public FileResult ExportXML(int idRel)
        {
            StringBuilder path = new StringBuilder();
            XmlWriterSettings xmlPostavke = new XmlWriterSettings();
            xmlPostavke.Indent = true;
            path.Append(Server.MapPath("/")).Append("Relations\\").Append("REL-").Append(idRel).Append(".xml");
            DataTable tbl = relacijaRepository.GetTblRelacije();
            DataRow relRow = null;
            relRow = tbl.Rows.OfType<DataRow>().First(k => int.Parse(k["IDRelacija"].ToString()) == idRel);
            if(relRow != null)
            {
                Relacija r = new Relacija();
                r.IDRelacija = idRel;
                r.GradPolazakID = int.Parse(relRow["GradPolazakID"].ToString());
                r.GradDolazakID = int.Parse(relRow["GradDolazakID"].ToString());
                r.PutniNalogID = int.Parse(relRow["PutniNalogID"].ToString());
                r.Kilometraza = int.Parse(relRow["Kilometraza"].ToString());
                r.PrijevozIznos = int.Parse(relRow["PrijevozIznos"].ToString());

                XmlFileWriter writer = new XmlFileWriter(r);
                writer.Write(path.ToString());
                byte[] xmlBytes = System.IO.File.ReadAllBytes(path.ToString());
                return File(xmlBytes, System.Net.Mime.MediaTypeNames.Application.Octet, Path.GetFileName(path.ToString()));
            }
            return null;
        }

        [HttpPost]
        public ActionResult ImportXML(HttpPostedFileBase file,int type)
        {
            switch(type)
            {
                case 0:
                    ImportRelacija(file);
                    return RedirectToAction("Index", "Relacija", null);
                case 1:
                    ImportBaza(file);
                    break;
            }
            return RedirectToAction("Index", "Home", null);
        }

        [HttpPost]
        public void ImportRelacija(HttpPostedFileBase file)
        {
            if(file != null)
            {
                if (file.ContentLength > 0)
                {
                    using (Stream stream = file.InputStream)
                    {
                        using (XmlReader reader = XmlReader.Create(stream))
                        {
                            XmlSerializer serializer = new XmlSerializer(typeof(Relacija));
                            try
                            {
                                Relacija r = serializer.Deserialize(reader) as Relacija;
                                relacijaRepository.Add(r);
                            }
                            catch (InvalidOperationException e)
                            {

                                Console.WriteLine(e.Message);
                            }

                        }
                    }
                }
            }
        }

        [HttpPost]
        public void ImportBaza(HttpPostedFileBase file)
        {
            if (file.ContentLength > 0)
            {
                using (Stream stream = file.InputStream)
                {
                    using (XmlReader reader = XmlReader.Create(stream))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(BackupModel));
                        try
                        {
                            BackupModel m = serializer.Deserialize(reader) as BackupModel;

                            //insert drzava
                            foreach (Drzava drzava in m.Drzave)
                            {
                                drzavaRepository.Add(drzava);
                            }
                            //insert gradova
                            foreach (Grad grad in m.Gradovi)
                            {
                                gradRepository.Add(grad);
                            }
                            //insert vozaca
                            foreach (Vozac vozac in m.Vozaci)
                            {
                                vozacRepository.Add(vozac);
                            }
                            //insert vozila
                            foreach (Vozilo vozilo in m.Vozila)
                            {
                                voziloRepository.Add(vozilo);
                            }
                            //insert putnog naloga
                            foreach (PutniNalog nalog in m.PutniNalozi)
                            {
                                putniNalogRepository.Add(nalog);
                            }
                            //insert relacije
                            foreach (Relacija relacija in m.Relacije)
                            {
                                relacijaRepository.Add(relacija);
                            }
                            //insert kategorija troskova
                            foreach (KategorijaTroska kategorija in m.KategorijeTroskova)
                            {
                                kategorijaTrosakRepository.Add(kategorija);
                            }
                            // insert kategorija servisa
                            foreach (KategorijaServis kategorijaServis in m.KategorijeServisa)
                            {
                                kategorijaServisRepository.Add(kategorijaServis);
                            }
                            //insert servisa
                            foreach (Servis servis in m.Servisi)
                            {
                                servisRepository.Add(servis);
                            }
                        }
                        catch (InvalidOperationException e)
                        {

                            Console.WriteLine(e.Message);
                        }
                        
                    }
                }
            }
        }
    }
}