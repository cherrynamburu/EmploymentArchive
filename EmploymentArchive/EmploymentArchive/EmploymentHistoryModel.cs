using System;

namespace EmploymentArchive
{
    class EmploymentHistoryModel
    {
        //internal static int EmploymentHistoryId { get; set; }
        internal static DateTime From { get; private set; }
        internal static DateTime To { get; set; }
        internal static bool SetFrom(string dateTimeAsString)
        {
            DateTime dateTime;
            if (DateTime.TryParse(dateTimeAsString, out dateTime))
            {
                From = dateTime;
                return true;
            }
                return false;
        }

        internal static bool SetTo(string dateTimeAsString)
        {
            DateTime dateTime;
            if (DateTime.TryParse(dateTimeAsString, out dateTime))
            {
                To = dateTime;
                return true;
            }
            return false;
        }

    }
}


