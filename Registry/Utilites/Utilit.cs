using Microsoft.IdentityModel.Tokens;
using Registry.Models;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Registry.Utilites
{
    public class Utilit
    {
        public static bool checkCity(string city) {
            if(city.IsNullOrEmpty())
                return false;
            city = Utilit.stringToLower(city);
            if(city.Length > 255 || city.Length < 4) 
                return false;
            Regex regex = new Regex(@"[\d!@#$%^&*()=_+`~"";:.,<>/?\\|}{[\]]");
            Match match = regex.Match(city);
            if (match.Success)
                return false;
            return true;
        }
        public static bool checkDate(string date)
        {
            //2024-01-11 - parametr date
            //11.01.2024 00:00:00 - datetime.now
            if(date.IsNullOrEmpty()) 
                return false;
            Regex regex = new Regex(@"^\d{4}-\d{2}-\d{2}$");
            Match match = regex.Match(date);
            if(!match.Success)
                return false;
            int[] dateInt = Utilit.splitDate(date, '-');
            
            DateTime currencyDate =  DateTime.Now.Date;
            //currencyDate.Day > dateInt[2] 
            //    || currencyDate.Year != dateInt[0]
            //    || currencyDate.Month > dateInt[1]
            if (currencyDate.Year == dateInt[0] &&
                (currencyDate.Month < dateInt[1] || 
                (currencyDate.Month == dateInt[1] && currencyDate.Day <= dateInt[2])))
                return true;
            return false;
        }
        public static int[] splitDate(string date, char separator)
        {
            string[] arrDate = date.Trim().Split(separator);
            int[] dateInt = new int[arrDate.Length];

            for (int i = 0; i < arrDate.Length; i++)
                dateInt[i] = Convert.ToInt32(arrDate[i]);
            return dateInt;
        }
        public static string stringToLower(string s)
        {
            return s.Trim().ToLower();
        }
        public static string? dayOfWeek(string day) {
            string? result = null;
            switch (day)
            {
                case "Monday": result = "Пн"; break;
                case "Tuesday": result = "Вт"; break;
                case "Wednesday": result = "Ср"; break;
                case "Thursday": result = "Чт";break;
                case "Friday": result = "Пт"; break;
                case "Saturday": result = "Сб"; break;
                case "Sunday": result = "Нд"; break;
            }
            return result;
        }
        public static string? mounthToString(int mounth)
        {
            string? result = null;
            switch (mounth)
            {
                case 1: result = "Січня"; break;
                case 2: result = "Лютого"; break;
                case 3: result = "Березня"; break;
                case 4: result = "Квітня"; break;
                case 5: result = "Травня"; break;
                case 6: result = "Червня"; break;
                case 7: result = "Липня"; break;
                case 8: result = "Серпня"; break;
                case 9: result = "Вересня"; break;
                case 10: result = "Жовтня"; break;
                case 11: result = "Листопада"; break;
                case 12: result = "Грудня"; break;
            }
            return result;
        }
    }
}
