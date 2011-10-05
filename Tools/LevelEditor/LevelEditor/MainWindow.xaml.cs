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

        public LevelPart SelectedObject { get; set; }

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
                    Traversable=true,
                    Type=TileTypes.Floor,
                    FloorType = FloorTypes.Themed,
                    Orientation= Orientations.Down,
                    Active=true
                },
                new Tile(){
                    Name="Wall_Top",
                    Traversable=true,
                    Type=TileTypes.Wall,
                    Orientation = Orientations.Down,
                    Active=true
                },

                new Tile(){
                    Name="Wall_Left",
                    Traversable=false,
                    Type=TileTypes.Wall,
                    Orientation = Orientations.Right,
                    Active=true
                },

                new Tile(){
                    Name="Wall_Right",
                    Traversable=false,
                    Type=TileTypes.Wall,
                    Orientation = Orientations.Left,
                    Active=true
                },

                new Tile(){
                    Name="Wall_Bottom",
                    Traversable=false,
                    Type=TileTypes.Bottom,
                    Active=true
                },

                new Tile(){
                    Name="Corner_TL",
                    Traversable=false,
                    Type=TileTypes.Corner,
                    Orientation=Orientations.Down,
                    Active=true
                },

                new Tile(){
                    Name="Corner_TR",
                    Traversable=false,
                    Type=TileTypes.Corner,
                    Orientation=Orientations.Left,
                    Active=true
                },

                new Tile(){
                    Name="Corner_BR",
                    Traversable=false,
                    Type=TileTypes.Corner,
                    Orientation=Orientations.Up,
                    Active=true
                }
                ,

                new Tile(){
                    Name="Corner_BL",
                    Traversable=false,
                    Type=TileTypes.Corner,
                    Orientation = Orientations.Right,
                    Active=true
                }
            };
            ObservableCollection<Tile> rooms = new ObservableCollection<Tile>()
            {
                new Tile()
                {
                    Name="Room",
                    Traversable=false,
                    Type = TileTypes.Room,
                    Orientation = Orientations.Down,
                    FloorType = FloorTypes.Themed,
                    IsSpecialObject=true,
                    Active=true
                },
                new Tile()
                {
                    Name="Walls",
                    Traversable=false,
                    Type = TileTypes.Wall,
                    Orientation = Orientations.Down,
                    FloorType = FloorTypes.Themed,
                    IsSpecialObject=true,
                    Active=true
                },
                new Tile()
                {
                    Name="Wall",
                    Traversable=false,
                    Type = TileTypes.Wall,
                    Orientation = Orientations.Down,
                    FloorType = FloorTypes.Themed,
                    IsSpecialObject=true,
                    Active=true
                },
                new Tile()
                {
                    Name="Floor",
                    Traversable=false,
                    Type = TileTypes.Floor,
                    Orientation = Orientations.Down,
                    FloorType = FloorTypes.Themed,
                    IsSpecialObject=true,
                    Active=true
                }
            };
            var stuff = new ObservableCollection<TileGroup>(){
                new TileGroup(tiles){
                    GroupName="Tiles"
                },
                new TileGroup(rooms){
                    GroupName="Rooms"
                }
            };

            //clears selected items
            foreach (var tg in stuff)
            {
                tg.Tiles.SelectedIndex = -1;
            }

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
            //obsolete
        }
        void SaveMap_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //open save box and then create all the crap that needs to get saved
        }

        void LoadGrid(TileList<Tile> t)
        {
            TileMap = t;
            var l = t.Data;
            var tiles = l.Item2;
            foreach (var list in tiles)
            {

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
            if (CancelSelection && e.MouseDevice.RightButton == MouseButtonState.Released)
            {
                //clear selections
                foreach (TileGroup elem in Objects.Items)
                {
                    elem.Tiles.SelectedIndex = -1;
                }
                ClearTile = true;
                CancelSelection = false;
            }

            Moving = false;
        }

        void Level_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.MouseDevice.LeftButton == MouseButtonState.Pressed)
            {
                if (SelectedTile != null)
                {
                    Moving = true;
                    if (!SelectedTile.IsSpecialObject)
                        AddTile(GetCell(e.GetPosition(Level)));
                    else
                    {
                        // fill all of it
                    }
                }
            }
            if (ClearTile && e.MouseDevice.RightButton == MouseButtonState.Pressed)
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
            //do some handling here for end of room that skips the rest
            if (SelectedTile != null && SelectedTile.IsSpecialObject)
            {
                if (e.MouseDevice.LeftButton == MouseButtonState.Pressed)
                {


                    if (SelectedObject == null)
                    {
                        Index2D i = new Index2D((int)GridCell.X, (int)GridCell.Y);
                        // fill the pieces
                        if (SelectedTile.Name == "Room")
                        {
                            SelectedObject = new Room(i)
                            {
                                FloorType = SelectedTile.FloorType,
                            };
                        }
                        else if (SelectedTile.Name == "Walls")
                        {
                            SelectedObject = new BorderWalls(i)
                            {
                                FloorType = SelectedTile.FloorType,
                            };
                        }
                        else if (SelectedTile.Name == "Wall")
                        {
                            SelectedObject = new Wall(i)
                            {
                                FloorType = SelectedTile.FloorType,
                            };
                        }
                        else if (SelectedTile.Name == "Floor")
                        {
                            SelectedObject = new Floor(i)
                            {
                                FloorType = SelectedTile.FloorType,
                            };
                        }
                    }
                    else//here we complete things for the object
                    {
                        SelectedObject.EndCell = new Index2D((int)GridCell.X, (int)GridCell.Y);

                        //determine what it is
                        List<Tile> tiles = TileMap.Add(SelectedObject);

                        foreach (Tile t in tiles)
                        {
                            Grid.SetColumn(t, t.GridCell.X);
                            Grid.SetRow(t, t.GridCell.Y);
                            This.mainWindow.Level.Children.Add(t);
                        }

                        SelectedObject = null;
                    }
                }
                else if (e.MouseDevice.RightButton == MouseButtonState.Pressed)
                {
                }
            }
            else
            {
                if (e.MouseDevice.LeftButton == MouseButtonState.Pressed)
                {
                    if (SelectedTile != null)
                    {
                        GridCell = GetCell(e.GetPosition(Level));
                        FirstClick = true;
                        AddTile(GridCell);
                    }
                }
                else if (e.MouseDevice.RightButton == MouseButtonState.Pressed)
                {
                    if (ClearTile)
                    {
                        GridCell = GetCell(e.GetPosition(Level));
                        RemoveTile(GridCell);
                        FirstClick = true;
                    }
                    CancelSelection = true;
                }
            }
        }

        private void AddTile(Vector newpt)
        {
            if (newpt != GridCell || FirstClick)
            {
                if (GridCell.X < 0 || GridCell.Y < 0 || GridCell.X > gridSize.X || GridCell.Y > gridSize.Y)
                {
                    GridCell = newpt;
                }
                //deterimine which coord changed more
                Vector diff = newpt - GridCell;

                bool Horiz = (diff.X < 0 ? -diff.X : diff.X) > (diff.Y < 0 ? -diff.Y : diff.Y);

                Vector dir = Horiz ? new Vector(diff.X, 0) : new Vector(0, diff.Y);
                dir.Normalize();

                while (FirstClick || (Horiz ? GridCell.X != (Moving ? newpt.X + dir.X : newpt.X) : GridCell.Y != (Moving ? newpt.Y + dir.Y : newpt.Y)))
                {
                    if (GridCell.X < 0 || GridCell.Y < 0)
                        GridCell = newpt;
                    RemoveTile(GridCell);

                    //draw the selecteditem
                    Tile toadd = Tile.DeepCopy(SelectedTile);

                    //set the cell
                    int y = (int)GridCell.Y;
                    int x = (int)GridCell.X;
                    Grid.SetRow(toadd, y);
                    Grid.SetColumn(toadd, x);

                    Level.Children.Add(toadd);
                    if (!TileMap.Add(toadd))
                    {
                        TileMap.Add(toadd, x, y);
                    }

                    if ((Horiz ? GridCell.X != (Moving ? newpt.X + dir.X : newpt.X) : GridCell.Y != (Moving ? newpt.Y + dir.Y : newpt.Y)))
                        GridCell += dir;
                    else
                    {
                        FirstClick = false;
                    }
                }



                //set the new last grid point
                GridCell = newpt;
            }
        }

        private void RemoveTile(Vector newpt)
        {
            //remove old element
            List<Tile> toRemove = new List<Tile>();

            if (newpt != GridCell || FirstClick)
            {
                if (GridCell.X < 0 || GridCell.Y < 0 || GridCell.X > gridSize.X || GridCell.Y > gridSize.Y)
                {
                    GridCell = newpt;
                }
                //deterimine which coord changed more
                Vector diff = newpt - GridCell;

                bool Horiz = (diff.X < 0 ? -diff.X : diff.X) > (diff.Y < 0 ? -diff.Y : diff.Y);

                Vector dir = Horiz ? new Vector(diff.X, 0) : new Vector(0, diff.Y);
                dir.Normalize();

                while (FirstClick || (Horiz ? GridCell.X != (Moving ? newpt.X + dir.X : newpt.X) : GridCell.Y != (Moving ? newpt.Y + dir.Y : newpt.Y)))
                {
                    if (GridCell.X < 0 || GridCell.Y < 0)
                        GridCell = newpt;
                    foreach (Tile item in Level.Children)
                    {
                        int x = Grid.GetColumn(item);
                        int y = Grid.GetRow(item);
                        if ((int)GridCell.X == x && (int)GridCell.Y == y)
                        {
                            toRemove.Add(item);
                        }
                    }
                    if ((Horiz ? GridCell.X != (Moving ? newpt.X + dir.X : newpt.X) : GridCell.Y != (Moving ? newpt.Y + dir.Y : newpt.Y)))
                        GridCell += dir;
                    FirstClick = false;
                }
                //set the new last grid point
                GridCell = newpt;
            }
            foreach (var elem in toRemove)
            {
                Level.Children.Remove(elem);
                if (!TileMap.Remove(elem))
                {
                    int x = Grid.GetColumn(elem);
                    int y = Grid.GetRow(elem);
                    TileMap.Remove(elem, x, y);
                }
            }
        }

        /// <summary>
        /// Wheter we can clear tiles or not
        /// </summary>
        public bool ClearTile { get; set; }
        /// <summary>
        /// We can cancel our current selectin
        /// </summary>
        public bool CancelSelection { get; set; }
        /// <summary>
        /// This is the first left click
        /// </summary>
        public bool FirstClick { get; set; }
        /// <summary>
        /// The mouse is moving, keep drawing
        /// </summary>
        public bool Moving { get; set; }
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
                    case TileTypes.Room:
                        //do some magic to show pic for the walls etc
                        file = "room.png";
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