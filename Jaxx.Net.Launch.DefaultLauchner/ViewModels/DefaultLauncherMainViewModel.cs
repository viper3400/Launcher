using Jaxx.Net.Launch.DefaultLauchner.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Media;

namespace Jaxx.Net.Launch.DefaultLauchner.ViewModels
{
    public class DefaultLauncherMainViewModel : BindableBase
    {
        public ObservableCollection<Button> Items { get; set; }

        private string errorMessage;
        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }
        private bool isErrrorMessageVisible = false;
        public bool IsErrorMessageVisible
        {
            get { return isErrrorMessageVisible; }
            set { SetProperty(ref isErrrorMessageVisible, value); }
        }

        private Timer errorMessageVisibilityTimer;

        private List<CustomCommand> customCommands;
        public DefaultLauncherMainViewModel()
        {
            errorMessageVisibilityTimer = new Timer(5000);
            errorMessageVisibilityTimer.AutoReset = true;
            errorMessageVisibilityTimer.Elapsed += ErrorMessageVisibilityTimer_Elapsed;

            var configFile = "config.txt";
            try
            {
                LoadButtonConfiguration(configFile);
            }
            catch (Exception e)
            {
                var msg = e.Message;
                ShowErrorMessage(msg);
            }
        }

        private void LoadButtonConfiguration(string configFile)
        {
            customCommands = new List<CustomCommand>();
            var config = File.ReadAllLines(configFile);
            foreach (var command in config)
            {
                var splitted = command.Split(";");
                var customCommand = new CustomCommand { Name = splitted[0], Command = splitted[1], ShellExecute = bool.Parse(splitted[2]) };

                try
                {
                    customCommand.Color = new BrushConverter().ConvertFromString(splitted[3]) as Brush;
                }
                catch (Exception)
                { }


                customCommands.Add(customCommand);
            }


            Items = new ObservableCollection<Button>();

            foreach (var command in customCommands)
            {
                var btn = new Button
                {
                    Content = command.Name,
                    Command = CustomCommandDelegate,
                    CommandParameter = command,
                    Height = 40,
                    Margin = new System.Windows.Thickness(2)
                };

                if (command.Color != null) btn.Background = command.Color;

                Items.Add(btn);
            }
        }

        private void ErrorMessageVisibilityTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            errorMessageVisibilityTimer.Stop();
            IsErrorMessageVisible = false;
            ErrorMessage = "";
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
                var msg = $"{e.Message} ({parameter.Command})";
                ShowErrorMessage(msg);
            }
        }

        private void ShowErrorMessage(string msg)
        {
            IsErrorMessageVisible = true;
            ErrorMessage = msg;
            errorMessageVisibilityTimer.Start();
        }

        bool CanExecuteCustomCommandDelegate(CustomCommand parameter)
        {
            return true;
        }
    }

}
