using DGraficLib;
using DGraficLib.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LabaratoryWork3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int HEXAGON_START_SIZE = 50;
        private readonly WpfRender render;
        private readonly Figure2D figure2D;
        private readonly TickExampleController tickExampleController;
        private readonly System.Windows.Threading.DispatcherTimer dispatcherTimer;
        public MainWindow()
        {
            InitializeComponent();
            render = new WpfRender(renderPanel);
            figure2D = new Figure2D(
                VertexHelper.GenerateHexagon(40, HEXAGON_START_SIZE), 
                new Transform2D() { Position = 
                new System.Drawing.PointF(HEXAGON_START_SIZE, 
                HEXAGON_START_SIZE), Rotation = 45 }, 
                System.Drawing.Color.Red);
            tickExampleController = new TickExampleController(new WpfPanel(renderPanel), figure2D, 20f);
            
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += RenderTick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 5);
            StartTimer();
        }

        private DateTime lastFrameTime = DateTime.Now;
        private void RenderTick(object sender, EventArgs e)
        {
            var nowDateTime = DateTime.Now;
            var deltaTime = nowDateTime.Ticks - lastFrameTime.Ticks;
            render.Clear();
            tickExampleController.OnTick(deltaTime);
            render.Show(figure2D);
            lastFrameTime = nowDateTime;
        }

        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            if(dispatcherTimer.IsEnabled)
            {
                StopTimer();
            }
            else
            {
                StartTimer();
            }
        }

        private void StopTimer()
        {
            dispatcherTimer.Stop();
            stopButton.Content = "Start";
        }

        private void StartTimer()
        {
            dispatcherTimer.Start();
            stopButton.Content = "Stop";
        }

        private void settingsButton_Click(object sender, RoutedEventArgs e)
        {
            new SettingsScreen(figure2D).ShowDialog();
        }

        private void openTask2Button_Click(object sender, RoutedEventArgs e)
        {
            new Task2Window().Show();
        }

        private void openTask3Button_Click(object sender, RoutedEventArgs e)
        {
            new Task3Window().Show();
        }
    }
}
