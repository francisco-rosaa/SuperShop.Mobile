using Prism;
using Prism.Ioc;
using Syncfusion.Licensing;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;
using SuperShopMobile.Services;
using SuperShopMobile.ViewModels;
using SuperShopMobile.Views;

namespace SuperShopMobile
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            SyncfusionLicenseProvider.RegisterLicense("NDgyMzE1QDMxMzkyZTMyMmUzME40aWJaYzlCSmkyb2VWaUsvY3JyWk5Xd3JnaVZmTW5GTm9nNHpZMzFVdGs9");
            
            InitializeComponent();

            await NavigationService.NavigateAsync
                ($"/{nameof(SuperShopMasterDetailPage)}/NavigationPage/{nameof(ProductsPage)}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<ProductsPage, ProductsPageViewModel>();
            containerRegistry.RegisterForNavigation<ProductDetailPage, ProductDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<SuperShopMasterDetailPage, SuperShopMasterDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<ModifyUserPage, ModifyUserPageViewModel>();
            containerRegistry.RegisterForNavigation<ShowHistoryPage, ShowHistoryPageViewModel>();
            containerRegistry.RegisterForNavigation<ShowCartPage, ShowCartPageViewModel>();
        }
    }
}
