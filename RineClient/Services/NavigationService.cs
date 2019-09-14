using System;
using System.Collections.Concurrent;
using Windows.UI.Xaml.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RineClient.Services
{
    public class NavigationService : INavigationService
    {
        private static readonly ConcurrentDictionary<Type, Type> _viewModelViewPairs = new ConcurrentDictionary<Type, Type>();

        public NavigationService(Frame frame)
        {
            _frame = frame;
        }

        static public void Register<TViewModel, TView>() where TView: Page
        {
            if (_viewModelViewPairs.TryAdd(typeof(TViewModel), typeof(TView)))
            {
                throw new InvalidOperationException($"ViewModel already registered '{typeof(TViewModel).FullName}'");
            }
        }

        static public Type GetView(Type viewModelType)
        {
            if (_viewModelViewPairs.TryGetValue(viewModelType, out Type viewType))
            {
                return viewType;
            }
            else
            {
                throw new InvalidOperationException($"View not registered for ViewModel '{viewModelType.FullName}'");
            }
        }

        static public Type GetView<TViewModel>()
        {
            return GetView(typeof(TViewModel));
        }

        private Frame _frame;

        public void GoBack() => _frame?.GoBack();

        public bool Navigate<TViewModel>(object parameter = null)
        {
            return Navigate(typeof(TViewModel), parameter);
        }

        public bool Navigate(Type viewModelType, object parameter = null)
        {
            if (viewModelType == null)
            {
                throw new InvalidOperationException("Navigation frame not initialized.");
            }
            return _frame.Navigate(viewModelType, parameter);
        }

        public void Initialize(Frame frame)
        {
            _frame = frame;
        }
    }
}
