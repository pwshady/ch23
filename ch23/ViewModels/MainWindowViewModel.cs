using ch23.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ch23.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        #region TitleWindow
        private string _Title ="111111";

        /// <summary>
        /// TitleWindow
        /// </summary>
        public string Title 
        { 
            get => _Title; 
            set 
            {
                if (Equals(_Title, value)) return;
                _Title = value;
                OnProprtyCanged();
            } 
        }
        #endregion

    }
}
