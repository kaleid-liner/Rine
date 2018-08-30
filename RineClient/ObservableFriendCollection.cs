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
                }
            }
            if (e.NewItems != null)
            {
                foreach (User item in e.NewItems)
                {
                    item.Messages.CollectionChanged += ChildCollectionChanged;
                }
            }
            base.OnCollectionChanged(e);
        }

        private void ChildCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
            base.OnCollectionChanged(args);
        }
    }
}
