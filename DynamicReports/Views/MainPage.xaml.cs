using DynamicReports.Events;
using DynamicReports.ViewModels;
using Microsoft.Practices.Prism.Events;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;

namespace DynamicReports.Views
{
    public partial class MainPage
    {
        private readonly IEventAggregator _eventAggregator;

        public MainPage()
        {
            StyleManager.ApplicationTheme = new Windows8Theme();

            InitializeComponent();

            _eventAggregator = new EventAggregator();
            _eventAggregator.GetEvent<InitialiseDynamicColumnsEvent>().Subscribe(InitialiseDynamicColumns);
            this.DataContext = new MainPageViewModel(_eventAggregator); 
        }

        public void InitialiseDynamicColumns(object o)
        {

            var dynamicReport = ((MainPageViewModel)this.DataContext).DynamicReport;
            DynamicReportGrid.Columns.Clear();
            this.DynamicReportGrid.IsFilteringAllowed = true;
            if (dynamicReport.Items.Count == 0) return;

            foreach (var column in dynamicReport.Items[0].GetProperties())
            {
                DynamicReportGrid.Columns.Add(new GridViewDataColumn()
                                                  {
                                                      Header = column.Name,
                                                      UniqueName = column.Name,
                                                      IsFilterable = true,
                                                      ShowFieldFilters = true,
                                                      ShowFilterButton = true,
                                                      ShowDistinctFilters = true,
                                                      IsGroupable = true,
                                                      IsSortable = true
                                                  });
            }
        }
    }
}
