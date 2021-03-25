using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LabaratoryWork7Part2
{
    public static class PageManager
    {
        private static Frame target; 
        public static void Init(Frame target)
        {
            PageManager.target = target;
        }


        public static bool CanGoBack => target.CanGoBack;
        public static void NavigateToPage(Page page) => target.Navigate(page);
        public static void GoBack() => target.GoBack();
    }
}
