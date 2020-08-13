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
    
    public class GridLayout : Grid
    {
        static GridLayout()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GridLayout), new FrameworkPropertyMetadata(typeof(GridLayout)));
            
        }
        public static List<Cell> edges = new List<Cell>();

        public GridLayout()
        {
            
        }

        public  int _sizeH = 19;
        public static int _sizeW = 20;


        protected override void OnInitialized(EventArgs e)
        {
            Width = 25 * _sizeW;
            Height = 25 * _sizeH;
            CreateCells();
        }

        private void CreateCells()
        {
            int count = 1;
            for (int i = 0; i < _sizeH; i++)
            {
                for (int k = 0; k < _sizeW; k++)
                {
                    
                    Cell cell = new Cell(count, StateEdge.Edge);
                    cell.Point = new Point(i, k);
                    this.Children.Add(cell);
                    cell.Margin = new Thickness(-492+k*49,-338+i*49, 0, 0);

                    edges.Add(cell);

                    count++;
                    
                }
            }
        }
        
        public static Cell GetCell(int n)
        {
            Cell cell = edges.Find(c => n == c.Num);
            return cell;
        }
    
        public static void ChangeMode(MouseMode mouseMode)
        {
            Cell.mouseMode = mouseMode;
        }

        public static void ClearPath()
        {
            foreach (Cell cell in edges)
            {
                if(cell.TypeEdge != StateEdge.Wall)
                    cell.ChangeType(StateEdge.Edge);
            }
        }

       
    }

    public enum MouseMode
    {
        WallMode,
        StartMode,
        EndMode,
        None
    }
}
