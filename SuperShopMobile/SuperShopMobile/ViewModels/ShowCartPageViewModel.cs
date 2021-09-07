using Prism.Navigation;
using SuperShopMobile.Helpers;

namespace SuperShopMobile.ViewModels
{
    public class ShowCartPageViewModel : ViewModelBase
    {
        public ShowCartPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = Languages.ShowShoppingCart;
        }
    }
}
