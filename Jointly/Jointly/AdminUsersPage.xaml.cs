using Jointly.Models;
using Jointly.Renderers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Jointly
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminUsersPage : SearchPage
    {
        public ObservableCollection<UserModel> Users { get; set; }

        public AdminUsersPage()
        {
            InitializeComponent();

            Users = new ObservableCollection<UserModel>();
            this.BindingContext = this;

            SearchPlaceHolderText = "Пошук";

            Users.CollectionChanged += Users_CollectionChanged;
            SetTestList();
        }

        private void Users_CollectionChanged(object sender, EventArgs e)
        {
            UsersListView.ItemsSource = Users;
        }

        private void SetTestList()
        {
            Users.Add(new UserModel
            {
                ID = 1,
                EMail = "dsifhasfjkhsadljk@gmail.com",
                Name = "User1",
                Role = "user",
                Registration = DateTime.Today.Date.ToString()
            });
            Users.Add(new UserModel
            {
                ID = 2,
                EMail = "dhfgdhdhgk@gmail.com",
                Name = "User2",
                Role = "user",
                Registration = DateTime.Today.Date.ToString()
            });
            Users.Add(new UserModel
            {
                ID = 3,
                EMail = "uihyjldfsgljjksdfg@gmail.com",
                Name = "User3",
                Role = "user",
                Registration = DateTime.Today.Date.ToString()
            });
        }
	}
}