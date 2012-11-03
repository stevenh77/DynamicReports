using System.Collections.ObjectModel;
using System.Json;
using DynamicReports.Models;
using DynamicReports.ViewModels;

namespace DynamicReports.ViewModels
{
    public class DynamicReportViewModel : ViewModelBase
    { 
        public ObservableCollection<CustomType> Items { get; private set; }

        public DynamicReportViewModel()
        {
            Items = new ObservableCollection<CustomType>();
        }

        public void UpdateModel(string json)
        {
            Items.Clear();

            var jsonArray = JsonValue.Parse(json) as JsonArray;

            if (jsonArray == null) return;

            var template = jsonArray[0] as JsonObject;

            if (template == null) return;

            var jsonHelper = new JsonHelper<CustomType>(template);

            foreach (var item in jsonArray)
            {
                var customType = new CustomType();

                jsonHelper.MapJsonObject(customType.SetPropertyValue, item);
                Items.Add(customType);
            }
        }
    }
}
