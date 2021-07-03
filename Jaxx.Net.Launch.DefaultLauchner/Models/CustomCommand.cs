using System.Windows.Media;

namespace Jaxx.Net.Launch.DefaultLauchner.Models
{
    public class CustomCommand
    {
        public string Name { get; set; }
        public string Command { get; set; }
        public string Arguments { get; set; }
        public bool ShellExecute { get; set; }
        public Brush Color { get; set; }
    }
}
