using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Zadatak.Models
{
    public class Servis
    {
        [Key]
        [XmlElement("idServis")]
        public int? IDServis { get; set; }

        [Display(Name = "Vozilo")]
        [XmlElement("voziloId")]
        public int VoziloID { get; set; }

        [XmlElement("cijena")]
        [Required(ErrorMessage = "Ovo polje je obavezno")]
        public double? Cijena { get; set; }

        [XmlElement("opis")]
        [Required(ErrorMessage = "Ovo polje je obavezno")]
        public string Opis { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [XmlElement("datum")]
        [Required(ErrorMessage = "Ovo polje je obavezno")]
        public DateTime? Datum { get; set; }

        [Display(Name = "Kategorija servisa")]
        [XmlElement("kategorijaServisId")]
        public int KategorijaServisID { get; set; }

        [XmlIgnore]
        public virtual Vozilo Vozilo { get; set; }

        [XmlIgnore]
        public virtual KategorijaServis KategorijaServis { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }

    }
}