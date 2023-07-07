using System;
using UnityEditor;
using UnityEngine;
using UnityToolbarExtender;

namespace CountdownTimer
{
    [InitializeOnLoad]
    public static class Clock
    {
        internal static DeadlineSO GetInstance()
        {
            string[] guids = AssetDatabase.FindAssets("t:DeadlineSO");
            if (guids.Length == 0)
            {
                return null;
            }
            else
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[0]);
                return AssetDatabase.LoadAssetAtPath<DeadlineSO>(path);
            }
        }

        private static DeadlineSO deadline;

        static Clock()
        {
            Rescan();
            ToolbarExtender.RightToolbarGUI.Add(OnToolbarGUI);
        }

        private static void Rescan()
        {
            deadline = GetInstance();

            if (deadline == null)
            {
                Debug.LogWarning("No deadline asset found! To create one, \"Assets > Create > Countdown Deadline\"");
            }
        }

        private static void OnToolbarGUI()
        {
            if (deadline != null)
            {
                DateTimeOffset dt = deadline.GetDateTimeOffset();
                var diff = dt - DateTimeOffset.UtcNow;

                GUIStyle deathClockStyle = new(EditorStyles.label);
                deathClockStyle.normal.textColor = Color.gray;
                deathClockStyle.fontSize = 16;

                // Fixed the bug: if you forget to cast down via (int), the double may round up giving off-by-1 in the hours
                GUILayout.Label($"{(int)diff.TotalHours:00}:{diff.Minutes:00}:{diff.Seconds:00}", deathClockStyle);
            }
            else
            {
                if (GUILayout.Button("Rescan for Deadlines"))
                {
                    Rescan();
                }
                GUILayout.FlexibleSpace();
            }
        }
    }
}
