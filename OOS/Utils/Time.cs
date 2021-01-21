using Cosmos.HAL;

namespace OOS.Utils
{
    public static class Time
    {
        static int Hour() { return RTC.Hour; }

        static int Minute() { return RTC.Minute; }

        static int Second() { return RTC.Second; }

        static int Century() { return RTC.Century; }

        static int Year() { return RTC.Year; }

        static int Month() { return RTC.Month; }

        static int DayOfMonth() { return RTC.DayOfTheMonth; }

        static int DayOfWeek() { return RTC.DayOfTheWeek; }

        static string getTime24(bool hour, bool min, bool sec)
        {
            string timeStr = "";
            if (hour)
            {
                if (Hour().ToString().Length == 1)
                {
                    timeStr += "0" + Hour().ToString();
                }
                else
                {
                    timeStr += Hour().ToString();
                }
            }
            if (min)
            {
                if (Minute().ToString().Length == 1)
                {
                    timeStr += ":";
                    timeStr += "0" + Minute().ToString();
                }
                else
                {
                    timeStr += ":";
                    timeStr += Minute().ToString();
                }
            }
            if (sec)
            {
                if (Second().ToString().Length == 1)
                {
                    timeStr += ":";
                    timeStr += "0" + Second().ToString();
                }
                else
                {
                    timeStr += ":";
                    timeStr += Second().ToString();
                }
            }
            return timeStr;
        }

        static string getTime12(bool hour, bool min, bool sec)
        {
            string timeStr = "";
            if (hour)
            {
                if (Hour() > 12)
                    timeStr += Hour() - 12;
                else
                    timeStr += Hour();
            }
            if (min)
            {
                if (Minute().ToString().Length == 1)
                {
                    timeStr += ":";
                    timeStr += "0" + Minute().ToString();
                }
                else
                {
                    timeStr += ":";
                    timeStr += Minute().ToString();
                }
            }
            if (sec)
            {
                if (Second().ToString().Length == 1)
                {
                    timeStr += ":";
                    timeStr += "0" + Second().ToString();
                }
                else
                {
                    timeStr += ":";
                    timeStr += Second().ToString();
                }
            }
            if (hour)
            {
                if (Hour() > 12)
                    timeStr += " PM";
                else
                    timeStr += " AM";
            }
            return timeStr;
        }


        public static string TimeString(bool hour, bool min, bool sec)
        {
            return getTime12(hour, min, sec);
        }

        public static string YearString()
        {
            int intyear = Year();
            string stringyear = intyear.ToString();

            if (stringyear.Length == 2)
            {
                stringyear = "20" + stringyear;
            }
            return stringyear;
        }

        public static string MonthString()
        {
            int intmonth = Month();
            string stringmonth = intmonth.ToString();

            if (stringmonth.Length == 1)
            {
                stringmonth = "0" + stringmonth;
            }
            return stringmonth;
        }

        public static string DayString()
        {
            int intday = DayOfMonth();
            string stringday = intday.ToString();

            if (stringday.Length == 1)
            {
                stringday = "0" + stringday;
            }
            return stringday;
        }

        public static void WaitSeconds(int secNum)
        {
            int StartSec = Second();
            int EndSec;
            if (StartSec + secNum > 59)
            {
                EndSec = 0;
            }
            else
            {
                EndSec = StartSec + secNum;
            }
            while (RTC.Second != EndSec) {}
        }

    }
}
