using Avalonia.Controls;
using demkur.Models;

namespace demkur
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            using var ctx = new User20Context();
        }

        public class Tovar
        {

        }
    }
}