using Jaxx.Net.Launch.DefaultLauchner.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace Jaxx.Net.Launch.DefaultLauchner.ViewModels
{
    public class DefaultLauncherMainViewModel : BindableBase
    {
        public ObservableCollection<Button> Items { get; set; }
        private List<CustomCommand> customCommands;
        public DefaultLauncherMainViewModel()
        {
            customCommands = new List<CustomCommand>();
            var config = File.ReadAllLines("config.txt");
            foreach (var command in config)
            {
                var splitted = command.Split(";");
                customCommands.Add(new CustomCommand { Name = splitted[0], Command = splitted[1], ShellExecute = bool.Parse(splitted[2]) });
            }
            
            
            Items = new ObservableCollection<Button>();

            foreach (var command in customCommands)
            {
                Items.Add(new Button { Content = command.Name, Command = CustomCommandDelegate, CommandParameter = command, Height = 40, Margin = new System.Windows.Thickness(2) });
            }
        }
        private DelegateCommand<CustomCommand> delegateCommand;
        public DelegateCommand<CustomCommand> CustomCommandDelegate =>
            delegateCommand ?? (delegateCommand = new DelegateCommand<CustomCommand>(ExecuteCustomCommandDelegate, CanExecuteCustomCommandDelegate));

        void ExecuteCustomCommandDelegate(CustomCommand parameter)
        {
            try
            {
                using (Process myProcess = new Process())
                {
                    myProcess.StartInfo.UseShellExecute = parameter.ShellExecute;
                    // You can start any process, HelloWorld is a do-nothing example.
                    myProcess.StartInfo.FileName = parameter.Command;
                    //myProcess.StartInfo.CreateNoWindow = true;
                    myProcess.Start();
                    // This code assumes the process you are starting will terminate itself. 
                    // Given that is is started without a window so you cannot terminate it 
                    // on the desktop, it must terminate itself or you can do it programmatically
                    // from this application using the Kill method.
                }
            }
            catch (Exception e)
            {
                
            }
        }

        bool CanExecuteCustomCommandDelegate(CustomCommand parameter)
        {
            return true;
        }
    }
    
}
