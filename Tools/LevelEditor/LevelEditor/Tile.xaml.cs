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
        public BitmapSource Image { get; set; }

        public TileTypes Type { get; set; }

        public bool CollisionEnabled { get; set; }

        public FloorTypes FloorType { get; set; }

        public Element Theme { get; set; }

        public Orientations Orientation { get; set; }

        public Tile()
        {
            InitializeComponent();
        }
    }
}
