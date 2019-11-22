using System;
using System.Collections.Generic;
using System.Text;

namespace Jaxx.Net.Launch.DefaultLauchner.Models
{
    public class CustomCommand
    {
        public string Name { get; set; }
        public string Command { get; set; }
        public bool ShellExecute { get; set; }
    }
}
