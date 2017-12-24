using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Word = Microsoft.Office.Interop.Word;
using System.Reflection;
using System.Data.SQLite;
using System.Data.Common;
using System.IO;

namespace CreateYourTour.Model
{
    class Ticket
    {
        public static string FIO;
        public static string DATE;

        private string Country;
        private string City;
        private string Hotel;
        private string DateFrom;
        private string DateTo;
        private int AmountOfPeople;
        private int Price;

        public Ticket(string Country, string City, string Hotel, string DateFrom, string DateTo, int AmountOfPeople)
        {
            this.Country = Country;
            this.City = City;
            this.Hotel = Hotel;
            this.DateFrom = DateFrom;
            this.DateTo = DateTo;
            this.AmountOfPeople = AmountOfPeople;
            this.Price = ConnectionToDB.getPrice(Country, City, Hotel);
        }

        private void AddStringToWordFile(string field, int i, Word.Document doc)
        {
            //REVIEW: Если тут где-то будет null - вылетит исключение
            object startLocation = doc.Sentences[i].End - 5;
            object endLocation = doc.Sentences[i].End - 1;
            Word.Range rng = doc.Range(ref startLocation, ref endLocation);
            rng.Select();
            rng.Text = field;
        }

        public void CreateTicketToWordFile()
        {
            var app = new Word.Application();

            //REVIEW: При запуске приложения с рабочего стола поведение может быть неожиданным
            string path = Directory.GetCurrentDirectory();
            //REVIEW: Path.Combine
            var doc = app.Documents.Open(FileName: @""+ path + "\\Test.docx");

            //REVIEW: И это исключение из метода AddStringToWordFile мы не обрабатываем
            AddStringToWordFile(FIO, 3, doc);
            AddStringToWordFile(DATE, 4, doc);
            AddStringToWordFile(Country,6,doc);
            AddStringToWordFile(City, 7, doc);
            AddStringToWordFile(Hotel, 8, doc);
            AddStringToWordFile(DateFrom + " - " + DateTo, 9, doc);
            AddStringToWordFile(AmountOfPeople.ToString(), 10, doc);
            AddStringToWordFile(DateTime.Now.ToShortDateString(), 11, doc);
            AddStringToWordFile((Price*AmountOfPeople).ToString(), 12, doc);

            app.Visible = true;

            //REVIEW: Path.Combine
            doc.SaveAs2(FileName: @"" + path + "\\Test_" + FIO + ".docx");
            doc.PrintPreview();
        }
    }
}
