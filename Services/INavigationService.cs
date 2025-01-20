using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2.Services
{
    public interface INavigationService
    {
        void Navigate(params object[] parameters);
        Task AsyncNavigation(params object[] parameters);
    }
}
