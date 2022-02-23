using System.Collections.ObjectModel;
using System.ComponentModel;
using WeatherWpfMvvmApp.Model;
using WeatherWpfMvvmApp.ViewModel.Commands;
using WeatherWpfMvvmApp.ViewModel.Helpers;

namespace WeatherWpfMvvmApp.ViewModel
{
    public class WeatherViewModel : INotifyPropertyChanged
    {
        private string query;

        public string Query
        {
            get { return query; }
            set
            {
                query = value;
                OnPropertyChanged(nameof(Query));
            }
        }

        private CurrentConditions currentConditions;

        public CurrentConditions CurrentConditions
        {
            get { return currentConditions; }
            set
            {
                currentConditions = value;
                OnPropertyChanged(nameof(CurrentConditions));
            }
        }

        private City selectedCity;

        public City SelectedCity
        {
            get { return selectedCity; }
            set
            {
                selectedCity = value;
                OnPropertyChanged(nameof(SelectedCity));
                GetCurrentCondition();
            }
        }

        public ObservableCollection<City> Cities { get; set; }

        public SearchCommand SearchCommand { get; set; }

        public WeatherViewModel()
        {
            // Show binding value is only in design mode.
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                selectedCity = new()
                {
                    LocalizedName = "Rayong"
                };
                CurrentConditions = new()
                {
                    WeatherText = "Parly cloudy",
                    Temperature = new()
                    {
                        Metric = new Units()
                        {
                            Value = "30"
                        }
                    }
                };
            }

            SearchCommand = new(this);
            Cities = new ObservableCollection<City>();
        }

        private async void GetCurrentCondition()
        {
            Query = string.Empty;

            if (Cities.Count > 0)
                Cities.Clear();

            if(SelectedCity != null && !string.IsNullOrWhiteSpace(SelectedCity.Key))
                CurrentConditions = await AccuWeatherHelper.GetCurrentConditions(SelectedCity.Key);
        }

        public async void MakeQuery()
        {
            var cities = await AccuWeatherHelper.GetCitiesAsync(Query);

            if (Cities.Count > 0)
                Cities.Clear();

            foreach (var city in cities)
            {
                Cities.Add(city);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
