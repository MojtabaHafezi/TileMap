using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Logging
{
    public class Logging
    {
        public static void Log(string message)
        {
            Debug.Log(GetCallingMethodName() + "() - " + message);
        }

        public static void LogWarning(string message)
        {
            Debug.LogWarning(GetCallingMethodName() + "() - " + message);
        }

        public static void LogError(string message)
        {
            Debug.LogError(GetCallingMethodName() + "() - " + message);
        }

        private static string GetCallingMethodName()
        {
            return new System.Diagnostics.StackTrace(1).GetFrame(1).GetMethod().Name;
        }
    }
}