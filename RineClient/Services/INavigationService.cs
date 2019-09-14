using System;
using Windows.UI.Xaml.Controls;

namespace RineClient.Services
{
    public interface INavigationService
    {
        bool Navigate<TViewModel>(object parameter = null);

        bool Navigate(Type viewModelType, object parameter = null);

        void GoBack();

        void Initialize(Frame frame);
    }
}
