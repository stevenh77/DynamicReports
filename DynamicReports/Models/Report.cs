using System;
using System.Collections.Generic;

namespace DynamicReports.Models
{
    public class Report
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }

        public Dictionary<ReportParameter, object> ReportParameters { get; set; }
    }
}
