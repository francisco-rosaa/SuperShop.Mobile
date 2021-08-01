using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Essentials;
using Xamarin.Forms;
using SuperShopMobile.ItemViewModels;
using SuperShopMobile.Models;
using SuperShopMobile.Services;

namespace SuperShopMobile.ViewModels
{
    public class ProductsPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;

        private ObservableCollection<ProductItemViewModel> _products;
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

        public ObservableCollection<ProductItemViewModel> Products
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
                Products = new ObservableCollection<ProductItemViewModel>
                    (_myProducts.Select(x => new ProductItemViewModel(_navigationService)
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Price = x.Price,
                        ImageUrl = x.ImageUrl,
                        LastPurchase = x.LastPurchase,
                        LastSale = x.LastSale,
                        IsAvailable = x.IsAvailable,
                        Stock = x.Stock,
                        User = x.User,
                        ImageFullPath = x.ImageFullPath
                    })
                    .ToList());
            }
            else
            {
                Products = new ObservableCollection<ProductItemViewModel>
                    (_myProducts.Select(x => new ProductItemViewModel(_navigationService)
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Price = x.Price,
                        ImageUrl = x.ImageUrl,
                        LastPurchase = x.LastPurchase,
                        LastSale = x.LastSale,
                        IsAvailable = x.IsAvailable,
                        Stock = x.Stock,
                        User = x.User,
                        ImageFullPath = x.ImageFullPath
                    })
                    .Where(x => x.Name.ToLower().Contains(Search.ToLower()))
                    .ToList());
            }
        }
    }
}
