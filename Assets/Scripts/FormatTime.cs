﻿using System;

namespace Assets.Scripts
{
    public class FormatTime
    {
        public static string FormatLevelTime(float timeInSeconds)
        {
            TimeSpan time = TimeSpan.FromSeconds(timeInSeconds);

            if (time.TotalMinutes > 1)
            {
                return (string.Format("{0}:{1}.{2}", (int)time.TotalMinutes, time.Seconds, time.Milliseconds));
            }
            else
            {
                return (string.Format("{0}.{1}", time.Seconds, time.Milliseconds));
            }
        }

        public static string FormatEndLevelRecord(string record)
        {
            return (string.Format("{0} {1}", "Record :", record));
        }

        public static string FormatEndLevelTime(string time)
        {
            return (string.Format("{0} {1}", "Time :", time));
        }
    }
}