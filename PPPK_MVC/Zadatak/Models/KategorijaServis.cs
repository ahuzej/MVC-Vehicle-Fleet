using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Zadatak.Models
{
    public class KategorijaServis
    {
        [XmlElement("idKategorijaServis")]
        public int? IDKategorijaServis { get; set; }
        [XmlElement("naziv")]
        [Display(Name = "Kategorija servisa")]
        public string Naziv { get; set; }
    }
}