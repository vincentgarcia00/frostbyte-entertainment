using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Frostbyte
{

    internal class Tile
    {
        internal static readonly int TileSize = 200;

        internal bool Traversable { get; set; }

        internal TileTypes Type { get; set; }

        internal string InstanceName { get; set; }

        internal FloorTypes FloorType { get; set; }

        internal Element Theme { get; set; }

        internal Orientations Orientation { get; set; }

        internal Tile()
        {
            Traversable = true;
        }

        
    }

    /// \file This is Shared with the Level Editor


    /// <summary>
    /// Wraper class for our dictionary that allows us to most efficiently obtain data from the dictionary
    /// Shared with The Level Editor
    /// </summary>
    public class TileDictionary<T>
    {
        /// <summary>
        /// Dict of the form [y,x]=Tile
        /// </summary>
        Dictionary<int, Dictionary<int, T>> mDict = new Dictionary<int,Dictionary<int,T>>();

        Vector2? cache_key = null;
        T cache_value;

        internal TileDictionary()
        {

        }

        internal void Add(int x, int y, T t)
        {
            Dictionary<int, T> elem;
            if (mDict.TryGetValue(y, out elem))
            {
                elem[x] = t;
            }
            else
            {
                elem = new Dictionary<int, T>();
                elem[x] = t;
            }
        }

        internal bool TryGetValue(int x, int y, out T value)
        {
            if (cache_key.HasValue && cache_key.Value.X == x && cache_key.Value.Y == y)
            {
                value = cache_value;
                return true;
            }
            if (mDict.ContainsKey(x) && mDict[x].ContainsKey(y))
            {
                value = mDict[x][y];
                cache_key = new Vector2(x, y);
                cache_value = value;
                return true;
            }

            value = default(T);
            return false;
        }

        internal void Clear()
        {
            mDict.Clear();
        }

        public Dictionary<int, T> this[int i]
        {
            get
            {
                return mDict[i];
            }
            set
            {
                mDict[i] = value;
            }
        }
    }
}
