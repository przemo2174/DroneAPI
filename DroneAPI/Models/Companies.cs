using System;
using System.Collections.Generic;

namespace DroneAPI.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Nazwa { get; set; }
        public string Nip { get; set; }
        public string Krs { get; set; }
        public string Baza { get; set; }
        public DateTime? AktualneNaDzien { get; set; }
        public string Stanowisko { get; set; }
        public string Branza { get; set; }
        public string Kod { get; set; }
        public string Wojewodztwo { get; set; }
        public string Miasto { get; set; }
        public string Ulica { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string Witryna { get; set; }
        public string Pkd { get; set; }
        public string KodPkd { get; set; }
        public bool? Sejmik { get; set; }
        public bool? TabelaWraporcie { get; set; }
        public bool? Zawieszony { get; set; }
        public string Ankieta2015 { get; set; }
        public string Ankieta2016 { get; set; }
        public string Ankieta2017 { get; set; }
        public string Tag2015 { get; set; }
        public string Tag2016 { get; set; }
        public string Tag2017 { get; set; }
    }
}
