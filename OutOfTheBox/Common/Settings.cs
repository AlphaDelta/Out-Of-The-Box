using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace OutOfTheBox.Common
{
    class Settings
    {
        static List<Setting> Data = new List<Setting>(16);
        static FileStream file;

        public static void Set<T>(string key, T value)
        {
            if (key == "") return;

            Type tt = typeof(T);
            if (tt != typeof(string) &&
                tt != typeof(int) &&
                tt != typeof(double) &&
                tt != typeof(float) &&
                tt != typeof(long) &&
                tt != typeof(decimal)) return;

            foreach (Setting pair in Data) if (pair.Key == key && pair.Type == typeof(T)) { pair.Value = value; Save(); return; }
            Data.Add(new Setting(key, value, typeof(T)));
            Save();
        }

        public static T Get<T>(string key)
        {
            foreach (Setting pair in Data)
                if (pair.Key == key && pair.Type == typeof(T))
                {
                    return (T)pair.Value;
                }
            return default(T);
        }

        static void Save()
        {
            StringBuilder sb = new StringBuilder(256);
            bool flag = false;
            foreach (Setting s in Data)
            {
                if (flag) sb.Append('\n');
                else flag = true;

                if (s.Type == typeof(string)) sb.Append("STR ");
                else if (s.Type == typeof(int)) sb.Append("INT ");
                else if (s.Type == typeof(long)) sb.Append("LNG ");
                else if (s.Type == typeof(double)) sb.Append("DBL ");
                else if (s.Type == typeof(float)) sb.Append("FLT ");
                else if (s.Type == typeof(decimal)) sb.Append("DEC ");
                sb.Append(s.Key + ": ");
                sb.Append(s.Value);
            }

            file.SetLength(0);
            byte[] bytes = Encoding.UTF8.GetBytes(sb.ToString());
            file.Write(bytes, 0, bytes.Length);
        }

        static bool Initialized = false;
        public static void Initialize()
        {
            string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\OutOfTheBox\";
            if (!Directory.Exists(appdata)) Directory.CreateDirectory(appdata);
            if (File.Exists(appdata + "settings"))
            {
                string[] setmem = File.ReadAllLines(appdata + "settings");
                foreach (string line in setmem)
                {
                    string[] spl = line.Split(' ');
                    if (spl.Length < 3) continue;

                    string key = spl[1].Replace(":", "");
                    StringBuilder vl = new StringBuilder();
                    int l = 0;
                    foreach (string s in spl)
                    {
                        l++;
                        if (l < 3) continue;
                        vl.Append(s);
                        if (l != 3) vl.Append(' ');
                    }
                    string val = vl.ToString();

                    if (spl[0] == "STR") Data.Add(new Setting(key, val, typeof(string)));
                    else if (spl[0] == "INT") Data.Add(new Setting(key, Int32.Parse(val), typeof(int)));
                    else if (spl[0] == "LNG") Data.Add(new Setting(key, Int64.Parse(val), typeof(long)));
                    else if (spl[0] == "DBL") Data.Add(new Setting(key, Double.Parse(val), typeof(double)));
                    else if (spl[0] == "FLT") Data.Add(new Setting(key, float.Parse(val), typeof(float)));
                    else if (spl[0] == "DEC") Data.Add(new Setting(key, Decimal.Parse(val), typeof(decimal)));
                }
            }
            file = File.Open(appdata + "settings", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read);


            Initialized = true;
        }

        public static void Uninitialize()
        {
            if (Initialized)
            {
                file.Close();
                file.Dispose();
                Initialized = false;
            }
        }
    }

    class Setting
    {
        public readonly string Key;
        public object Value;
        public readonly Type Type;

        public Setting(string Key, object Value, Type Type) { this.Key = Key; this.Value = Value; this.Type = Type; }
    }
}
