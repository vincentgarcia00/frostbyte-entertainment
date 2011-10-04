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
    public class TileList<T>
    {
        /// <summary>
        /// Dict of the form [y,x]=Tile
        /// </summary>
        List< List<T> > mTiles = new List< List<T> >();

        Index2D cache_key = new Index2D(-1,-1);
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
				row.Add(default(T));
			}
			
			mTiles[y][x] = t;
        }

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

            value = default(T);
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
		
		public void Save(string filename, XDocument doc)
		{
			doc.Save(filename);
		}

		public XDocument Compress()
		{
			XDocument doc = new XDocument(new XElement("Level"));

			return doc;
		}

		public List< List<T> > Parse(string filename)
		{
			return Parse(XDocument.Load(filename));
		}

		public List< List<T> > Parse(XDocument doc)
		{
			return new List< List<T> >();
		}
    }
}
