using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoTera.Systems
{
    internal static class NavigationSystem
    {
        public static async void GoToOptionsAsync()
        {
            await Shell.Current.GoToAsync(nameof(OptionsPage));
        }
    }
}
