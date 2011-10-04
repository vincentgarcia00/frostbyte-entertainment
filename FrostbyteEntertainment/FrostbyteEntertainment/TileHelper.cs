using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

/// \file TileHelper.cs This is Shared with the Level Editor

namespace Frostbyte
{
    ///// <summary>
    ///// Base Class for Tileable objects
    ///// </summary>
    ///// <typeparam name="T">Will be The corresponding tile for Game/Editor</typeparam>
    //public interface TileHelper<T>
    //{
    //    XElement ToXML();
    //    //void SetParse(XElement elem);/// \todo make sure all things implementing the interface have one of these but static
    //}

    public partial class Index2D
    {
        public Index2D(int x, int y)
        {
            X = x;
            Y = y;
        }

#if LEVELEDITOR
        //Stuff for Level editor goes here
#else
        //stuff for the game goes here
#endif

        public int X { get; set; }
        public int Y { get; set; }

        public static Index2D Parse(string s)
        {
            string[] ss = s.Split(new char[] { ',' });
            int x = int.Parse(ss[0]);
            int y = int.Parse(ss[1]);
            return new Index2D(x, y);
        }
        public override string ToString()
        {
            return String.Format("{0},{1}", X, Y);
        }
    }

    public class Wall : LevelObject
    {
        public Index2D StartCell
        {
            get
            {
                return GridCell;
            }
            set
            {
                GridCell = value;
            }
        }

        public Index2D EndCell { get; set; }

        public Wall(Index2D start, Index2D end, TileTypes t, Element theme, bool move = false)
        {
            StartCell = start;
            EndCell = end;
            Type = t;
            Theme = theme;
            Traversable = move;
        }

        public Wall(Index2D start)
        {
            StartCell = start;
        }

        public override XElement ToXML()
        {
            XElement e = new XElement("Wall");
            e.SetAttributeValue("Type", Type);
            e.SetAttributeValue("StartCell", StartCell);
            e.SetAttributeValue("EndCell", EndCell);
            e.SetAttributeValue("Collision", Traversable);
            e.SetAttributeValue("Theme", Theme);
            return e;
        }

