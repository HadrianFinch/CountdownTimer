using System;
using UnityEngine;

namespace CountdownTimer
{
    [CreateAssetMenu(fileName = "Countdown Deadline", menuName = "Countdown Deadline")]
    public class DeadlineSO : ScriptableObject
    {
        public int year = DateTimeOffset.Now.Year;
        public int month = DateTimeOffset.Now.Month;
        public int day = DateTimeOffset.Now.Day;
        public int hour = DateTimeOffset.Now.Hour + 1;
        public int minute = 0;
        public int second = 0;

        public TimeZoneEnum timeZone;
        public bool isDaylightSavings;

        private TimeSpan tzOffset;

        public DateTimeOffset GetDateTimeOffset() => new(year, month, day, hour, minute, second, tzOffset);

        private void Reset()
        {
            var now = DateTimeOffset.Now;
            var tz = TimeZoneInfo.Local;
            timeZone = (TimeZoneEnum)tz.BaseUtcOffset.Hours;
            isDaylightSavings = tz.IsDaylightSavingTime(now);
            UpdateTimeZoneOffset();
        }

        private void OnValidate() => UpdateTimeZoneOffset();

        private void UpdateTimeZoneOffset()
        {
            var hoursOffset = (int)timeZone + (isDaylightSavings ? 1 : 0);
            var minOffset = timeZone == TimeZoneEnum.India ? 30 : 0;
            tzOffset = new TimeSpan(hoursOffset, minOffset, 0);
        }
    }

    public enum TimeZoneEnum
    {
        [InspectorName("Hawaii - Aleutian Standard Time (-10)")]
        Hawaii = -10,
        [InspectorName("Alaska Standard Time (-9)")]
        Alaska = -9,
        [InspectorName("Pacific Standard Time (-8)")]
        Pacific = -8,
        [InspectorName("Mountain Standard Time (-7)")]
        Mountain = -7,
        [InspectorName("Central Standard Time (-6)")]
        Central = -6,
        [InspectorName("Eastern Standard Time (-5)")]
        Eastern = -5,
        [InspectorName("Atlantic Standard Time (-4)")]
        Atlantic = -4,
        [InspectorName("Brasilia Standard Time (-3)")]
        Brasilia = -3,
        [InspectorName("Greenwich Mean Time (+0)")]
        Greenwich = 0,
        [InspectorName("Central European Time (+1)")]
        CentralEuropean = 1,
        [InspectorName("Eastern European Time (+2)")]
        EasternEuropean = 2,
        [InspectorName("Moscow Standard Time (+3)")]
        Moscow = 3,
        [InspectorName("Gulf Standard Time (+4)")]
        Gulf = 4,
        [InspectorName("India Standard Time (+5:30)")]
        India = 5,
        [InspectorName("China Standard Time (+8)")]
        China = 8,
        [InspectorName("Japan Standard Time (+9)")]
        Japan = 9,
        [InspectorName("Australian Eastern Standard Time (+10)")]
        Australia = 10,
    }
}