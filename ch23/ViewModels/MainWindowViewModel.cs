using ch23.Infrastructure.Commands;
using ch23.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

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

        #region Commands

        #region CloseApplicationCommand

        public ICommand CloseApplicationCommand {  get; }

        private void OnCloseApplicationCommandExecuted(object o)
        {
            Application.Current.Shutdown();
        }

        private bool CanCloseApplicationCommandExecute(object o) 
        { 
            return true;
        }

        #endregion

        #endregion

        public MainWindowViewModel() 
        {
            #region Commands

            CloseApplicationCommand = new RelatedCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);

            #endregion
        }

    }
}
