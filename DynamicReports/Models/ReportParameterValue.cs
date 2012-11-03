using System.Runtime.Serialization;

namespace DynamicReports.Models
{
    [DataContract]
    public class ReportParameterValue
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string DisplayName { get; set; }
    }
}
