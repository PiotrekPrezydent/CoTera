using Microsoft.Maui.Controls;

namespace CoTera.Systems
{
    internal static class AppControllerSystem
    {
        internal static async void GoToOptionsAsync() => await Shell.Current.GoToAsync(nameof(OptionsPage));

        internal static async void GoBackToMainAsync() => await Shell.Current.GoToAsync("..");

        internal static async void Alert(string title, string message, string cancel) => await Shell.Current.CurrentPage.DisplayAlert(title, message, cancel);


        internal static void AddLoadingFrameToCurrentPage()
        {
            var currentPage = Shell.Current.CurrentPage;
            if (currentPage is ContentPage contentPage)
            {
                if (contentPage.Content is Grid grid)
                {
                    if (!grid.Children.Contains(LoadingFrame))
                    {
                        Grid.SetColumn(LoadingFrame, 1);
                        Grid.SetRow(LoadingFrame, 1);
                        grid.Children.Add(LoadingFrame);
                    }
                    else
                    {
                        (grid.Children[grid.Children.IndexOf(LoadingFrame)] as Frame)!.IsVisible = true;
                    }

                }
            }
        }

        internal static void RemoveLoadingFrameFromCurrentPage()
        {
            var currentPage = Shell.Current.CurrentPage;
            if (currentPage is ContentPage contentPage)
                if (contentPage.Content is Grid grid)
                    (grid.Children[grid.Children.IndexOf(LoadingFrame)] as Frame)!.IsVisible = false;

        }

        internal static Label LoadingLabel = new Label
        {
            Text = "Loading, please wait...",
            FontAttributes = FontAttributes.Bold,
            FontSize = 18,
            TextColor = Colors.Black,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };

        internal static Frame LoadingFrame = new Frame()
        {
            WidthRequest = 200,
            HeightRequest = 200,
            BackgroundColor = Colors.LightGray,
            CornerRadius = 10,
            HasShadow = true,
            IsVisible = false,
            Content = new StackLayout
            {
                HorizontalOptions= LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Children =
                {
                    LoadingLabel,
                    new ActivityIndicator
                    {
                        IsRunning = true,
                        IsVisible = true,
                        Color = Colors.Black,
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.Center,
                        BackgroundColor = Colors.Transparent
                    },
                }
            }
        };



    }
}