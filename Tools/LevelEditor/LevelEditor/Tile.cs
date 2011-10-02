using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Frostbyte;
using System.Windows.Media.Imaging;

namespace LevelEditor
{

    public partial class Tile 
    {
        public BitmapSource Image { get; set; }

        public TileTypes Type { get; set; }

        public bool CollisionEnabled { get; set; }

        public FloorTypes FloorType { get; set; }

        public Element Theme { get; set; }

        public string Name { get; set; }

        public Orientations Orientation { get; set; }
    }
}
