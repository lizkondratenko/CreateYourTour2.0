using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateYourTour.Model
{
    static class ConnectionToDB
    {
        //REVIEW: При запуске с рабочего стола - могут быть проблемы
        private static string path = Directory.GetCurrentDirectory();
        //REVIEW: Path.Combine
        private static string databaseName = @"" + path + "\\Persons.db";

        private static string getInfoFromDB(string SELECT_part, string FROM_part, string WHERE_part)
        {
            //REVIEW: Тут может быть куча исключений
            SQLiteConnection dbConnection = new SQLiteConnection(string.Format("Data Source={0}; Version=3", databaseName));
            dbConnection.Open();

            string sql = string.Format("SELECT {0} FROM {1} WHERE {2}", SELECT_part, FROM_part, WHERE_part);
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            string info = "";
            foreach (DbDataRecord record in reader)
            {
                info = (record[SELECT_part].ToString());
            }

            dbConnection.Close();

            return info;
        }

        private static List<string> getListFromDB(string SELECT_part, string FROM_part, string WHERE_part)
        {
            //REVIEW: И тут может быть куча исключений
            List<string> listOfItems = new List<string>();

            SQLiteConnection dbConnection = new SQLiteConnection(string.Format("Data Source={0}; Version=3", databaseName));
            dbConnection.Open();

            string sql = string.Format("SELECT {0} FROM {1} {2}", SELECT_part, FROM_part, WHERE_part);
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            
            foreach (DbDataRecord record in reader)
            {
                if (!listOfItems.Contains(record[SELECT_part].ToString()))
                {
                    listOfItems.Add(record[SELECT_part].ToString());
                }
            }

            dbConnection.Close();

            return listOfItems;
        }

        public static int getPrice(string Country, string City, string Hotel)
        {
            string sql = string.Format("Country = \"{0}\" AND City = \"{1}\" AND Hotel = \"{2}\"", Country, City, Hotel);
            int Price = Convert.ToInt32(getInfoFromDB("Price", "Tours", sql));
            return Price;
        }

        public static string getNameOrBirthday(string type, string login, string password)
        {
            string sql = string.Format("Login=\"{0}\" AND Password=\"{1}\"", login, password);
            string NameOrBirthday = getInfoFromDB(type, "Persons", sql);
            return NameOrBirthday;
        }

        public static string getPassword(string login)
        {
            string sql = string.Format("Login=\"{0}\"", login);
            string Password = getInfoFromDB("Password", "Persons", sql);
            return Password;
        }

        public static List<string> getCountriesForComboBox()
        {
            List<string> List = getListFromDB("Country", "Tours", "");
            return List;
        }

        public static List<string> getCitiesForComboBox(string Country)
        {
            string sql = string.Format("WHERE Country=\"{0}\"", Country);
            List<string> List = getListFromDB("City", "Tours", sql);
            return List;
        }

        public static List<string> getHotelsForComboBox(string Country, string City)
        {
            string sql = string.Format("WHERE Country=\"{0}\" AND City=\"{1}\"", Country, City);
            List<string> List = getListFromDB("Hotel", "Tours", sql);
            return List;
        }

        public static int getAmountOfPeople(string Country, string City, string Hotel)
        {
            string sql = string.Format("Country=\"{0}\" AND City=\"{1}\" AND Hotel=\"{2}\"", Country, City, Hotel);
            int Count = Convert.ToInt32(getInfoFromDB("AmountOfPeople", "Tours", sql));
            return Count;
        }

        public static DateTime getDate(string type, string Country, string City, string Hotel)
        {
            string sql = string.Format("Country=\"{0}\" AND City=\"{1}\" AND Hotel=\"{2}\"", Country, City, Hotel);
            DateTime date = Convert.ToDateTime(getInfoFromDB(type, "Tours", sql));
            return date;
        }


        private static string CreateSQLInquiry(string tableName, string type, Account account)
        {
            //REVIEW: Ну, по-хорошему напрашивается enum
            if (type == "insert")
            {
                return string.Format("{5} into {6} (Name, Birthday, Pasport, Login, Password) values (\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\")", account.getName(), account.getDate(), account.getPasportData(), account.getLogin(), account.getPassword(), type, tableName);
            }
            else
            {
                return null;
            }

        }

        public static void AddAccountToDataBase(Account acc)
        {
            //REVIEW: Может быть исключение
            SQLiteConnection dbConnection = new SQLiteConnection(string.Format("Data Source={0}; Version=3", databaseName));
            dbConnection.Open();
            string sql = CreateSQLInquiry("Persons", "insert",acc);
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            command.ExecuteNonQuery();
            dbConnection.Close();
        }

    }
}
