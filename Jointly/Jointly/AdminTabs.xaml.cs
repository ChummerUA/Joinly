using Jointly.Renderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Jointly
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminTabs : TabbedPage
    {
        public string SearchText { get; set; } = "";

        public bool Iconified { get; set; } = true;

        public AdminTabs()
        {
            InitializeComponent();
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            
            for (int i = 0; i < Children.Count; i++)
            {
                if (Children[i] != CurrentPage)
                {
                    var oldPage = Children[i] as SearchPage;
                    oldPage.IsSearchBarEnabled = false;
                }
            }

            var current = CurrentPage as SearchPage;
            current.IsSearchBarEnabled = true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}