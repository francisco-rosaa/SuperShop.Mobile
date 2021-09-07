using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Prism.Navigation;
using SuperShopMobile.Helpers;
using SuperShopMobile.ItemViewModels;
using SuperShopMobile.Models;
using SuperShopMobile.Views;

namespace SuperShopMobile.ViewModels
{
    public class SuperShopMasterDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public SuperShopMasterDetailPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            LoadMenus();
        }

        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

        private void LoadMenus()
        {
            List<Menu> menus = new List<Menu>
            {
                new Menu
                {
                    Icon = "ic_card_giftcard.png",
                    PageName = $"{nameof(ProductsPage)}",
                    Title = Languages.Products
                },
                new Menu
                {
                    Icon = "ic_shopping_cart",
                    PageName = $"{nameof(ShowCartPage)}",
                    Title = Languages.ShowShoppingCart
                },
                new Menu
                {
                    Icon = "ic_history",
                    PageName = $"{nameof(ShowHistoryPage)}",
                    Title = Languages.ShowPurchaseHistory,
                    IsLoginRequired = true
                },
                new Menu
                {
                    Icon = "ic_person",
                    PageName = $"{nameof(ModifyUserPage)}",
                    Title = Languages.ModifyUser,
                    IsLoginRequired = true
                },
                new Menu
                {
                    Icon = "ic_exit_to_app",
                    PageName = $"{nameof(LoginPage)}",
                    Title = Languages.Login
                }
            };

            Menus = new ObservableCollection<MenuItemViewModel>
                (menus.Select(x => new MenuItemViewModel(_navigationService)
                {
                    Icon = x.Icon,
                    PageName = x.PageName,
                    Title = x.Title,
                    IsLoginRequired = x.IsLoginRequired
                }).ToList());
        }
    }
}
