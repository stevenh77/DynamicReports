using System.Collections.Generic;
using DynamicReports.Models;

namespace DynamicReports.ViewModels
{
    public class FlyoutViewModel
    {
        public FlyoutViewModel(List<Report> reports)
        {
            Reports = reports;
        }

        public List<Report> Reports { get; set; }
    }
}
