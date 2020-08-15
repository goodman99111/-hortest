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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Graph
{
    /// <summary>
    /// Выполните шаги 1a или 1b, а затем 2, чтобы использовать этот пользовательский элемент управления в файле XAML.
    ///
    /// Шаг 1a. Использование пользовательского элемента управления в файле XAML, существующем в текущем проекте.
    /// Добавьте атрибут XmlNamespace в корневой элемент файла разметки, где он 
    /// будет использоваться:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Graph"
    ///
    ///
    /// Шаг 1б. Использование пользовательского элемента управления в файле XAML, существующем в другом проекте.
    /// Добавьте атрибут XmlNamespace в корневой элемент файла разметки, где он 
    /// будет использоваться:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Graph;assembly=Graph"
    ///
    /// Потребуется также добавить ссылку из проекта, в котором находится файл XAML,
    /// на данный проект и пересобрать во избежание ошибок компиляции:
    ///
    ///     Щелкните правой кнопкой мыши нужный проект в обозревателе решений и выберите
    ///     "Добавить ссылку"->"Проекты"->[Поиск и выбор проекта]
    ///
    ///
    /// Шаг 2)
    /// Теперь можно использовать элемент управления в файле XAML.
    ///
    ///     <MyNamespace:MyCustomControl/>
    ///
    /// </summary>
    public class Cell : Control
    {
        private Color CurrColor = Colors.White;
        public StateEdge TypeEdge = StateEdge.Edge;
        public static MouseMode mouseMode = MouseMode.None;
        public int Num { get; set; }
        public Point Point { get; set; }

        public int PathLengthFromStart { get; set; }
        public Cell CameFrom { get; set; }
        public int HeuristicEstimatePathLength { get; set; }
        public int EstimateFullPathLength
        {
            get
            {
                return this.PathLengthFromStart + this.HeuristicEstimatePathLength;
            }
        }

        public List<Cell> ListAdjacency = new List<Cell>();

        static Cell()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Cell), new FrameworkPropertyMetadata(typeof(Cell)));
            
        }
        public Cell(int num, StateEdge stateEdge)
        {
            if (stateEdge == StateEdge.Edge)
            { 
                this.Background = new SolidColorBrush(Colors.White);
                CurrColor = Colors.White;
            }
            if(stateEdge == StateEdge.Wall)
            {
                this.Background = new SolidColorBrush(Colors.Gray);
                CurrColor = Colors.Gray;
            }
            if(stateEdge == StateEdge.StartPoint)
            {
                this.Background = new SolidColorBrush(Colors.Blue);
                CurrColor = Colors.Blue;
            }
            if (stateEdge == StateEdge.EndPoint)
            {
                this.Background = new SolidColorBrush(Colors.Red);
                CurrColor = Colors.Red;
            }


            this.BorderBrush = new SolidColorBrush(Colors.Black);
            this.BorderThickness = new Thickness(1);

            TypeEdge = stateEdge;
            Num = num;
        }


        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            
            if(MouseMode.WallMode == mouseMode && TypeEdge == StateEdge.Edge)
            {
                ChangeType(StateEdge.Wall);
                return;
            }
            else if(MouseMode.WallMode == mouseMode && TypeEdge == StateEdge.Wall)
            {
                ChangeType(StateEdge.Edge);
                return;
            }

            if (MouseMode.StartMode == mouseMode)
            {
                ASearch.SetStart(this);
                return;
            }

            if (MouseMode.EndMode == mouseMode)
            {
                ASearch.SetEnd(this);
                return;
            }

            if(MouseMode.GetInfo == mouseMode)
            {
                MessageBox.Show(TypeEdge.ToString());
                return;
            }

        }
        
        public void ChangeColor(Color color)
        {
            ColorAnimation colorAnimation = new ColorAnimation();
            colorAnimation.From = CurrColor;
            colorAnimation.To = color;
            colorAnimation.Duration = TimeSpan.FromSeconds(0.2);

            this.Background.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);

            CurrColor = color;
        }

        public void ChangeType(StateEdge state)
        {
            if (state == StateEdge.Edge)
            {
                ChangeColor(Colors.White);
                TypeEdge = state;
            }
            else if (state == StateEdge.Wall)
            {
                ChangeColor(Colors.Gray);
                TypeEdge = state;
            }
            else if (state == StateEdge.StartPoint)
            {
                ChangeColor(Colors.Green);
                TypeEdge = state;
            }
            else if (state == StateEdge.EndPoint)
            {
                ChangeColor(Colors.Red);
                TypeEdge = state;
            }
        }
    }



    public enum StateEdge
    {
        StartPoint,
        EndPoint,
        Edge,
        Wall
    }
}
