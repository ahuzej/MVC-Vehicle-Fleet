using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zadatak.Models
{
    public class StanjePutnogNaloga
    {

        public StanjePutnogNaloga(Tip tip, string boja)
        {
            this.TipNaloga = tip;
            this.Boja = boja;
        }

        public enum Tip
        {
            Buduci = 1,
            Otvoreni = 2,
            Zatvoreni = 3
        }

        [Display(Name = "Stanje")]
        public Tip TipNaloga { get; set; }
        public string Boja { get; set; }
    }
}