using Microsoft.Extensions.DependencyInjection;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using PrismBlank.ViewModels;
using PrismBlank.Views;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace PrismBlank
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override IContainerExtension CreateContainerExtension()
        {
            return ContainerLocator.Current;
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry cr)
        {
            cr.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            cr.RegisterForNavigation<NavigationPage>();
            cr.RegisterForNavigation<MainPage, MainPageViewModel>();
            cr.RegisterServices(s =>
            {
                s.AddHttpClient<MainPageViewModel>((a) =>
                {
                    a.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName));
                    a.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                    a.BaseAddress = new Uri("https://dummy.url/test");
                    a.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);
                }).ConfigurePrimaryHttpMessageHandler(x => new HttpClientHandler()
                {
                    AutomaticDecompression = DecompressionMethods.GZip
                });
            });
        }
    }
}
