namespace System
{
    public static class PersianDateTimeExtension
    {
        private const string DaysAgo = "{0} روز قبل";
        private const string HoursAgo = "{0} ساعت قبل";
        private const string JustNow = "همین الان";
        private const string LastMonth = "ماه قبل";
        private const string LastWeek = "هفته قبل";
        private const string LastYear = "سال قبل";
        private const string MinutesAgo = "{0} دقیقه قبل";
        private const string MonthsAgo = "{0} ماه قبل";
        private const string NotYet = "هنوز نه";
        private const string ThreeWeeksAgo = "سه هفته قبل";
        private const string TwoWeeksAgo = "دو هفته قبل";
        private const string YearsAgo = "{0} سال قبل";
        private const string Yesterday = "دیروز";

        public static PersianDateTime ToPersianDateTime(this DateTime? dt)
        {
            if (!dt.HasValue)
                return null;
            return new PersianDateTime(dt.Value);
        }

        public static PersianDateTime ToPersianDateTime(this DateTime dt)
        {
            return new PersianDateTime(dt);
        }

        public static string ToPersianRelativeDate(this PersianDateTime date)
        {
            var gDate = date.ToDateTime();
            return gDate.ToPersianRelativeDate();
        }

        public static string ToPersianRelativeDate(this DateTime date)
        {
            var timeSince = DateTime.Now.Subtract(date);
            if (timeSince.TotalMilliseconds < 1) return NotYet;
            if (timeSince.TotalMinutes < 1) return JustNow;
            if (timeSince.TotalMinutes < 60) return string.Format(MinutesAgo, timeSince.Minutes);
            if (timeSince.TotalHours < 24) return string.Format(HoursAgo, timeSince.Hours);
            if (timeSince.TotalDays < 2) return Yesterday;
            if (timeSince.TotalDays < 7) return string.Format(DaysAgo, timeSince.Days);
            if (timeSince.TotalDays < 14) return LastWeek;
            if (timeSince.TotalDays < 21) return TwoWeeksAgo;
            if (timeSince.TotalDays < 28) return ThreeWeeksAgo;
            if (timeSince.TotalDays < 60) return LastMonth;
            if (timeSince.TotalDays < 365) return string.Format(MonthsAgo, Math.Round(timeSince.TotalDays / 30));
            return timeSince.TotalDays < 730
                ? LastYear
                : string.Format(YearsAgo, Math.Round(timeSince.TotalDays / 365));
        }

        public static string ToPersianRelativeDate(this DateTime? dt)
        {
            if (!dt.HasValue)
                return null;
            return new PersianDateTime(dt.Value).ToString();
        }

        public static string ToStringPersianDateTime(this DateTime? dt)
        {
            if (!dt.HasValue)
                return null;
            return new PersianDateTime(dt.Value).ToString();
        }

        public static string ToStringPersianDateTime(this DateTime dt)
        {
            return new PersianDateTime(dt).ToString();
        }
    }
}