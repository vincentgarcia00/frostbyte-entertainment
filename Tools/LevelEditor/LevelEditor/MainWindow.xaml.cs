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

        public MainWindow()
        {
            this.InitializeComponent();

            ObservableCollection<Tile> tiles =  new ObservableCollection<Tile>(){
                new Tile(){
                    Name="Floor",
                    CollisionEnabled=false,
                    Type=TileTypes.Floor,
                    FloorType = FloorTypes.Themed,
                    Image = new BitmapImage(new Uri( "floor.png",UriKind.RelativeOrAbsolute)),
                    Orientation= Orientations.Down
                },
                new Tile(){
                    Name="Wall_Top",
                    CollisionEnabled=true,
                    Type=TileTypes.Wall,
                    Image =  new BitmapImage(new Uri( "wall.png", UriKind.RelativeOrAbsolute)),
                    Orientation = Orientations.Down
                },

                new Tile(){
                    Name="Wall_Left",
                    CollisionEnabled=true,
                    Type=TileTypes.Wall,
                    Image = new BitmapImage(new Uri( "wall.png", UriKind.RelativeOrAbsolute)),
                    Orientation = Orientations.Right
                },

                new Tile(){
                    Name="Wall_Right",
                    CollisionEnabled=true,
                    Type=TileTypes.Wall,
                    Image = new BitmapImage(new Uri( "wall.png", UriKind.RelativeOrAbsolute)),
                    Orientation = Orientations.Left
                },

                new Tile(){
                    Name="Wall_Bottom",
                    CollisionEnabled=true,
                    Type=TileTypes.Bottom,
                    Image = new BitmapImage(new Uri( "wall.png", UriKind.RelativeOrAbsolute)),

                },

                new Tile(){
                    Name="Corner_TL",
                    CollisionEnabled=true,
                    Type=TileTypes.Corner,
                    Image = new BitmapImage(new Uri( "corner.png", UriKind.RelativeOrAbsolute)),
                    Orientation=Orientations.Down
                },

                new Tile(){
                    Name="Corner_TR",
                    CollisionEnabled=true,
                    Type=TileTypes.Corner,
                    Image =  new BitmapImage(new Uri( "corner.png", UriKind.RelativeOrAbsolute)),
                    Orientation=Orientations.Left
                },

                new Tile(){
                    Name="Corner_BR",
                    CollisionEnabled=true,
                    Type=TileTypes.Corner,
                    Image = new BitmapImage(new Uri( "corner.png", UriKind.RelativeOrAbsolute)),
                    Orientation=Orientations.Up
                }
                ,

                new Tile(){
                    Name="Corner_BL",
                    CollisionEnabled=true,
                    Type=TileTypes.Corner,
                    Image =  new BitmapImage(new Uri( "corner.png", UriKind.RelativeOrAbsolute)),
                    Orientation = Orientations.Right
                }
                    
                
            };
            var stuff = new ObservableCollection<TileGroup>(){
                new TileGroup(tiles){
                    GroupName="Tiles"
                }
            };

            Objects.ItemsSource = stuff;
        }
    }


    public class TransformConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                Orientations o = (Orientations)value;
                return new RotateTransform(90*(int)o);

            }
            return new RotateTransform(0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}