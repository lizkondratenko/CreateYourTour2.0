using CreateYourTour.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CreateYourTour.Model
{
    static class TourModel
    {
        
        private static bool IsAllFieldsNotNull(string Country, string City, string Hotel, string DateFrom, string DateTo, int AmountOfPeople)
        {
            //REVIEW: Строки не проверяются на пустоту с помощью == или !=
            if ((Country != "") && (City != "") && (Hotel != "") && (AmountOfPeople != 0) && (DateFrom != "") && (DateTo != ""))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static void CreatePersonalTicket(string Country, string City, string Hotel, string DateFrom, string DateTo, int AmountOfPeople)
        {
            if (IsAllFieldsNotNull(Country, City, Hotel, DateFrom, DateTo, AmountOfPeople))
            {
                Ticket ticket = new Ticket(Country, City, Hotel, DateFrom, DateTo, AmountOfPeople);
                ticket.CreateTicketToWordFile();
            }
            else
            {
                MessageBox.Show("Вы заполнили не все поля!");
            }
        }
    }
}