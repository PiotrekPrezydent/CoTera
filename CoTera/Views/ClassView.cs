using Newtonsoft.Json.Linq;

namespace CoTera.Views
{
    internal class ClassView
    {
        internal string Name;

        internal string TimeSpan;

        internal string Room;

        internal string Type;

        internal string Week;

        internal ClassView(JToken rawClass)
        {
            Name = rawClass["nazwa"] == null ? "" : rawClass["nazwa"]!.ToString();

            TimeSpan = rawClass["godziny"] == null ? "" : rawClass["godziny"]!.ToString();

            Room = rawClass["sala"] == null ? "" : rawClass["sala"]!.ToString();

            Type = rawClass["rodzaj"] == null ? "" : rawClass["rodzaj"]!.ToString();

            Week = rawClass["tydzien"] == null ? "" : rawClass["tydzien"]!.ToString();
        }

        internal string GetClassInfo() => Name + "\n" + TimeSpan + "\n" + Type + "\n" + Room;

        internal ClassView(string name, string timeSpan)
        {
            Name = name;
            TimeSpan = timeSpan;
        }
    }
}
