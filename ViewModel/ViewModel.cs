using Graph.ViewModel.ClientWPF.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
        private static ViewModel _instance;

        private string _currentMousemode;
        private string _timePath;
        private string _lengthPath;


        private RelayCommand _modeWall;
        private RelayCommand _modeStart;
        private RelayCommand _modeEnd;
        private RelayCommand _getInfoCell;
        private RelayCommand _deleteAllWalls;
        private RelayCommand _createRandomWalls;
        private RelayCommand _clearPath;
        private RelayCommand _A;

        public event PropertyChangedEventHandler PropertyChanged;


        public ViewModel()
        {
            CurrentMouseMode = "Текущий режим: ";
            TimePath = "Время(мс): ";
            LengthPath = "Длина пути: ";
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
        public string TimePath
        {
            get { return _timePath; }
            set
            {
                _timePath = value;
                OnPropertyChanged("TimePath");
            }
        }
        public string LengthPath
        {
            get { return _lengthPath; }
            set
            {
                _lengthPath = value;
                OnPropertyChanged("LengthPath");
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
        public RelayCommand ClearPath
        {
            get
            {
                return _clearPath ?? (_clearPath = new RelayCommand(obj =>
                {
                    GridLayout.ClearPath();
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
                    Stopwatch sw = Stopwatch.StartNew();
                    List<Cell> path = ASearch.Search(ASearch.a.Num, ASearch.b.Num);
                    sw.Stop();


                    TimePath = $"Время(мс): {sw.ElapsedMilliseconds}";
                    LengthPath = path != null ? $"Длина пути: {path.Count - 1}" : $"Длина пути: 0";
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

        public static ViewModel GetViewModel()
        {
            return _instance != null ? _instance : _instance = new ViewModel();
        }

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public void SetSpecifics(string time, string length)
        {
            TimePath = time;
            LengthPath = length;
        }
    }
}
