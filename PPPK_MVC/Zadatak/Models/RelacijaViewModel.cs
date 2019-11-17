using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zadatak.Models
{
    public class RelacijaViewModel
    {
        public int IDRelacija { get; set; }

        public IEnumerable<Grad> ListaGradova { get; set; }

        public IEnumerable<Drzava> ListaDrzava { get; set; }

        public IEnumerable<PutniNalog> ListaPutnihNaloga { get; set; }

        public IEnumerable<Vozilo> ListaVozila { get; set; }

        public IEnumerable<Vozac> ListaVozaca { get; set; }

        [Display(Name = "Grad polazak")]
        [Required]
        public int? GradPolazakID { get; set; }

        [Display(Name = "Grad dolazak")]
        [Required]
        public int? GradDolazakID { get; set; }

        [Display(Name = "Putni nalog")]
        [Required]
        public int? PutniNalogID { get; set; }

        [Required]
        public int? Kilometraza { get; set; }

        [Display(Name = "Prijevoz iznos")]
        [Required]
        public int? PrijevozIznos { get; set; }

        public virtual Grad GradPolazak { get; set; }

        public virtual Grad GradDolazak { get; set; }

        public virtual PutniNalog PutniNalog { get; set; }
    }
}