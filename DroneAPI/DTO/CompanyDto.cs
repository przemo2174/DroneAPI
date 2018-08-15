using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DronesApp.API.DTO
{
    public class CompanyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Nip { get; set; }
        public string Krs { get; set; }
        public string Base { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string Position { get; set; }
        public string Industry { get; set; }
        public string Postcode { get; set; }
        public string Voivodeship { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public string Pkd { get; set; }
        public string Pkdcode { get; set; }
        public bool? Council { get; set; }
        public bool? TableInReport { get; set; }
        public bool? Suspended { get; set; }
        public string Ankieta2015 { get; set; }
        public string Ankieta2016 { get; set; }
        public string Ankieta2017 { get; set; }
        public string Tag2015 { get; set; }
        public string Tag2016 { get; set; }
        public string Tag2017 { get; set; }
    }
}
