using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CoTera
{
    internal class CollegeClass
    {
        internal string Name;

        internal string TimeSpan;

        //[JsonConstructor]
        internal CollegeClass(string name, string timeSpan)
        {
            Name = name;
            TimeSpan = timeSpan;
        }
    }
}
