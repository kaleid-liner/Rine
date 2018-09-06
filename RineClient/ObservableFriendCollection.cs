using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace RineClient
{
    public class ObservableFriendCollection : ObservableCollection<User> 
    {
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (User item in e.OldItems)
                {
                    item.Messages.CollectionChanged -= ChildCollectionChanged;
                    item.PropertyChanged -= ChildPropertyChanged;
                }
            }
            if (e.NewItems != null)
            {
                foreach (User item in e.NewItems)
                {
                    item.Messages.CollectionChanged += ChildCollectionChanged;
                    item.PropertyChanged += ChildPropertyChanged;
                }
            }
            base.OnCollectionChanged(e);
        }

        private void ChildPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
            base.OnCollectionChanged(args);
        }

        private void ChildCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
            base.OnCollectionChanged(args);
        }

    }
}
