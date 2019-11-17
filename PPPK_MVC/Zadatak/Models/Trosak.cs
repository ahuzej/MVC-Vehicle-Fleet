namespace Zadatak.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Xml.Serialization;

    
    public class Trosak
    {
        [Key]
        [XmlElement("idTrosak")]
        public int? IDTrosak { get; set; }
        [XmlElement("cijena")]  
        public double? Cijena { get; set; }
    }
}
