using DGraficLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LabaratoryWork3
{
    /// <summary>
    /// Interaction logic for SettingsScreen.xaml
    /// </summary>
    public partial class SettingsScreen : Window
    {
        private readonly Figure2D figure;

        public SettingsScreen(Figure2D figure)
        {
            InitializeComponent();
            this.figure = figure;
            sizeSlider.Value = figure.Transform.Scale.X;
        }

        private void changeColorButton_Click(object sender, RoutedEventArgs e)
        {
            var colorDialog = new ColorDialog();

            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                figure.BackgroundColor = colorDialog.Color;
            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            figure.Transform.Scale = new System.Drawing.PointF((float)e.NewValue, (float)e.NewValue);
        }
    }
}
