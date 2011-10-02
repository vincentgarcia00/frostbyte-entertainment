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

        internal Tile()
        {
            Traversable = true;
        }
    }

    internal class TileDictionary
    {
        Dictionary<int, Dictionary<int, Tile>> mDict = new Dictionary<int,Dictionary<int,Tile>>();

        Vector2? cache_key = null;
        Tile cache_value;

        internal TileDictionary()
        {

        }

        internal void Add(int x, int y, Tile t)
        {
            if (!mDict.ContainsKey(x))
            {
                mDict.Add(x, new Dictionary<int, Tile>());
            }

            mDict[x].Add(y, t);
        }

        internal bool TryGetValue(int x, int y, out Tile value)
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

            value = new Tile();
            return false;
        }
    }
}
