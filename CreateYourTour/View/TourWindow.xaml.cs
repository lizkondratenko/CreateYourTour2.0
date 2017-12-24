using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using CreateYourTour.Model;

namespace CreateYourTour.View
{
    /// <summary>
    /// Логика взаимодействия для TourWindow.xaml
    /// </summary>
    public partial class TourWindow : System.Windows.Window
    { 
        public TourWindow()
        {
            InitializeComponent();

            //REVIEW: Ticket - это кто? Почему это не делается через биндинг? Ticket не может быть null? 
            tblTourIntro.Text = "Здравствуйте " + Ticket.FIO + "! Подберите себе лучший тур.";
        }
    }
}
