using Android.Content.Res;
using Android.Graphics;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Views.InputMethods;
using Jointly.Droid.Renderers;
using Jointly.Renderers;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using static Android.Views.View;
using SearchView = Android.Support.V7.Widget.SearchView;

[assembly: ExportRenderer(typeof(SearchPage), typeof(SearchPageRenderer))]
namespace Jointly.Droid.Renderers
{
    public class SearchPageRenderer : PageRenderer
    {
        private static SearchView _searchView;

        private static bool OrientationChanged { get; set; }

        public static Android.Support.V7.Widget.Toolbar ToolBar { get; set; }

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            OrientationChanged = true;

            SetToolbar();
        }

        private void SetToolbar()
        {
            var inflater = LayoutInflater.From(MainActivity.Current);

            var toolbarView = inflater.Inflate(Resource.Layout.Toolbar, null);
            var toolbar = MainActivity.Current.FindViewById<Android.Support.V7.Widget.Toolbar>(toolbarView.Id);

            MainActivity.ToolBar = toolbar;

            var page = Element as SearchPage;

            if (page.IsSearchBarEnabled)
            {
                if (!OrientationChanged)
                {
                    OrientationChanged = true;
                    return;
                }

                AddSearchToToolBar();

                OrientationChanged = false;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == "IsSearchBarEnabled")
            {
                var page = Element as SearchPage;
                var parent = page.Parent as AdminTabs;

                if (page.IsSearchBarEnabled)
                {
                    AddSearchToToolBar();
                    MainActivity.ToolBar.Title = Element.Title;

                    _searchView.SetQuery(parent.SearchText, false);
                    _searchView.Iconified = parent.Iconified;
                }
                else
                {
                    if (_searchView != null)
                    {
                        parent.Iconified = _searchView.Iconified;
                    }

                    MainActivity.ToolBar?.Menu?.RemoveItem(Resource.Menu.mainmenu);

                    parent.SearchText = page.SearchText;
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (_searchView != null)
            {
                _searchView.QueryTextChange -= searchView_QueryTextChange;
            }
            base.Dispose(disposing);
        }

        private void AddSearchToToolBar()
        {
            if (MainActivity.ToolBar == null || Element == null)
            {
                return;
            }

            MainActivity.ToolBar.InflateMenu(Resource.Menu.mainmenu);

            _searchView = MainActivity.ToolBar.Menu?.FindItem(Resource.Id.action_search)?.ActionView?.JavaCast<SearchView>();

            if (_searchView == null)
            {
                return;
            }

            _searchView.QueryTextChange += searchView_QueryTextChange;
            _searchView.SubmitButtonEnabled = false;
            _searchView.QueryHint = (Element as SearchPage)?.SearchPlaceHolderText;
            _searchView.SetOnQueryTextFocusChangeListener(new FocusChangeListenerClass());
            _searchView.SetIconifiedByDefault(true);
            _searchView.Clickable = true;
            _searchView.ImeOptions = (int)ImeAction.Search;
            _searchView.InputType = (int)InputTypes.TextVariationNormal;
            _searchView.MaxWidth = int.MaxValue;        //Hack to go full width - http://stackoverflow.com/questions/31456102/searchview-doesnt-expand-full-width
            if ((Element as SearchPage).SearchText != "")
            {
                _searchView.SetQuery((Element as SearchPage).SearchText, true);
            }
            MainActivity.ToolBar.Title = Element.Title;
        }

        private void searchView_QueryTextChange(object sender, SearchView.QueryTextChangeEventArgs e)
        {
            var searchPage = Element as SearchPage;
            if (searchPage == null)
            {
                return;
            }
            searchPage.SearchText = e?.NewText;
        }

        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            base.OnSizeChanged(w, h, oldw, oldh);
            
            if (oldh == 0 || oldw == 0)
            {
                return;
            }
            SetToolbar();
        }
    }

    public class FocusChangeListenerClass : Java.Lang.Object, IOnFocusChangeListener
    {
        public void OnFocusChange(Android.Views.View v, bool hasFocus)
        {
            var searchView = v as SearchView;
            if (hasFocus == false && searchView.Query == "")
            {
                searchView.Iconified = true;
            }
            if (hasFocus == true && searchView.Query != "")
            {
                searchView.Iconified = false;
            }
        }
    }
}
