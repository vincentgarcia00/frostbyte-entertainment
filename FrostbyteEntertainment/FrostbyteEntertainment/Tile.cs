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


    /// <summary>
    /// Wraper class for our dictionary that allows us to most efficiently obtain data from the dictionary
    /// Shared with The Level Editor
    /// </summary>
    public class TileList<T>
    {
        /// <summary>
        /// Dict of the form [y,x]=Tile
        /// </summary>
        List< List<T> > mTiles = new List< List<T> >();

        Vector2? cache_key = null;
        T cache_value;

        internal TileList()
        {

        }

        internal void Add(int x, int y, T t)
        {
            while(mTiles.Count < y){
				mTiles.Add(new List<T>());
			}
			foreach(var row in mTiles)
			while(row.Count < x){
				row.Add( null /*new default(T)*/);
			}
			
			mTiles[y][x] = t;
        }

        internal bool TryGetValue(int x, int y, out T value)
        {
            if (cache_key.HasValue && cache_key.Value.X == x && cache_key.Value.Y == y)
            {
                value = cache_value;
                return true;
            }
            
			if (mTiles[y][x] != null)
            {
                value = mTiles[y][x];
                cache_key = new Vector2(x, y);
                cache_value = value;
                return true;
            }

            value = null;//default(T);
            return false;
        }

        internal void Clear()
        {
            mTiles.Clear();
        }

        public List<int, T> this[int i]
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

        public List<int, List<int, T> > CopyValue()
        {
            return new List<int, List<int, T> >(mTiles);
        }
    }
}
