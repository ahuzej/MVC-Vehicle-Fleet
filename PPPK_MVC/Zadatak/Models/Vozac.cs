namespace Zadatak.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    public class Vozac
    {

        [Key]
        [XmlElement("idVozac")]
        public int? IDVozac { get; set; }

        [StringLength(30)]
        [Display(Name = "Ime")]
        [Required(ErrorMessage = "Ovo polje je obavezno")]
        [XmlElement("ime")]
        public string FirstName { get; set; }

        [StringLength(30)]
        [Display(Name = "Prezime")]
        [Required(ErrorMessage = "Ovo polje je obavezno")]
        [XmlElement("prezime")]
        public string LastName { get; set; }

        [StringLength(15)]
        [Required(ErrorMessage = "Ovo polje je obavezno")]
        [XmlElement("mobitel")]
        public string Mobitel { get; set; }

        [StringLength(15)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Vozaèka dozvola sadrži samo brojke")]
        [Required(ErrorMessage = "Ovo polje je obavezno")]
        [Display(Name = "Vozaèka dozvola")]
        [XmlElement("vozackaDozvola")]
        public string VozackaDozvola { get; set; }

        public string PunoIme
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

    }
}
