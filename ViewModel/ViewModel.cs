using Graph.ViewModel.ClientWPF.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Graph.ViewModel
{
    class ViewModel: INotifyPropertyChanged
    {
        private string _currentMousemode;


        private RelayCommand _modeWall;
        private RelayCommand _modeStart;
        private RelayCommand _modeEnd;
        private RelayCommand _getInfoCell;
        private RelayCommand _deleteAllWalls;
        private RelayCommand _createRandomWalls;
        private RelayCommand _A;

        public event PropertyChangedEventHandler PropertyChanged;


        public ViewModel()
        {
            CurrentMouseMode = "Текущий режим: ";
        }

        public string CurrentMouseMode
        {
            get { return _currentMousemode; }
            set
            {
                _currentMousemode = value;
                OnPropertyChanged("CurrentMouseMode");
            }
        }

        public RelayCommand ModeWall
        {
            get 
            {
                return _modeWall ?? (_modeWall = new RelayCommand(obj =>
                {
                    GridLayout.ChangeMode(MouseMode.WallMode);
                    CurrentMouseMode = "Текущий режим: установка/удаление стен";

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
                    CurrentMouseMode = "Текущий режим: установка начальной точки";

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
                    CurrentMouseMode = "Текущий режим: установка конечной точки";

                }));
            }
        }
        public RelayCommand GetInfo
        {
            get
            {
                return _getInfoCell ?? (_getInfoCell = new RelayCommand(obj =>
                {
                    GridLayout.ChangeMode(MouseMode.GetInfo);
                    CurrentMouseMode = "Текущий режим: получение информации о клетке";

                }));
            }
        }
        public RelayCommand CreateRandomWalls
        {
            get
            {
                return _createRandomWalls ?? (_createRandomWalls = new RelayCommand(obj =>
                {
                    GridLayout.CreateRandomWalls();
                    CurrentMouseMode = "Текущий режим: создание рандомных стен";

                }));
            }
        }
        public RelayCommand DeleteAllWalls
        {
            get
            {
                return _deleteAllWalls ?? (_deleteAllWalls = new RelayCommand(obj =>
                {
                    GridLayout.DeleteWalls();
                    CurrentMouseMode = "Текущий режим: удаление всех стен";

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
                    if (path != null)
                        foreach (Cell path_node in path)
                        {
                            path_node.ChangeColor(Colors.Aqua);
                        }
                    else
                        MessageBox.Show("Пути нет");
                    

                    ASearch.a.ChangeColor(Colors.Green);
                    ASearch.b.ChangeColor(Colors.Red);
                }));
            }
        }

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
