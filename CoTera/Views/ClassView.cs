﻿namespace CoTera.Views
{
    internal class ClassView
    {
        internal string Name;

        internal string TimeSpan;

        //[JsonConstructor]
        internal ClassView(string name, string timeSpan)
        {
            Name = name;
            TimeSpan = timeSpan;
        }
    }
}