        public static Wall Parse(XElement elem)
        {
#if DEBUG
            try
            {
#endif
                return new Wall(
                    Index2D.Parse(elem.Attribute("StartCell").Value),
                    Index2D.Parse(elem.Attribute("EndCell").Value),
                    (TileTypes)Enum.Parse(typeof(TileTypes), elem.Attribute("Type").Value),
                    (Element)Enum.Parse(typeof(Element), elem.Attribute("Theme").Value),
                    bool.Parse(elem.Attribute("Collision").Value)
                    );
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

    public class Floor : LevelObject
    {
        public Index2D StartCell
        {
            get
            {
                return GridCell;
            }
            set
            {
                GridCell = value;
            }
        }

        public Index2D EndCell { get; set; }

        public Floor(Index2D start, Index2D end, TileTypes t, Element theme, bool move = true, FloorTypes f = FloorTypes.Themed)
        {
            StartCell = start;
            EndCell = end;
            Type = t;
            Theme = theme;
            Traversable = move;
            FloorType = f;
        }

        public Floor(Index2D start)
        {
            StartCell = start;
        }

        public override XElement ToXML()
        {
            XElement e = new XElement("Floor");
            e.SetAttributeValue("Type", Type);
            e.SetAttributeValue("StartCell", StartCell);
            e.SetAttributeValue("EndCell", EndCell);
            e.SetAttributeValue("Collision", Traversable);
            e.SetAttributeValue("Theme", Theme);
            return e;
        }

        public static Floor Parse(XElement elem)
        {
#if DEBUG
            try
            {
#endif
                return new Floor(
                    Index2D.Parse(elem.Attribute("StartCell").Value),
                    Index2D.Parse(elem.Attribute("EndCell").Value),
                    (TileTypes)Enum.Parse(typeof(TileTypes), elem.Attribute("Type").Value),
                    (Element)Enum.Parse(typeof(Element), elem.Attribute("Theme").Value),
                    bool.Parse(elem.Attribute("Collision").Value)
                    );
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

    public class BorderWalls : LevelObject
    {
        public Index2D StartCell
        {
            get
            {
                return GridCell;
            }
            set
            {
                GridCell = value;
            }
        }

        public Index2D EndCell { get; set; }

        public BorderWalls(Index2D start, Index2D end, TileTypes t, Element theme, bool move = true)
        {
            StartCell = start;
            EndCell = end;
            Type = t;
            Theme = theme;
            Traversable = move;
        }

        public BorderWalls(Index2D start)
        {
            StartCell = start;
        }

        public override XElement ToXML()
        {
            XElement e = new XElement("Walls");
            e.SetAttributeValue("Type", Type);
            e.SetAttributeValue("StartCell", StartCell);
            e.SetAttributeValue("EndCell", EndCell);
            e.SetAttributeValue("Collision", Traversable);
            e.SetAttributeValue("Theme", Theme);
            return e;
        }

        public static BorderWalls Parse(XElement elem)
        {
#if DEBUG
            try
            {
#endif
                return new BorderWalls(
                    Index2D.Parse(elem.Attribute("StartCell").Value),
                    Index2D.Parse(elem.Attribute("EndCell").Value),
                    (TileTypes)Enum.Parse(typeof(TileTypes), elem.Attribute("Type").Value),
                    (Element)Enum.Parse(typeof(Element), elem.Attribute("Theme").Value),
                    bool.Parse(elem.Attribute("Collision").Value)
                    );
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

    public class Room : LevelObject
    {
        public Index2D StartCell
        {
            get
            {
                return GridCell;
            }
            set
            {
                GridCell = value;
            }
        }

        public Index2D EndCell { get; set; }

        public Room(Index2D start, Index2D end, Element theme, FloorTypes f = FloorTypes.Themed, bool move = true)
        {
            StartCell = start;
            EndCell = end;
            FloorType = f;
            Theme = theme;
            Traversable = move;
        }

        public Room(Index2D start)
        {
            StartCell = start;
        }

        public override XElement ToXML()
        {
            XElement e = new XElement("Walls");
            e.SetAttributeValue("Type", Type);
            e.SetAttributeValue("StartCell", StartCell);
            e.SetAttributeValue("EndCell", EndCell);
            e.SetAttributeValue("Collision", Traversable);
            e.SetAttributeValue("Theme", Theme);
            return e;
        }

        public static Room Parse(XElement elem)
        {
#if DEBUG
            try
            {
#endif
                if (elem.Attribute("FloorType") != null && elem.Attribute("Collision") != null)
                    return new Room(
                        Index2D.Parse(elem.Attribute("StartCell").Value),
                        Index2D.Parse(elem.Attribute("EndCell").Value),
                        //(TileTypes)Enum.Parse(typeof(TileTypes), elem.Attribute("Type").Value),
                        (Element)Enum.Parse(typeof(Element), elem.Attribute("Theme").Value),
                        (FloorTypes)Enum.Parse(typeof(FloorTypes), elem.Attribute("FloorType").Value),
                        bool.Parse(elem.Attribute("Collision").Value)
                    );
                else if (elem.Attribute("FloorType") != null)
                    return new Room(
                        Index2D.Parse(elem.Attribute("StartCell").Value),
                        Index2D.Parse(elem.Attribute("EndCell").Value),
                        //(TileTypes)Enum.Parse(typeof(TileTypes), elem.Attribute("Type").Value),
                        (Element)Enum.Parse(typeof(Element), elem.Attribute("Theme").Value),
                        (FloorTypes)Enum.Parse(typeof(FloorTypes), elem.Attribute("FloorType").Value)
                    );
                else
                    return new Room(
                        Index2D.Parse(elem.Attribute("StartCell").Value),
                        Index2D.Parse(elem.Attribute("EndCell").Value),
                        //(TileTypes)Enum.Parse(typeof(TileTypes), elem.Attribute("Type").Value),
                        (Element)Enum.Parse(typeof(Element), elem.Attribute("Theme").Value)
                    );

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

    public class LevelObject
    {
        public TileTypes Type { get; set; }

        public Element Theme { get; set; }

        public bool Traversable { get; set; }

        public Orientations Orientation { get; set; }

        public FloorTypes FloorType { get; set; }

        public Index2D GridCell { get; set; }

        public string InstanceName { get; set; }

        public virtual XElement ToXML()
        {
            return new XElement("Level");
        }
    }
}
