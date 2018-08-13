using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace DroneAPI.Helpers
{
    public class CompaniesQueryParameters
    {
        private const int MaxPageSize = 20;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public string Name { get; set; }
        public string Nip { get; set; }
        public string Voivodeship { get; set; }
        public string City { get; set; }
    }
}
