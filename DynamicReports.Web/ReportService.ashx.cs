using System.IO;
using System.Web;

namespace DynamicReports.Web
{
    public class ReportService : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var dataPath = HttpContext.Current.Server.MapPath("Report.json");

            using (var reader = new StreamReader(dataPath))
            {
                var result = reader.ReadToEnd();
                context.Response.ContentType = "application/json";
                context.Response.Write(result);
            }
        }

        public bool IsReusable { get { return false; } }
    }
}