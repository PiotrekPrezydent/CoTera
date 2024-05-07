namespace CoTera.Systems
{
    internal static class AppControllerSystem
    {
        internal static async void GoToOptionsAsync() => await Shell.Current.GoToAsync(nameof(OptionsPage));

        internal static async void GoBackToMainAsync() => await Shell.Current.GoToAsync("..");

        internal static async void Alert(string title, string message, string cancel) => await Shell.Current.CurrentPage.DisplayAlert(title, message, cancel);
    }
}