using Graph.ViewModel.ClientWPF.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Graph.ViewModel
{
    class ViewModel
    {
        private string _currentMousemode;


        private RelayCommand _modeWall;
        private RelayCommand _modeStart;
        private RelayCommand _modeEnd;
        private RelayCommand _A;

        public event PropertyChangedEventHandler PropertyChanged;


        public RelayCommand ModeWall
        {
            get 
            {
                return _modeWall ?? (_modeWall = new RelayCommand(obj =>
                {
                    GridLayout.ChangeMode(MouseMode.WallMode);
                }));
            }
        }
        public RelayCommand ModeStart
        {
            get
            {
                return _modeStart ?? (_modeStart = new RelayCommand(obj =>
                {
                    GridLayout.ChangeMode(MouseMode.StartMode);
                }));
            }
        }
        public RelayCommand ModeEnd
        {
            get
            {
                return _modeEnd ?? (_modeEnd = new RelayCommand(obj =>
                {
                    GridLayout.ChangeMode(MouseMode.EndMode);
                }));
            }
        }
        public RelayCommand A
        {
            get
            {
                return _A ?? (_A = new RelayCommand(obj =>
                {
                    GridLayout.ClearPath();
                    List<Cell> path = ASearch.Search(ASearch.a.Num, ASearch.b.Num);
                    foreach (Cell path_node in path)
                    {
                        path_node.ChangeColor(Colors.Aqua);
                    }
                    
                    ASearch.a.ChangeColor(Colors.Green);
                    ASearch.b.ChangeColor(Colors.Red);
                }));
            }
        }
    }
}
