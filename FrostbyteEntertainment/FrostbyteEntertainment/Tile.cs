using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

/// \file Tile.cs This is Shared with the Level Editor
namespace Frostbyte
{
    internal class Tile : TileHelper<Tile>
    {
        internal static readonly int TileSize = 64;

        internal TileTypes Type { get; set; }

        internal bool Traversable { get; set; }

        internal string InstanceName { get; set; }

        internal FloorTypes FloorType { get; set; }

        internal Element Theme { get; set; }

        internal Orientations Orientation { get; set; }

        internal Index2D GridCell { get; set; }

        internal Tile()
        {
            Traversable = true;
            FloorType = FloorTypes.DEFAULT;
            Type = TileTypes.DEFAULT;
            InstanceName = null;
            Theme = Element.DEFAULT;
        }

        public XElement ToXML()
        {
            XElement e = new XElement("Tile");
            e.SetAttributeValue("Type", Type);
            e.SetAttributeValue("InstanceName", InstanceName);
            e.SetAttributeValue("Collision", Traversable);
            e.SetAttributeValue("Theme", Theme);
            e.SetAttributeValue("Orientation", Orientation);
            e.SetAttributeValue("GridCell", GridCell);
            return e;
        }

        public static Tile Parse(XElement elem)
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
                        t.GridCell = Index2D.Parse(attr.Value);
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


