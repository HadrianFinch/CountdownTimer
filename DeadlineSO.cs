using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CountdownTimer
{
    [System.Serializable]
    public class SerializableDateTime
    {
        public int year;
        public int month;
        public int day;
        public int hour;
        public int minute;
        public int second;

        public DateTime GetDateTime()
        {
            return new DateTime(year, month, day, hour, minute, second);
        }
    }

    [CreateAssetMenu(fileName = "Countdown Deadline", menuName = "Countdown Deadline")]
    public class DeadlineSO : ScriptableObject
    {
        public SerializableDateTime deadline;
    }
}
