using System.IO;
using System.Web;

namespace DynamicReports.Web
{
    public class ExecutionService : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string[] values = context.Request.Params.GetValues("ReportName");

            if (values == null || values.Length == 0) 
                throw new HttpException(400, "ReportName not specified!");

            var endpoint = string.Format("{0}.json", values[0]);

            var dataPath = HttpContext.Current.Server.MapPath(endpoint);

            using (var reader = new StreamReader(dataPath))
            {
                var result = reader.ReadToEnd();
                context.Response.ContentType = "application/json";
                context.Response.Write(result);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}