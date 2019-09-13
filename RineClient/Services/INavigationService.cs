using System;

namespace RineClient.Services
{
    public interface INavigationService
    {
        bool Navigate<TViewModel>(object parameter = null);

        bool Navigate(Type viewModelType, object parameter = null);

        void GoBack();
    }
}
