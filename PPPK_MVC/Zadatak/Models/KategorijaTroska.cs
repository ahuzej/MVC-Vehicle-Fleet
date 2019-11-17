using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Zadatak.Models
{
    public class KategorijaTroska
    {
        [XmlElement("idKategorijaTrosak")]
        public int? IDKategorijaTrosak { get; set; }
        [XmlElement("naziv")]
        public string Naziv { get; set; }
    }
}