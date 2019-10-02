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
    public partial class AuthorizationPage : ContentPage
    {
        public AuthorizationPage()
        {
            InitializeComponent();
        }

        private void Entry_Completed(object sender, EventArgs e)
        {
            if(sender is Entry entry)
            {
                if(entry.ReturnCommandParameter is Entry next)
                {
                    next.Focus();
                }
                else if(entry.ReturnCommand != null)
                {
                    entry.ReturnCommand.Execute(null);
                }
            }
        }
    }

    public class StageEnterAction : TriggerAction<VisualElement>
    {
        protected override void Invoke(VisualElement sender)
        {
            var anim = new Animation
            {
                { 0.5, 1, new Animation(a => sender.Opacity = a, sender.Opacity, 0.8) },
            };
            anim.Commit(sender, nameof(StageEnterAction), length:400);
        }
    }

    public class ExitStageAction : TriggerAction<VisualElement>
    {
        protected override void Invoke(VisualElement sender)
        {
            var anim = new Animation
            {
                { 0, 0.5, new Animation(a => sender.Opacity = a, sender.Opacity, 0) },
            };
            anim.Commit(sender, nameof(ExitStageAction), length:400);
        }
    }
}