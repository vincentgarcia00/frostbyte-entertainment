using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using Frostbyte;
using System.IO;

namespace LevelEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<TileGroup> TileGroups { get; set; }

        public Vector Grid_Size
        {
            get
            {
                return gridSize;
            }
            set
            {
                gridSize = value;
                CreateGrid();
            }
        }

        private Vector gridSize;

        public Vector GridCell = new Vector(-1, -1);

        public int Cellsize = 32;

        public Tile SelectedTile { get; set; }

        public static Frostbyte.TileList<Tile> TileMap = new TileList<Tile>();

        public MainWindow()
        {
            this.InitializeComponent();

            This.mainWindow = this;

            ObservableCollection<Tile> tiles = new ObservableCollection<Tile>(){
                new Tile(){
                    Name="Floor",
                    Traversable=false,
                    Type=TileTypes.Floor,
                    FloorType = FloorTypes.Themed,
                    Orientation= Orientations.Down
                },
                new Tile(){
                    Name="Wall_Top",
                    Traversable=true,
                    Type=TileTypes.Wall,
                    Orientation = Orientations.Down
                },

                new Tile(){
                    Name="Wall_Left",
                    Traversable=true,
                    Type=TileTypes.Wall,
                    Orientation = Orientations.Right
                },

                new Tile(){
                    Name="Wall_Right",
                    Traversable=true,
                    Type=TileTypes.Wall,
                    Orientation = Orientations.Left
                },

                new Tile(){
                    Name="Wall_Bottom",
                    Traversable=true,
                    Type=TileTypes.Bottom,

                },

                new Tile(){
                    Name="Corner_TL",
                    Traversable=true,
                    Type=TileTypes.Corner,
                    Orientation=Orientations.Down
                },

                new Tile(){
                    Name="Corner_TR",
                    Traversable=true,
                    Type=TileTypes.Corner,
                    Orientation=Orientations.Left
                },

                new Tile(){
                    Name="Corner_BR",
                    Traversable=true,
                    Type=TileTypes.Corner,
                    Orientation=Orientations.Up
                }
                ,

                new Tile(){
                    Name="Corner_BL",
                    Traversable=true,
                    Type=TileTypes.Corner,
                    Orientation = Orientations.Right
                }
            };
            var stuff = new ObservableCollection<TileGroup>(){
                new TileGroup(tiles){
                    GroupName="Tiles"
                }
            };

            Objects.ItemsSource = stuff;

            Level.MouseDown += new MouseButtonEventHandler(Level_MouseDown);
            Level.MouseUp += new MouseButtonEventHandler(Level_MouseUp);
            Level.MouseMove += new MouseEventHandler(Level_MouseMove);

            Grid_Size = new Vector(100, 100);
            GridSize.DataContext = this;


            SaveMap.MouseUp += new MouseButtonEventHandler(SaveMap_MouseUp);
            SaveMap.MouseDown += new MouseButtonEventHandler(SaveMap_MouseDown);
        }

        void SaveMap_MouseDown(object sender, MouseButtonEventArgs e)
        {
            GenerateDictionary();
        }
        void SaveMap_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //open save box and then create all the crap that needs to get saved
        }

        private void GenerateDictionary()
        {
            TileMap.Clear();
            foreach (Tile tile in Level.Children)
            {
                int y = Grid.GetRow(tile);
                int x = Grid.GetColumn(tile);
                TileMap[y][x] = tile;
            }
        }



        private void CreateGrid()
        {
            Level.RowDefinitions.Clear();
            Level.ColumnDefinitions.Clear();
            while (Level.RowDefinitions.Count < Grid_Size.Y)
            {
                Level.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(Cellsize) });
            }
            while (Level.ColumnDefinitions.Count < Grid_Size.X)
            {
                Level.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(Cellsize) });
            }
            Level.Width = Level.RowDefinitions.Count * Cellsize;
            Level.Height = Level.ColumnDefinitions.Count * Cellsize;
        }

        void Level_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (Clear && e.MouseDevice.RightButton == MouseButtonState.Released)
            {
                SelectedTile = null;
                Clear = false;
            }
        }

        void Level_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.MouseDevice.LeftButton == MouseButtonState.Pressed)
            {
                if (SelectedTile != null)
                {
                    AddTile(GetCell(e.GetPosition(Level)));
                }
            }
            if (e.MouseDevice.RightButton == MouseButtonState.Pressed)
            {
                RemoveTile(GetCell(e.GetPosition(Level)));
            }
        }

        private Vector GetCell(Point point)
        {
            return new Vector((int)(point.X / Cellsize), (int)(point.Y / Cellsize));
        }

        void Level_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.MouseDevice.LeftButton == MouseButtonState.Pressed)
            {
                if (SelectedTile != null)
                {
                    GridCell = GetCell(e.GetPosition(Level));
                    AddTile(GridCell);
                }
            }
            if (e.MouseDevice.RightButton == MouseButtonState.Pressed)
            {
                Clear = true;
            }
        }

        private void AddTile(Vector newpt)
        {
            if (newpt != GridCell)
            {
                //deterimine which coord changed more
                Vector diff = newpt - GridCell;

                bool Horiz = (diff.X < 0 ? -diff.X : diff.X) > (diff.Y < 0 ? -diff.Y : diff.Y);

                Vector dir = Horiz ? new Vector(diff.X, 0) : new Vector(0, diff.Y);
                dir.Normalize();

                while ((Horiz ? GridCell.X != newpt.X : GridCell.Y != newpt.Y))
                {
                    if (GridCell.X < 0 || GridCell.Y < 0)
                        GridCell = newpt;
                    RemoveTile(GridCell);

                    //draw the selecteditem
                    Tile toadd = Tile.DeepCopy(SelectedTile);

                    //set the cell
                    Grid.SetRow(toadd, (int)GridCell.Y);
                    Grid.SetColumn(toadd, (int)GridCell.X);

                    Level.Children.Add(toadd);

                    if ((Horiz ? GridCell.X != newpt.X : GridCell.Y != newpt.Y))
                        GridCell += dir;
                }
                //set the new last grid point
                GridCell = newpt;
            }
        }

        private void RemoveTile(Vector newpt)
        {
            //remove old element
            List<UIElement> toRemove = new List<UIElement>();
            foreach (UIElement item in Level.Children)
            {
                int x = Grid.GetColumn(item);
                int y = Grid.GetRow(item);
                if ((int)GridCell.X == x && (int)GridCell.Y == y)
                {
                    toRemove.Add(item);
                }
            }
            foreach (var elem in toRemove)
            {
                Level.Children.Remove(elem);
            }
        }

        public bool Clear { get; set; }
    }


    public class TransformConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                Orientations o = (Orientations)value;
                return new RotateTransform(90 * (int)o);

            }
            return new RotateTransform(0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }

    public class TileConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                TileTypes tt = (TileTypes)value;
                string file = "error.png";
                switch (tt)
                {
                    case TileTypes.Wall:
                        file = "wall.png";
                        break;
                    case TileTypes.Bottom:
                        file = "sidewall.png";
                        break;
                    case TileTypes.Corner:
                        file = "corner.png";
                        break;
                    case TileTypes.Floor:
                        file = "floor.png";
                        break;
                    case TileTypes.Lava:
                        file = "lava.png";
                        break;
                    case TileTypes.Water:
                        file = "water.png";
                        break;
                    default:
                        file = "error.png";
                        break;
                }
                return new BitmapImage(new Uri(file, UriKind.RelativeOrAbsolute));
            }
            return new RotateTransform(0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}