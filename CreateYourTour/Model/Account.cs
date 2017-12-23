using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Text;

namespace CreateYourTour.Model
{
    class Account
    {
        private string name;
        private string date;
        private string pasportData;
        private string login;
        private string password;

        public Account(string name, string date, string pasportData, string login, string password)
        {
            this.name = name;
            this.date = date;
            this.pasportData = pasportData;
            this.login = login;
            this.password = password;
        }

        public string getName()
        {
            return name;
        }

        public string getDate()
        {
            return date;
        }

        public string getPasportData()
        {
            return pasportData;
        }

        public string getLogin()
        {
            return login;
        }

        public string getPassword()
        {
            return password;
        }


        public override string ToString()
        {
            return this.name + " " + this.date + " " + this.pasportData + " " + this.login + " " + this.password;
        }


    }
}
