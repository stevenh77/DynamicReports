using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DynamicReports.Models
{
    [DataContract]
    public class ReportType
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public IList<ReportParameter> Parameters { get; set; }

        [DataMember]
        public string Endpoint { get; set; }
    }
}
