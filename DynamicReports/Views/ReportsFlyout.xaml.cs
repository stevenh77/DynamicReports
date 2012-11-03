using System.Collections.Generic;
using DynamicReports.Models;
using DynamicReports.ViewModels;

namespace DynamicReports.Views
{
    public partial class ReportsFlyOut
    {
        public ReportsFlyOut()
        {
            InitializeComponent();

            var report1 = new Report() { Id = 1, Name = "Apple Pending Trades" };
            var report2 = new Report() { Id = 2, Name = "Microsoft Open Trades"};

            var reports = new List<Report> { report1, report2 };
            this.DataContext = new FlyoutViewModel(reports);
        }
    }
}
