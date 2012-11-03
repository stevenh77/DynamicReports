using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DynamicReports.Models
{
    [DataContract]
    public class ReportParameter
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string DisplayName { get; set; }

        [DataMember]
        public bool Required { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Value { get; set; }
        
        [DataMember]
        public bool IsValueList { get; set; }

        [DataMember]
        public IList<ReportParameterValue> ValueList { get; set; }
    }
}
