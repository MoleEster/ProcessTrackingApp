using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using ProcessTrackigWPF.Models.PlotModel;
using ProcessTrackingApp.Data.SavingData;
using ProcessTrackingData;
using ProcessTrackingData.Data.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Threading;

namespace ProcessTrackigWPF.ViewModels
{
    class ReportPageViewModel: ViewModel
    {
        private static DispatcherTimer timer;
        public ReportPageViewModel()
        {
            timer = new DispatcherTimer();
            timer.Tick += GetProcesses;
            timer.Start();
            ReportGrapf = CreatePlotModel();
        }

        public PlotModel ReportGrapf { get;private set; }

        private List<TrackingProcess> _processes;
        public List<TrackingProcess> Processes
        {
            get
            {
                return _processes;
            }
        }
        private PlotModel CreatePlotModel()
        {
            
            var model = new PlotModel();
            var verticalAxis = new LinearAxis { Position = AxisPosition.Left, Minimum = 0, Maximum = 1 };
            model.Axes.Add(verticalAxis);
            model.Series.Add(new LineSeries());
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom });
            JsonWorker.GetDataFromFile(x => x.Date == DateTime.Today);
            model.Create24HourGraff(JsonWorker.GetDataFromFile(x => x.Date == new DateTime(0001, 1, 1)));
            return model;
        }

        private async void GetProcesses(object sender,EventArgs e)
        {
            var result = await Process.GetProcesses().GetTrackingProcessesAsync();
            result = result.CleanFromExitedProcesses();
            _processes = result.ToList();
            OnPropertyChanged(nameof(Processes));
        }

    }
}
