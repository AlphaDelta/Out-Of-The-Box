using System;
namespace OutOfTheBox
{
    class Module : Attribute
    {
        public string Name = "";
        public int Version = 0;

        public Module(string Name, int Version)
        {
            this.Name = Name;
            this.Version = Version;
        }
        public Module(string Name, byte Major, byte Minor, byte Patch)
        {
            this.Name = Name;
            this.Version = (Major << 16) + (Minor << 8) + Patch;
        }
    }
}
