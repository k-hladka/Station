using Azure.Core;
using Microsoft.IdentityModel.Tokens;
using Registry.Models;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Registry.Utilites
{
    public class Utilit
    {
        public static bool checkCityorInitial(string cityOrInitial) {
            if(cityOrInitial.IsNullOrEmpty())
                return false;
            cityOrInitial = Utilit.stringToLower(cityOrInitial);
            if(cityOrInitial.Length > 255 || cityOrInitial.Length < 2) 
                return false;
            Regex regex = new Regex(@"[\d!@#$%^&*()=_+`~"";:.,<>/?\\|}{[\]]");
            Match match = regex.Match(cityOrInitial);
            if (match.Success)
                return false;
            return true;
        }
        public static bool checkDate(string date, string time="")
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
            if(currencyDate.Year == dateInt[0])
            {
                if (currencyDate.Month < dateInt[1])
                    return true;
                if(currencyDate.Month == dateInt[1] && currencyDate.Day < dateInt[2])
                    return true;
                if(currencyDate.Month == dateInt[1] && currencyDate.Day == dateInt[2])
                {
                    if(time.IsNullOrEmpty()) return true;
                    else
                    {
                        if(Convert.ToInt32(time.Substring(0, 2)) > DateTime.Now.Hour || 
                            (Convert.ToInt32(time.Substring(0, 2)) == DateTime.Now.Hour &&
                            Convert.ToInt32(time.Substring(3, 2)) >= DateTime.Now.Minute))
                            return true;
                    }
                }
                return false;
            }
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
        public static bool chechCountOfSeats(string count)
        {
            if(count.IsNullOrEmpty())
                return false;
            Regex regex = new Regex(@"^\d$");
            Match match = regex.Match(count);
            if (!match.Success)
                return false;
            int countInt = Int32.Parse(count.ToString());
            if(countInt < 1 || countInt > 4)
                return false;

            return true;
        }
        public static bool chechEmail(string email)
        {
            if(email.IsNullOrEmpty()) return false;
            email=Utilit.stringToLower(email);
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            Match match = regex.Match(email);
            if (!match.Success) return false;
            return true;
        }
        public static bool checkPhone(string phone)
        {
            if(phone.IsNullOrEmpty()) return false;
            if(phone.Length < 10 && phone.Length>13) return false;
            Regex regex = new Regex(@"^[+]?[(]?[0-9]{1,3}[)]?[-\s\./0-9]*$");
            Match match = regex.Match(phone);
            if (!match.Success) return false;
            return true;
        }
    }
}
