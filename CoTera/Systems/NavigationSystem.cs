namespace CoTera.Systems
{
    internal static class NavigationSystem
    {
        internal static async void GoToOptionsAsync() => await Shell.Current.GoToAsync(nameof(OptionsPage));

        internal static async void GoBackToMainAsync() => await Shell.Current.GoToAsync("..");
    }
}