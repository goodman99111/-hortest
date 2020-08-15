using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Graph
{
    class ASearch
    {
        public static Cell a = GridLayout.GetCell(1);
        public static Cell b = GridLayout.GetCell(20);

        public static void SetStart(Cell cell)
        {
            a.ChangeType(StateEdge.Edge);
            a = cell;
            a.ChangeType(StateEdge.StartPoint);
        }

        public static void SetEnd(Cell cell)
        {
            b.ChangeType(StateEdge.Edge);
            b = cell;
            b.ChangeType(StateEdge.EndPoint);
        }



        public static List<Cell> Search(int a, int b)
        {
            List<Cell> awaitCell = new List<Cell>();
            List<Cell> searchedCell = new List<Cell>();

            var cellStart = GridLayout.GetCell(a);
            cellStart.CameFrom = null;
            cellStart.PathLengthFromStart = 0;
            cellStart.HeuristicEstimatePathLength = GetHeuristicPathLength(a, b);

            awaitCell.Add(cellStart);

            while(awaitCell.Count > 0)
            {
                //Из списка точек на рассмотрение выбирается точка с наименьшим F. 
                var cell = awaitCell.OrderBy(edge => edge.EstimateFullPathLength).First();

                
                //Если cell — цель, то мы нашли маршрут.
                if (cell.Num == b)
                {
                    foreach (Cell path_node in searchedCell)
                    {
                        path_node.ChangeColor(Colors.Aquamarine);
                    }
                    return GetPathForNode(cell);
                }
                    
                //Переносим cell из списка ожидающих рассмотрения в список уже рассмотренных.
                awaitCell.Remove(cell);
                searchedCell.Add(cell);


                //Для каждой из точек, соседних для cell (обозначим эту соседнюю точку Y), делаем следующее:
                foreach (var child in GetPar(cell, b))
                {
                    //Если Y уже находится в рассмотренных — пропускаем ее
                    if (searchedCell.Count(node => node.Num == child.Num) > 0)
                        continue;


                    //Если Y еще нет в списке на ожидание — добавляем ее туда, 
                    //запомнив ссылку на X и рассчитав Y.G (это X.G + расстояние от X до Y) и Y.H.
                    var openNode = awaitCell.FirstOrDefault(node => node.Num == child.Num);
                    if (openNode == null)
                    {
                        child.CameFrom = cell;
                        awaitCell.Add(child);
                    }
                    else
                    {
                        if (openNode.PathLengthFromStart > child.PathLengthFromStart)
                        {
                            //Если же Y в списке на рассмотрение — проверяем, если X.G + расстояние от 
                            //X до Y<Y.G, значит мы пришли в точку Y более коротким путем, заменяем Y.G 
                            //на X.G +расстояние от X до Y, а точку, из которой пришли в Y на X.
                            openNode.CameFrom = cell;
                            openNode.PathLengthFromStart = child.PathLengthFromStart;
                        }
                    }
                }
                
            }
            

            return null;
        }


        //Расстояние между соседними клетками
        private static int GetDistanceBetweenNeighbours()
        {
            return 1;
        }

        //Функция примерной оценки расстояния до цели:
        private static int GetHeuristicPathLength(int a, int b)
        {
            Point cell_a = GridLayout.GetCell(a).Point;
            Point cell_b = GridLayout.GetCell(b).Point;
            return (int)Math.Abs(cell_a.X - cell_b.X) + (int)Math.Abs(cell_a.Y - cell_b.Y);
        }

        private static List<Cell> GetPathForNode(Cell cell)
        {
            var spis = GridLayout.edges;
            var result = new List<Cell>();
            var currentNode = cell;
            while (currentNode.CameFrom != null)
            {
                result.Add(currentNode);
                currentNode = currentNode.CameFrom;
            }
            result.Reverse();
            return result;
        }

        public static Collection<Cell> GetPar(Cell cell, int b)
        {
            var listpar = new Collection<Cell>();
            int count = cell.Num - 1;

            if (count % GridLayout._sizeW == GridLayout._sizeW - 1)
            {
                try { listpar.Add(GridLayout.edges[count - 1]); } catch { }
            }
            else if (count % GridLayout._sizeW == 0)
            {
                try { listpar.Add(GridLayout.edges[count + 1]); } catch { }
            }
            else
            {
                try { listpar.Add(GridLayout.edges[count - 1]); } catch { }
                try { listpar.Add(GridLayout.edges[count + 1]); } catch { }
            }
            
            try { listpar.Add(GridLayout.edges[count - GridLayout._sizeW]); } catch { }
            try { listpar.Add(GridLayout.edges[count + GridLayout._sizeW]); } catch { }

            /*
            foreach(Cell node in listpar)
            {
                if(node.TypeEdge == StateEdge.Wall)
                {
                    listpar.Remove(node);
                }
            }
            */
            int i = listpar.Count - 1;
            while(i >= 0)
            {
                if (listpar[i].TypeEdge == StateEdge.Wall)
                {
                    listpar.RemoveAt(i);
                    i--;
                }
                else
                    i--;
            }
            
            foreach (Cell node in listpar)
            {
                node.PathLengthFromStart = cell.PathLengthFromStart + GetDistanceBetweenNeighbours();
                node.HeuristicEstimatePathLength = GetHeuristicPathLength(node.Num, b);
            }

            return listpar;
        }
    }
}
