namespace CoTera.Systems
{
    public static class NavigationSystem
    {
        public static async void GoToOptionsAsync()
        {
            await Shell.Current.GoToAsync(nameof(OptionsPage));
        }

        public static async void GoBackToMainAsync()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
