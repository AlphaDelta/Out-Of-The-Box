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
                tt != typeof(string[]) &&
                tt != typeof(int) &&
                tt != typeof(int[]) &&
                tt != typeof(intd) &&
                tt != typeof(intd[]) &&
                tt != typeof(double) &&
                tt != typeof(float) &&
                tt != typeof(long) &&
                tt != typeof(decimal)) return;

            foreach (Setting pair in Data) if (pair.Key == key && pair.Type == typeof(T)) { pair.Value = value; Save(); return; }
            Data.Add(new Setting(key, value, typeof(T)));
            Save();
        }

        public static bool Get<T>(string key, ref T val)
        {
            foreach (Setting pair in Data)
                if (pair.Key == key && pair.Type == typeof(T))
                {
                    val = (T)pair.Value;
                    return true;
                }
            return false;
        }

        static void Save()
        {
            StringBuilder sb = new StringBuilder(256);
            bool flag = false;
            foreach (Setting s in Data)
            {
                if (flag) sb.Append('\n');
                else flag = true;

                string val = s.Value.ToString();

                if (s.Type == typeof(string)) sb.Append("STR ");
                else if (s.Type == typeof(string[]))
                {
                    StringBuilder sb2 = new StringBuilder();
                    bool flag2 = true;
                    foreach (string str in (string[])s.Value)
                    {
                        if (flag2)
                        {
                            sb2.Append("\"");
                            flag2 = false;
                        }
                        else sb2.Append(",\"");
                        sb2.Append(str.Replace(@"\", @"\\").Replace("\"", "\\\""));
                        sb2.Append("\"");
                    }


                    sb.Append("STR[] ");
                    val = sb2.ToString();
                }
                else if (s.Type == typeof(int)) sb.Append("INT ");
                else if (s.Type == typeof(int[]))
                {
                    StringBuilder sb2 = new StringBuilder();
                    bool flag2 = true;
                    foreach (int i in (int[])s.Value)
                    {
                        if (flag2) flag2 = false;
                        else sb2.Append(",");
                        sb2.Append(i);
                    }

                    sb.Append("INT[] ");
                    val = sb2.ToString();
                }
                else if (s.Type == typeof(intd))
                {
                    sb.Append("INTD ");
                    val = ((intd)s.Value).X + ":" + ((intd)s.Value).Y;
                }
                else if (s.Type == typeof(intd[]))
                {
                    StringBuilder sb2 = new StringBuilder();
                    bool flag2 = true;
                    foreach (intd i in (intd[])s.Value)
                    {
                        if (flag2) flag2 = false;
                        else sb2.Append(",");
                        sb2.Append(i.X + ":" + i.Y);
                    }

                    sb.Append("INTD[] ");
                    val = sb2.ToString();
                }
                else if (s.Type == typeof(long)) sb.Append("LNG ");
                else if (s.Type == typeof(double)) sb.Append("DBL ");
                else if (s.Type == typeof(float)) sb.Append("FLT ");
                else if (s.Type == typeof(decimal)) sb.Append("DEC ");
                sb.Append(s.Key + ": ");
                sb.Append(val);
            }

            file.SetLength(0);
            byte[] bytes = Encoding.UTF8.GetBytes((Internals.DEBUG ? sb.ToString() : DRX.Crypt(sb.ToString())));
            file.Write(bytes, 0, bytes.Length);
        }

        static bool Initialized = false;
        public static void Initialize()
        {
            string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\OutOfTheBox\";
            if (!Directory.Exists(appdata)) Directory.CreateDirectory(appdata);
            if (File.Exists(appdata + "settings"))
            {
                string setmem = File.ReadAllText(appdata + "settings");
                string[] decrypt = (Internals.DEBUG ? setmem : DRX.Crypt(setmem)).Split('\n');

                foreach (string line in decrypt)
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
                        if (l != 2) vl.Append(' ');
                    }
                    string val = vl.ToString();

                    if (spl[0] == "STR") Data.Add(new Setting(key, val, typeof(string)));
                    else if (spl[0] == "STR[]")
                    {
                        List<string> temp = new List<string>();
                        StringBuilder val2 = new StringBuilder();
                        bool esc = false;
                        bool instring = false;
                        foreach (char c in val)
                        {
                            if (c == '"' && !esc) { instring = !instring;  continue; }
                            if (c == ',' && !esc && !instring)
                            {
                                temp.Add(val2.ToString());
                                val2.Length = 0;
                                val2.Capacity = 0;
                                continue;
                            }
                            if (c == '\\' && !esc) { esc = true; continue; }
                            esc = false;
                            val2.Append(c);
                        }
                        if (val2.Length > 0) temp.Add(val2.ToString());
                        Data.Add(new Setting(key, temp.ToArray(), typeof(string[])));
                    }
                    else if (spl[0] == "INT") Data.Add(new Setting(key, Int32.Parse(val), typeof(int)));
                    else if (spl[0] == "INT[]")
                    {
                        List<int> temp = new List<int>();
                        string[] spl2 = val.Split(',');
                        foreach (string c in spl2) temp.Add(Int32.Parse(c));
                        Data.Add(new Setting(key, temp.ToArray(), typeof(int[])));
                    }
                    else if (spl[0] == "INTD")
                    {
                        string[] tmp = val.Split(':');
                        if (tmp.Length != 2) continue;
                        Data.Add(new Setting(key, new intd(Int32.Parse(tmp[0]), Int32.Parse(tmp[1])), typeof(intd)));
                    }
                    else if (spl[0] == "INTD[]")
                    {
                        List<intd> temp = new List<intd>();
                        string[] spl2 = val.Split(',');
                        foreach (string c in spl2)
                        {
                            string[] tmp = c.Split(':');
                            if (tmp.Length != 2) continue;
                            temp.Add(new intd(Int32.Parse(tmp[0]), Int32.Parse(tmp[1])));
                        }
                        Data.Add(new Setting(key, temp.ToArray(), typeof(intd[])));
                    }
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

        public Setting(string Key, object Value, Type Type)
        {
            this.Key = Key;
            this.Value = Value;
            this.Type = Type;
        }
    }
}
