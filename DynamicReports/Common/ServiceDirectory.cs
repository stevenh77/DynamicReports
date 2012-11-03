using System;
using System.Windows.Browser;

namespace DynamicReports.Common
{
    public class ServiceDirectory
    {
        public static Uri GetReports { get { return new Uri(HtmlPage.Document.DocumentUri, "ReportService.ashx"); }}
    }
}
