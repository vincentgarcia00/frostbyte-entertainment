using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Frostbyte;

namespace LevelEditor
{
    /// <summary>
    /// Interaction logic for Tile.xaml
    /// </summary>
    public partial class Tile : UserControl
    {
        public TileTypes Type { get; set; }

        public string InstanceName { get; set; }

        public bool Traversable { get; set; }

        public FloorTypes FloorType { get; set; }

        public Element Theme { get; set; }

        public Orientations Orientation { get; set; }

        public Tile()
        {
            InitializeComponent();
            //TileImage.RenderTransformOrigin = new Point(.5,.5);
            //Binding b = new Binding("Orientation");
            //b.Converter=new TransformConverter();
            //SetBinding(TileImage.RenderTransform,b.
        }

        public Tile(Tile SelectedTile)
        {
            InitializeComponent();
            Tile other = (Tile)SelectedTile.MemberwiseClone();
            Type = other.Type;
            InstanceName = "InsertName";
            Traversable = other.Traversable;
            FloorType = other.FloorType;
            Theme = other.Theme;
            Orientation = other.Orientation;
            DataContext = other;
        }

        public static Tile DeepCopy(Tile SelectedTile)
        {
            Tile other = new Tile(SelectedTile);
            
            //    {
            //        Name=SelectedTile.Name.ToString(),
            //        CollisionEnabled=SelectedTile.CollisionEnabled?true:false,
            //        Type=(TileTypes)SelectedTile.Type.GetTypeCode(),
            //        FloorType = (FloorTypes)SelectedTile.FloorType.GetTypeCode(),
            //        Image = new BitmapImage(new Uri( "wall.png",UriKind.RelativeOrAbsolute)),
            //        Orientation= (Orientations)SelectedTile.Orientation.GetTypeCode()
            //    }
            //;
            return other;
        }

        public override string ToString()
        {
            return string.Format("Name:{0}\nType:{1}\nCollides:{2}\nTheme:{3}\nOrientation:{4}", Name, Type, Traversable, Theme, Orientation);
        }
    }
}
