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
    public partial class AdminEventsPage : SearchPage
    {
        private ObservableCollection<EventModel> Events { get; set; }

		public AdminEventsPage ()
		{
			InitializeComponent ();

            Events = new ObservableCollection<EventModel>();

            this.BindingContext = this;

            SearchPlaceHolderText = "Пошук";

            Events.CollectionChanged += Events_CollectionChanged;
            SetTestList();
		}

        private void Events_CollectionChanged(object sender, EventArgs e)
        {
            EventsListView.ItemsSource = Events;
        }

        private void SetTestList()
        {
            Events.Add(new EventModel
            {
                Name = "Event1",
                CreationTime = DateTime.Today,
                StartTime = new TimeSpan(0, 0, 0).ToString(),
                EndTime = new TimeSpan(23, 59, 59).ToString()
            });
            Events.Add(new EventModel
            {
                Name = "Event2",
                CreationTime = DateTime.Today,
                StartTime = new TimeSpan(0, 0, 0).ToString(),
                EndTime = new TimeSpan(23, 59, 59).ToString()
            });
            Events.Add(new EventModel
            {
                Name = "Event3",
                CreationTime = DateTime.Today,
                StartTime = new TimeSpan(0, 0, 0).ToString(),
                EndTime = new TimeSpan(23, 59, 59).ToString()
            });
        }
    }
}