using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Windows.Browser;
using DynamicReports.Common;
using DynamicReports.Events;
using DynamicReports.Models;
using DynamicReports.ViewModels;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;

namespace DynamicReports.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            Init();
            WireUpCommands();
            WireUpEvents();
            LoadData();
        }

        private void WireUpEvents()
        {
        }

        private void WireUpCommands()
        {
            ExecuteReportCommand = new DelegateCommand(OnExecuteReportCommand, CanExecuteReportCommand);
        }

        private bool CanExecuteReportCommand()
        {
            return (SelectedReport != null);
        }

        private void OnExecuteReportCommand()
        {
            string parameters = "ReportName=" + SelectedReport.Name;

            var getReport = new WebClient();

            getReport.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
            var endpoint = new Uri(HtmlPage.Document.DocumentUri, SelectedReport.Endpoint);
            getReport.UploadStringCompleted += GetReportOnUploadStringCompleted;
            getReport.UploadStringAsync(endpoint, "POST", parameters);
        }

        private void GetReportOnUploadStringCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            if (e.Error != null) return;

            DynamicReport.UpdateModel(e.Result);
            eventAggregator.GetEvent<InitialiseDynamicColumnsEvent>().Publish(null);
            OnPropertyChanged("DynamicReport");
        }

        private void LoadData()
        {
            GetReportTypes();
        }

        private void Init()
        {
            ReportTypes = new ObservableCollection<ReportType>();
            DynamicReport = new DynamicReportViewModel();
        }

        private void GetReportTypes()
        {
            var getReportTypes = new WebClient();
            getReportTypes.OpenReadCompleted += GetReportTypes_Completed;
            getReportTypes.OpenReadAsync(ServiceDirectory.GetReports);
        }

        void GetReportTypes_Completed(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Error != null) return;

            var sr = new StreamReader(e.Result);
            var jsonStr = sr.ReadToEnd();
            var json = JsonSerializer.Deserialize<List<ReportType>>(jsonStr);

            ReportTypes.Clear();
            json.ForEach(x => ReportTypes.Add(x));
        }

        public DelegateCommand ExecuteReportCommand { get; private set; }
        public ObservableCollection<ReportType> ReportTypes { get; private set; }
        public DynamicReportViewModel DynamicReport { get; private set; }

        private ReportType selectedReport;
        private IEventAggregator eventAggregator;

        public ReportType SelectedReport
        {
            get { return selectedReport; }

            set
            {
                if (selectedReport == value) return;
                selectedReport = value;
                OnPropertyChanged("SelectedReport");
                ExecuteReportCommand.RaiseCanExecuteChanged();
            }
        }
    }
}
