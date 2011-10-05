using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Frostbyte
{
    /// <summary>
    /// Wraper class for our dictionary that allows us to most efficiently obtain data from the dictionary
    /// Shared with The Level Editor
    /// </summary>
    public partial class TileList<T> where T : new()
    {
        /// <summary>
        /// Dict of the form [y,x]=Tile
        /// </summary>
        List<List<T>> mTiles = new List<List<T>>();

        /// <summary>
        /// All data that the level file contains in order from the file (for saving again)
        /// </summary>
        List<LevelObject> LevelParts = new List<LevelObject>();

        public Tuple<List<LevelObject>, List<List<T>>> Data
        {
            get
            {
                return new Tuple<List<LevelObject>, List<List<T>>>(LevelParts, mTiles);
            }
        }

        Index2D cache_key = new Index2D(-1, -1);
        T cache_value;

        internal TileList()
        {

        }

        #region Adds
        /// <summary>
        /// Adds a Tile to the tile map but not list of objects
        /// </summary>
        /// <param name="t">The tile to add</param>
        /// <param name="x">The X grid location</param>
        /// <param name="y">The Y gird location</param>
        /// <returns></returns>
        internal bool Add(T t, int x, int y)
        {
            //fills grid
            while (mTiles.Count < y + 1)
            {
                mTiles.Add(new List<T>());
            }
            foreach (var row in mTiles)
                while (row.Count < x + 1)
                {
                    row.Add(new T());
                }

            mTiles[y][x] = t;
            return true;
        }

        /// <summary>
        /// Adds the Room to ListObjects and adds all relevant cells to the grid
        /// </summary>
        /// <param name="r">Room to add</param>
        /// <returns>Success</returns>
        internal bool Add(Room r)
        {
            LevelParts.Add(r);
            //add tiles to the list
            return true;
        }

        /// <summary>
        /// Adds the Walls to ListObjects and adds all relevant cells to the grid
        /// </summary>
        /// <param name="r">Walls to add</param>
        /// <returns>Success</returns>
        internal bool Add(BorderWalls r)
        {
            LevelParts.Add(r);
            //add tiles to the list
            return true;
        }

        /// <summary>
        /// Adds the Wall to ListObjects and adds all relevant cells to the grid
        /// </summary>
        /// <param name="r">Wall to add</param>
        /// <returns>Success</returns>
        internal bool Add(Wall r)
        {
            LevelParts.Add(r);
            //add tiles to the list
            return true;
        }

        /// <summary>
        /// Adds the Floor to ListObjects and adds all relevant cells to the grid
        /// </summary>
        /// <param name="r">Floor to add</param>
        /// <returns>Success</returns>
        internal bool Add(Floor r)
        {
            LevelParts.Add(r);
            //add tiles to the list
            return true;
        }

        /// <summary>
        /// Adds a FrostByte.Tile to the grid and to ObjectList
        /// </summary>
        /// <param name="r">Room to add</param>
        /// <returns>Success</returns>
        internal bool Add(T r)
        {
            if (r as LevelObject != null)
            {
                //determine what it is
                if (r as Tile != null)
                {
                    Tile t = r as Tile;
                    mTiles[t.GridCell.Y][t.GridCell.X] = r;
                    LevelParts.Add(t);
                    return true;
                }
            }
            //it's a level editor tile
            return false;
        }

        /// <summary>
        /// Determines what an object is and adds it to the level
        /// </summary>
        /// <param name="obj">Object we want to split and add</param>
        internal List<T> Add(LevelPart obj)
        {
            throw new NotImplementedException();
        }
        #endregion Adds

        #region RemoveItems
        public bool Remove(T elem)
        {
            Tile t = elem as Tile;
            if (t != null)
            {
                mTiles[t.GridCell.Y][t.GridCell.X] = new T();
                LevelParts.Remove(t);
                return true;
            }
            return false;
        }

        public bool Remove(T elem, int x, int y)
        {
            mTiles[y][x] = new T();
            return true;
        }
        #endregion RemoveItems

        internal bool TryGetValue(int x, int y, out T value)
        {
            if (cache_key.X == x && cache_key.Y == y)
            {
                value = cache_value;
                return true;
            }

            if (mTiles[y][x] != null)
            {
                value = mTiles[y][x];
                cache_key = new Index2D(x, y);
                cache_value = value;
                return true;
            }

            value = new T();
            return false;
        }

        internal void Clear()
        {
            mTiles.Clear();
        }

        public List<T> this[int i]
        {
            get
            {
                return mTiles[i];
            }
            set
            {
                mTiles[i] = value;
            }
        }

        public List<List<T>> CopyValue()
        {
            return new List<List<T>>(mTiles);
        }

        /// <summary>
        /// Saves the Document to the given tile
        /// </summary>
        /// <param name="filename">SaveFile name</param>
        /// <param name="doc">Document to save</param>
        public void Save(string filename, XDocument doc)
        {
            doc.Save(filename);
        }

        /// <summary>
        /// Generates the XDocument with all our data.
        /// </summary>
        /// <returns>XDocument form of the level</returns>
        public XDocument Serialize()
        {
            XDocument doc = new XDocument(new XElement("Level"));

            return doc;
        }

        /// <summary>
        /// Loads File
        /// </summary>
        /// <param name="filename">Name of the file to load</param>
        /// <returns>The object created</returns>
        public static TileList<T> Load(string filename)
        {
            return Parse(XDocument.Load(filename));
        }

        /// <summary>
        /// parses an xdocument and re
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static TileList<T> Parse(XDocument doc)
        {
            //load data
            TileList<T> tl = new TileList<T>();


            return tl;
        }
            }
}
