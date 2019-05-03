using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Jointly
{
    public partial class MainPage : ContentPage
    {
        public bool IsNormalScreenMode { get; set; }

        private Rectangle EntriesNormal { get; set; }
        
        private Rectangle EntriesLandscape { get; set; }

        private Rectangle MapFullNormal { get; set; }
        private Rectangle MapMinimizedNormal { get; set; }

        private Rectangle MapFullLandscape { get; set; }
        private Rectangle MapMinimizedLandscape { get; set; }

        public MainPage()
        {
            InitializeComponent();

            var icon = FileImageSource.FromResource("round_add_box_white_36dp");
            var mapButtonCommand = new Command(MapButton_Clicked);
            var createEventButton = new ToolbarItem
            {
                Command = mapButtonCommand,
                Icon = "round_add_box_white_36dp"
            };

            var adminButtonCommand = new Command(AdminButton_Clicked);
            var adminPageButton = new ToolbarItem
            {
                Command = adminButtonCommand,
                Icon = "outline_security_black_36dp"
            };

            ToolbarItems.Add(createEventButton);
            ToolbarItems.Add(adminPageButton);


            var cancelGesturer = new TapGestureRecognizer();
            cancelGesturer.Tapped += Cancel_Clicked;

            var confirmGesturer = new TapGestureRecognizer();
            confirmGesturer.Tapped += Confirm_Clicked;

            Cancel.GestureRecognizers.Add(cancelGesturer);
            Confirm.GestureRecognizers.Add(confirmGesturer);
            
            EventName.Focused += EventName_Focused;
            EventName.Unfocused += EventName_Unfocused;
            AdditionalInfo.Focused += AdditionalInfo_Focused;
            AdditionalInfo.Unfocused += AddititonalInfo_Unfocused;
            ConfirmButton.Clicked += Confirm_Clicked;
            CancelButton.Clicked += Cancel_Clicked;
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (width > height)
            {
                IsNormalScreenMode = false;

                if (MapFullLandscape.Width == 0)
                {
                    SetLandscapeRectangles();
                }

                if(EntriesInfo.Opacity == 1)
                {
                    EntriesInfo.Layout(EntriesLandscape);
                    Map.Layout(MapMinimizedLandscape);
                }
                else if(EntriesInfo.Opacity == 0)
                {
                    EntriesInfo.Layout(EntriesLandscape);
                    Map.Layout(MapFullLandscape);
                }
            }
            else
            {
                IsNormalScreenMode = true;

                if (MapFullNormal.Width == 0)
                {
                    SetNormalRectangles();
                }

                if (EntriesInfo.Opacity == 1)
                {
                    EntriesInfo.Layout(EntriesNormal);
                    Map.Layout(MapMinimizedNormal);
                }
                else if(EntriesInfo.Opacity == 0)
                {
                    EntriesInfo.Layout(EntriesNormal);
                    Map.Layout(MapFullNormal);
                }
            }
        }

        private void SetNormalRectangles()
        {
            EntriesNormal = new Rectangle(EntriesInfo.X, EntriesInfo.Y, EntriesInfo.Width, EntriesInfo.Height);
            MapFullNormal = new Rectangle(Map.X, Map.Y, Map.Width, Map.Height);
            MapMinimizedNormal = new Rectangle(Map.X, Map.Y + EntriesInfo.Height, Map.Width, Map.Height - EntriesInfo.Height);
        }

        private void SetLandscapeRectangles()
        {
            EntriesLandscape = new Rectangle(EntriesInfo.X, EntriesInfo.Y, EntriesInfo.Width / 2, EntriesInfo.Height);
            MapFullLandscape = new Rectangle(Map.X, Map.Y, Width, Height);
            MapMinimizedLandscape = new Rectangle(Map.Width / 2, Map.Y, Map.Width / 2, Map.Height);
        }

        private async void ChangeLayout()
        {
            if (EntriesInfo.Opacity == 1)
            {
                var rect = new Rectangle();
                if (IsNormalScreenMode)
                {
                    rect = MapFullNormal;
                }
                else
                {
                    rect = MapFullLandscape;
                }
                await Task.WhenAll(
                    EntriesInfo.FadeTo(0),
                    Map.LayoutTo(rect)
                    );
            }
            else if (EntriesInfo.Opacity == 0)
            {
                var rect = new Rectangle();
                if (IsNormalScreenMode)
                {
                    rect = MapMinimizedNormal;
                }
                else
                {
                    rect = MapMinimizedLandscape;
                }
                await Task.WhenAll(
                    EntriesInfo.FadeTo(1),
                    Map.LayoutTo(rect)
                    );
            }
        }

        private void MapButton_Clicked()
        {
            ChangeLayout();
        }

        private async void AdminButton_Clicked()
        {
            await Navigation.PushAsync(new AdminTabs());
        }

        private void EventName_Focused(object sender, EventArgs e)
        {
            if(EventName.Text == "Назва події")
            {
                EventName.Text = "";
                EventName.Opacity = 1;
            }
        }

        private void EventName_Unfocused(object sender, EventArgs e)
        {
            if (EventName.Text == "")
            {
                EventName.Text = "Назва події";
                EventName.Opacity = 0.5;
            }
        }

        private void AdditionalInfo_Focused(object sender, EventArgs e)
        {
            if (AdditionalInfo.Text == "Додатковий опис")
            {
                AdditionalInfo.Text = "";
                AdditionalInfo.Opacity = 1;
            }
        }

        private void AddititonalInfo_Unfocused(object sender, EventArgs e)
        {
            if (AdditionalInfo.Text == "")
            {
                AdditionalInfo.Text = "Додатковий опис";
                AdditionalInfo.Opacity = 0.5;
            }
        }

        private void Cancel_Clicked(object sender, EventArgs e)
        {
            ChangeLayout();

            EventName.Text = "Назва події";
            AdditionalInfo.Text = "Додатковий опис";
            StartPicker.Time = new TimeSpan();
            EndPicker.Time = new TimeSpan();
        }

        private void Confirm_Clicked(object sender, EventArgs e)
        {
            ChangeLayout();

            EventName.Text = "Назва події";
            AdditionalInfo.Text = "Додатковий опис";
            StartPicker.Time = new TimeSpan();
            EndPicker.Time = new TimeSpan();
        }
    }
}
