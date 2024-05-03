namespace CoTera.Views
{
    internal class DayView
    {
        internal DayOfWeek Day;
        internal ClassView[] Classes;

        internal DayView(DayOfWeek day, ClassView[] classes)
        {
            Day = day;
            Classes = classes;
        }
    }

}