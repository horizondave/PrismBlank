using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace PrismBlank.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        HttpClient _http;
        public MainPageViewModel(INavigationService navigationService, HttpClient http)
            : base(navigationService)
        {
            _http = http;
            Title = "Main Page";
        }
    }
}
