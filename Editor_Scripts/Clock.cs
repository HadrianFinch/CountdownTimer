using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

using UnityToolbarExtender;

using System.Linq;

namespace CountdownTimer
{
    [InitializeOnLoad]
    public class Clock
    {
        internal static DeadlineSO GetInstance()
        {
            string[] guids = UnityEditor.AssetDatabase.FindAssets("t:DeadlineSO");
            if (guids.Length == 0)
            {
                return null;
            }
            else
            {
                string path = UnityEditor.AssetDatabase.GUIDToAssetPath(guids[0]);
                return UnityEditor.AssetDatabase.LoadAssetAtPath<DeadlineSO>(path);
            }
        }

        static DeadlineSO deadline = null;

        static Clock()
        {
            ToolbarExtender.RightToolbarGUI.Add(OnToolbarGUI);
            Rescan();
        }

        private static void Rescan()
        {
            deadline = GetInstance();

            if (deadline == null)
            {
                Debug.LogWarning("No deadline asset found! To create one, open \"Create > Countdown Deadline\"");
            }
        }

        static void OnToolbarGUI()
        {
            if (deadline != null)
            {
                System.DateTime dt = deadline.deadline.GetDateTime();
                var diff = dt.Subtract(System.DateTime.UtcNow);

                GUIStyle deathClockStyle = new GUIStyle(EditorStyles.label);
                deathClockStyle.normal.textColor = Color.gray;
                deathClockStyle.fontSize = 16;

                GUILayout.Label($"{diff.TotalHours:00}:{diff.Minutes:00}:{diff.Seconds:00}", deathClockStyle);
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
