using System;
using System.Collections.Generic;

namespace TestPlanetun.Models
{
    public partial class Inspection
    {
        public int CompanyId { get; set; }
        public string BrokerCode { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string InspectionNumber { get; set; }
    }
}
