namespace Zadatak.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    public class Vozilo
    {

        [Key]
        [XmlElement("idVozilo")]
        public int? IDVozilo { get; set; }

        [StringLength(30)]
        [XmlElement("tip")]
        public string Tip { get; set; }

        [StringLength(30)]
        [XmlElement("marka")]
        public string Marka { get; set; }

        [Display(Name = "Godina proizvodnje")]
        [XmlElement("godinaProizvodnje")]
        public int? GodinaProizvodnje { get; set; }

        [Display(Name = "Stanje kilometra")]
        [XmlElement("stanjeKilometra")]
        public int? StanjeKilometra { get; set; }

        [Display(Name = "Vozilo")]
        [XmlIgnore]
        public string PunoIme
        {
            get
            {
                return Marka + " " + Tip;
            }
        }
    }
}
