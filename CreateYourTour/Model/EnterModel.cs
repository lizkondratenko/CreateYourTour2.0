using CreateYourTour.View;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CreateYourTour.Model
{
    static class EnterModel
    {
        public static TourWindow tourWindow;

        private static bool tourWindowWasShowed = false;
        public static void Tour_Click()
        {
            if (!tourWindowWasShowed)
            {
                tourWindowWasShowed = true;
                tourWindow = new TourWindow();
                tourWindow.ShowDialog();
            }
            else
            {
                tourWindow = new TourWindow();
                tourWindow.ShowDialog();
            }
        }
        public static void Close_Tour_Window()
        {
            tourWindow.Close();
        }
        private static string whatIsEmpty(string login, string password)
        {
            //REVIEW: Строка не проверяется на пустоту с помощью !=""
            if (login != "")
            {
                if (password != "")
                {
                    return null;
                }
                else return "Пароль";
            }
            else return "Логин";

        }

        private static bool CheckLogPass(string login, string password, ref string userName, ref string birthday)
        {
            string Password = ConnectionToDB.getPassword(login);
            
            userName = ConnectionToDB.getNameOrBirthday("Name", login, password);
            
            birthday = ConnectionToDB.getNameOrBirthday("Birthday", login, password);
                
            return Password.Equals(password) ? true : false;
        }

        public static void EnterLogin(string login, string password)
        {
            switch (whatIsEmpty(login, password))
            {
                case ("Пароль"):
                    MessageBox.Show("Пожалуйста заполните поле " + whatIsEmpty(login, password));
                    break;
                case ("Логин"):
                    MessageBox.Show("Пожалуйста заполните поле " + whatIsEmpty(login, password));
                    break;
                case (null):
                    string userName = "";
                    string birthday = "";
                    if (CheckLogPass(login, password, ref userName, ref birthday))
                    {
                        MessageBox.Show("Connect");
                        Ticket.FIO = userName;
                        Ticket.DATE = birthday;
                        Tour_Click();
                        MainModel.Close_Enter_Window();
                    }
                    else
                    {
                        MessageBox.Show("Неверный логин или Пароль!");
                    }
                    break;
            }
        }
    }
}
