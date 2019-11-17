using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zadatak.Models
{
    public class ServisViewModel
    {
        public int IDServis { get; set; }

        public int VoziloID { get; set; }

        public double Cijena { get; set; }

        public string Opis { get; set; }

        public DateTime Datum { get; set; }

        public int KategorijaServisID { get; set; }

        public virtual Vozilo Vozilo { get; set; }
    }
}