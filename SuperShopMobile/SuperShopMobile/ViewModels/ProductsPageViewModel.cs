using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Prism.Commands;
using Prism.Navigation;
using SuperShopMobile.Models;
using SuperShopMobile.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SuperShopMobile.ViewModels
{
    public class ProductsPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;

        private ObservableCollection<ProductResponse> _products;
        private List<ProductResponse> _myProducts;
        private bool _isRunning;
        private string _search;
        private DelegateCommand _searchCommand;


        public ProductsPageViewModel(INavigationService navigationService, IApiService apiService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = "Products";
            LoadProductsAsync();
        }

        public ObservableCollection<ProductResponse> Products
        {
            get => _products;
            set => SetProperty(ref _products, value);
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public string Search
        {
            get => _search;

            set
            {
                SetProperty(ref _search, value);
                ShowProducts();
            }
        }

        public DelegateCommand SearchCommand =>
            _searchCommand ?? (_searchCommand = new DelegateCommand(ShowProducts));

        private async void LoadProductsAsync()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await App.Current.MainPage.DisplayAlert("Error", "No Internet connection", "Accept");
                });

                return;
            }

            IsRunning = true;

            string url = App.Current.Resources["ApiBaseUrl"].ToString();
            string prefix = App.Current.Resources["ApiServicePrefix"].ToString();
            string controller = App.Current.Resources["ApiProductsController"].ToString();

            Response response = await _apiService.GetListAsync<ProductResponse>(url, prefix, controller);

            IsRunning = false;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return;
            }

            _myProducts = (response.Result as List<ProductResponse>).OrderBy(x => x.Name).ToList();
            ShowProducts();
        }

        private void ShowProducts()
        {
            if (string.IsNullOrEmpty(Search))
            {
                Products = new ObservableCollection
                    <ProductResponse>(_myProducts);
            }
            else
            {
                Products = new ObservableCollection
                    <ProductResponse>(_myProducts.Where(x => x.Name.ToLower().Contains(Search.ToLower())));
            }
        }

    }
}
