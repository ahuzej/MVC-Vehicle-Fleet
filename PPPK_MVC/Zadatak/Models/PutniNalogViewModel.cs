using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zadatak.Models
{
    public class PutniNalogViewModel
    {

        public int IDPutniNalog { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno")]
        public int? VozacID { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno")]
        public int? VoziloID { get; set; }

        public int? GradID { get; set; }

        public int? DrzavaID { get; set; }

        public IEnumerable<Vozac> ListaVozaca { get; set; }
        public IEnumerable<Vozilo> ListaVozila { get; set; }
        public IEnumerable<Grad> ListaGradova { get; set; }

        public IEnumerable<Drzava> ListaDrzava { get; set; }


        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Datum odlaska")]
        [Required(ErrorMessage = "Ovo polje je obavezno")]
        public DateTime? DatumOdlaska { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Datum dolaska")]
        [Required(ErrorMessage = "Ovo polje je obavezno")]
        public DateTime? DatumDolaska { get; set; }


        [Display(Name = "Broj sati")]
        [Required(ErrorMessage = "Ovo polje je obavezno")]
        public int? BrojSati { get; set; }

        [Display(Name = "Broj dnevnica")]
        [Required(ErrorMessage = "Ovo polje je obavezno")]
        public int? BrojDnevnica { get; set; }

        [Display(Name = "Iznos dnevnice")]
        [Required(ErrorMessage = "Ovo polje je obavezno")]
        public int? IznosDnevnice { get; set; }

        [StringLength(100)]
        [Display(Name = "Opis")]
        [Required(ErrorMessage = "Ovo polje je obavezno")]
        public string Opis { get; set; }


        public virtual Vozac Vozac { get; set; }


        public virtual Vozilo Vozilo { get; set; }

        public virtual StanjePutnogNaloga Stanje { get; set; }

        public PutniNalogViewModel()
        {

        }

        public void NamjestiStanje()
        {
            if (DatumOdlaska != null && DatumDolaska != null)
            {
                if (DateTime.Now > DatumOdlaska && DateTime.Now < DatumDolaska)
                {
                    Stanje = new StanjePutnogNaloga(StanjePutnogNaloga.Tip.Otvoreni, "green");
                }
                else if (DateTime.Now < DatumOdlaska && DateTime.Now < DatumDolaska)
                {
                    Stanje = new StanjePutnogNaloga(StanjePutnogNaloga.Tip.Buduci, "powderblue");
                }
                else
                {
                    Stanje = new StanjePutnogNaloga(StanjePutnogNaloga.Tip.Zatvoreni, "red");
                }

            }
        }

        public string Ukupno
        {
            get
            {
                return IznosZaPlatiti() + " kn";
            }
        }

        public int IznosZaPlatiti()
        {
            int a = BrojSati.HasValue ? Convert.ToInt32(BrojSati) : 0;
            int b = BrojDnevnica.HasValue ? Convert.ToInt32(BrojDnevnica) : 0;
            int c = IznosDnevnice.HasValue ? Convert.ToInt32(IznosDnevnice) : 0;
            return (b * c) * a;
        }
    }
}