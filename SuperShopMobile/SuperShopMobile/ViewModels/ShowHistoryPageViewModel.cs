using Prism.Navigation;
using SuperShopMobile.Helpers;

namespace SuperShopMobile.ViewModels
{
    public class ShowHistoryPageViewModel : ViewModelBase
    {
        public ShowHistoryPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = Languages.ShowPurchaseHistory;
        }
    }
}
