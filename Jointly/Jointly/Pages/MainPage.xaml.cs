using Jointly.ViewModels;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Jointly.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private double prevPanY = -1;
        private double currentPanY = 0;
        private double deltaPanY = 0;
        private double panelY;

        public MainPage()
        {
            InitializeComponent();
        }

        private void container_SizeChanged(object sender, EventArgs e)
        {
            if(container.Height > 0)
            {
                container.SizeChanged -= container_SizeChanged;
                NewEvent_ExitAction.Y = container.Height;
                NewEvent_EnterAction.Y = container.Height - NewEventPanel.Height;
                NewEventPanel.TranslationY = container.Height;
            }
        }

        private void NewEvent_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            if(BindingContext is MainViewModel vm && vm.Current == Stage.NewEvent)
            {
                var grid = sender as Grid;

                switch (e.StatusType)
                {
                    case GestureStatus.Canceled:
                    case GestureStatus.Completed:
                        if(deltaPanY > 0)
                        {
                            vm.Current = Stage.Map;
                        }
                        else
                        {
                            var anim = new Animation(a => NewEventPanel.TranslationY = a, NewEventPanel.TranslationY, Height - NewEventPanel.Height);
                            anim.Commit(NewEventPanel, "CancelPan", length: 50);
                        }
                        ResetPan();
                        break;
                    case GestureStatus.Started:
                        panelY = grid.TranslationY;
                        break;
                    case GestureStatus.Running:
                        if(prevPanY == -1)
                        {
                            prevPanY = e.TotalY;
                        }
                        else
                        {
                            prevPanY = currentPanY;
                        }

                        currentPanY = e.TotalY;
                        deltaPanY += currentPanY - prevPanY;
                        var y0 = grid.TranslationY;
                        var y = y0 + deltaPanY;
                        (sender as Grid).TranslationY = Math.Max(y, Height - grid.Height);

                        break;
                }
            }
        }

        private void ResetPan()
        {
            prevPanY = -1;
            currentPanY = 0;
            deltaPanY = 0;
        }
    }

    public class NewEvent_EnterAction : TriggerAction<Grid>
    {
        public double Y { get; set; }
        protected override void Invoke(Grid sender)
        {
            var anim = new Animation(a => sender.TranslationY = a, sender.TranslationY, Y);
            anim.Commit(sender, nameof(NewEvent_EnterAction));
        }
    }

    public class NewEvent_ExitAction : TriggerAction<Grid>
    {
        public double Y { get; set; }
        protected override void Invoke(Grid sender)
        {
            var anim = new Animation(a => sender.TranslationY = a, sender.TranslationY, Y);
            anim.Commit(sender, nameof(NewEvent_ExitAction));
        }
    }
}