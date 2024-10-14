namespace CoTera.Systems
{
    internal static class AppControllerSystem
    {
        internal static async void GoToOptionsAsync() => await Shell.Current.GoToAsync(nameof(OptionsPage));

        internal static async void GoBackToMainAsync() 
        {
            Task t = Shell.Current.GoToAsync("..");
            await t;
            (Shell.Current.CurrentPage as MainPage)!.SetPage();

        }

        internal static async void Alert(string title, string message, string cancel) => await Shell.Current.CurrentPage.DisplayAlert(title, message, cancel);

    }
}