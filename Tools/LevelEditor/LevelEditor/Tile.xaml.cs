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
using System.Xml.Linq;

namespace LevelEditor
{
    /// <summary>
    /// Interaction logic for Tile.xaml
    /// </summary>
    public partial class Tile : UserControl
    {
        Frostbyte.Tile t = new Frostbyte.Tile();

        public TileTypes Type
        {
            get
            {
                return t.Type;
            }
            set
            {
                t.Type = value;
            }
        }

        public string InstanceName
        {
            get
            {
                return t.InstanceName;
            }
            set
            {
                t.InstanceName = value;
            }
        }

        public bool Traversable
        {
            get
            {
                return t.Traversable;
            }
            set
            {
                t.Traversable = value;
            }
        }

        public FloorTypes FloorType
        {
            get
            {
                return t.FloorType;
            }
            set
            {
                t.FloorType = value;
            }
        }

        public Element Theme
        {
            get
            {
                return t.Theme;
            }
            set
            {
                t.Theme = value;
            }
        }

        public Orientations Orientation
        {
            get
            {
                return t.Orientation;
            }
            set
            {
                t.Orientation = value;
            }
        }

        public Frostbyte.Tile ValueTile
        {
            get
            {
                return t;
            }
            set
            {
                t = value;
            }
        }

        public bool IsSpecialObject { get; set; }

        public bool Active { get;set; }

        public bool InMenu { get; set; }

        /// <summary>
        /// Stores if this tile is a part of a given container
        /// </summary>
        public LevelObject Container { get; set; }

        public Tile()
        {
            InitializeComponent();
            //TileImage.RenderTransformOrigin = new Point(.5,.5);
            //Binding b = new Binding("Orientation");
            //b.Converter=new TransformConverter();
            //SetBinding(TileImage.RenderTransform,b.
            InMenu = true;
        }

        public Tile(Tile SelectedTile)
        {
            InitializeComponent();
            Tile other = (Tile)SelectedTile.MemberwiseClone();
            other.Active = true;
            other.InMenu = false;
            Type = other.Type;
            InstanceName = "InsertName";
            Traversable = other.Traversable;
            FloorType = other.FloorType;
            Theme = other.Theme;
            Orientation = other.Orientation;
            Active = other.Active;
            InMenu = other.InMenu;
            DataContext = other;
            
        }

        public static Tile DeepCopy(Tile SelectedTile)
        {
            Tile other = new Tile(SelectedTile);
            return other;
        }

        public override string ToString()
        {
            return string.Format("Name:{0}\nType:{1}\nCollides:{2}\nTheme:{3}\nOrientation:{4}", Name, Type, Traversable, Theme, Orientation);
        }

        public XElement ToXML()
        {
            XElement e = new XElement("Tile");
            e.SetAttributeValue("Type", Type);
            e.SetAttributeValue("InstanceName", InstanceName);
            e.SetAttributeValue("Collision", Traversable);
            e.SetAttributeValue("Theme", Theme);
            e.SetAttributeValue("Orientation", Orientation);
            Point GridCell = new Point(Grid.GetColumn(this), Grid.GetRow(this));
            e.SetAttributeValue("GridCell", GridCell);
            return e;
        }

        public Tile Parse(XElement elem)
        {
#if DEBUG
            try
            {
#endif
                Tile t = new Tile();
                foreach (XAttribute attr in elem.Attributes())
                {
                    if (attr.Name == "Type")
                    {
                        t.Type = (TileTypes)Enum.Parse(typeof(TileTypes), attr.Value);
                    }
                    else if (attr.Name == "InstanceName")
                    {
                        t.InstanceName = attr.Value;
                    }
                    else if (attr.Name == "Collision")
                    {
                        t.Traversable = bool.Parse(attr.Value);
                    }
                    else if (attr.Name == "Theme")
                    {
                        t.Theme = (Element)Enum.Parse(typeof(Element), elem.Attribute("Theme").Value);
                    }
                    else if (attr.Name == "Orientation")
                    {
                        t.Orientation = (Orientations)Enum.Parse(typeof(Orientations), elem.Attribute("Theme").Value);
                    }
                    else if (attr.Name == "GridCell")
                    {
                        Index2D i = Index2D.Parse(attr.Value);
                        Grid.SetColumn(this, i.X);
                        Grid.SetRow(this, i.Y);
                    }
                }
                return t;
#if DEBUG
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetBaseException().Message);
                return null;
            }
        }
#endif
    }
}

