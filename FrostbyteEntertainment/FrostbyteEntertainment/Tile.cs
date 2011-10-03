using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Xml.Linq;

namespace Frostbyte
{

    internal class Tile : TileHelper<Tile>
    {
        internal static readonly int TileSize = 64;

        internal bool Traversable { get; set; }

        internal TileTypes Type { get; set; }

        internal string InstanceName { get; set; }

        internal FloorTypes FloorType { get; set; }

        internal Element Theme { get; set; }

        internal Orientations Orientation { get; set; }

        internal Tile()
        {
            Traversable = true;
			FloorType = FloorTypes.DEFAULT;
        }
        
        public XElement ToXML()
        {
            throw new NotImplementedException();
        }

        public Tile Parse(XElement elem)
        {
            throw new NotImplementedException();
        }
    }

    /// \file Tile.cs This is Shared with the Level Editor



}
