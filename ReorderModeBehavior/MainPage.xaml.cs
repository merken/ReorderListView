using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace ReorderModeBehavior
{
    public abstract class BindableBase : INotifyPropertyChanged
    {
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }
    }
    public class User : BindableBase
    {
        private string name;
        public String Name
        {
            get
            {
                return name;
            }
            set
            {
                this.name = value;
                RaisePropertyChanged();
            }
        }
        private string lastName;
        public String LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                this.lastName = value;
                RaisePropertyChanged();
            }
        }
        private int order;
        public int Order
        {
            get
            {
                return order;
            }
            set
            {
                this.order = value;
                RaisePropertyChanged();
            }
        }
    }

    public class MainPageViewModel : BindableBase
    {
        private User selectedUser;
        public User SelectedUser
        {
            get
            {
                return selectedUser;
            }
            set
            {
                this.selectedUser = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<User> users;
        public ObservableCollection<User> Users
        {
            get
            {
                return users;
            }
            set
            {
                this.users = value;
                RaisePropertyChanged();
            }
        }
    }

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
            MainPageViewModel vm = new MainPageViewModel();
            vm.Users = new ObservableCollection<User>
            {
                new User{ Name = "Maarten",  LastName="Merken", Order=1},
                new User{ Name = "John",  LastName="Doe", Order=2},
                new User{ Name = "Jane",  LastName="Doe", Order=3},
                new User{ Name = "Jimmy",  LastName="Doe", Order=4},
                new User{ Name = "Joey",  LastName="Doe", Order=5},
                new User{ Name = "Jin",  LastName="Doe", Order=6}
            };

            vm.Users.CollectionChanged += (s, ea) =>
            {
                //The collection has been reordered!
                int order = 1;
                foreach (var user in vm.Users)
                {
                    user.Order = order++;
                }
            };

            this.DataContext = vm;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }
    }
}
