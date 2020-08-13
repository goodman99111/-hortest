using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Graph
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewModel.ViewModel();

            CreatMain();
        }

        public void CreatMain()
        {
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            GridLayout.ClearPath();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            GridLayout.ClearPath();

            var path = ASearch.Search(1, 200);
            if (path != null)
                foreach (Cell path_node in path)
                    path_node.ChangeColor(Colors.Aqua);

           GridLayout.GetCell(1).ChangeType(StateEdge.StartPoint);
           GridLayout.GetCell(200).ChangeType(StateEdge.EndPoint);
        }
    }
}
