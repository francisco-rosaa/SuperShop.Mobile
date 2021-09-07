using Prism.Navigation;
using SuperShopMobile.Helpers;

namespace SuperShopMobile.ViewModels
{
    public class ModifyUserPageViewModel : ViewModelBase
    {
        public ModifyUserPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = Languages.ModifyUser;
        }
    }
}
