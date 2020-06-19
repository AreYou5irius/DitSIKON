using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using SIKONClassLibrary;
using SIKONClassLibrary.EventHandlers;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SIKONClient
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProgramView : Page
    {
        private List<Event> Events { get; set; }
        private List<TimeToEvent> TimeToEvents { get; set; }
        private List<Room> Rooms { get; set; }
        private int ElementWidth { get; set; }
        private int ElementHeight { get; set; }
        private int TimeStart { get; set; }
        private int TimeEnd { get; set; }

        public ProgramView()
        {
            this.InitializeComponent();

            Events = new EventsHandler().Read();
            TimeToEvents = new TimeToEventHandler().Read();
            Rooms = new RoomHandler().Read();

            ElementWidth = 100;
            ElementHeight = 60;

            TimeStart = 8;
            TimeEnd = 20;

            InitializeLists();

            ProgramCanvas.Width = Rooms.Count * ElementWidth;
            ProgramCanvas.Height = ElementHeight * (TimeEnd - TimeStart);

            RoomCanvas.Width = Rooms.Count * ElementWidth;
            RoomCanvas.Height = ElementHeight;

            TimeCanvas.Width = ElementWidth;
            TimeCanvas.Height = ElementHeight * (TimeEnd - TimeStart);

            DrawFrame();

            DrawProgram();

            DrawTimes();
        }

        private void InitializeLists()
        {
            foreach (var ev in Events)
            foreach (var ro in Rooms)
                if (ev.Room_ID == ro.ID)
                    ev.Room = ro;

            foreach (var ev in Events)
            foreach (var ti in TimeToEvents)
                if (ev.ID == ti.Event_ID)
                {
                    ev.TimeToEvent.Add(ti);
                    ev.Time = ti;
                }
        }

        private void DrawFrame()
        {

            foreach (var ro in Rooms)
            {
                var shape = new TextBlock();
                shape.Width = ElementWidth;
                shape.Text = ro.Name;
                shape.Translation = new Vector3(ro.ID*ElementWidth-ElementWidth,0,0);

                RoomCanvas.Children.Add(shape);
            }

        }

        private void DrawProgram()
        {
            foreach (var ev in Events)
            {
                if (ev.Time != null && ev.Room != null)
                {
                    var shape = new Button();
                    shape.VerticalAlignment = VerticalAlignment.Top;
                    shape.HorizontalAlignment = HorizontalAlignment.Left;
                    shape.Width = ElementWidth;
                    shape.Height = ElementHeight;
                    int time = (ev.Time.Time.Hour - TimeStart) * ElementHeight + ev.Time.Time.Minute;
                    //int time = 100;
                    int r = ev.Room.ID * ElementWidth - ElementWidth;
                    shape.Content = ev.Subject;
                    shape.Background = new SolidColorBrush(Colors.DarkOrange);

                    shape.Translation = new Vector3(r, time, 0);

                    ProgramCanvas.Children.Add(shape);
                }
            }
        }

        private void DrawTimes()
        {

            for (int i = TimeStart; i < TimeEnd; i++)
            {
                var shape = new TextBlock();
                shape.Height = ElementHeight;
                shape.Width = ElementWidth;
                shape.Text = $"KL{i}:00";
                shape.Translation = new Vector3(0, i * ElementHeight - TimeStart * ElementHeight, 0);

                TimeCanvas.Children.Add(shape);
            }
        }
    }
}
