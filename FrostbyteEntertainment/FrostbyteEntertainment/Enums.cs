using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Frostbyte
{
    public enum Orientations
    {
        Down=0,
        Left,
        Up,
        Right,
        Up_Left,
        Up_Right,
        Down_Left, 
        Down_Right
    }
    
    public enum TileTypes
    {
        DEFAULT = -1,
        Wall = 0,
        Bottom,
        Corner,
        Floor,
        Lava,
        Water
    }
    public enum FloorTypes
    {
        DEFAULT=-1,
        Themed=0,
        Water,
        Lava
    }
    /// <summary>
    /// Also themes
    /// </summary>
    public enum Element
    {
        DEFAULT = -1,
        Earth = 0,
        Lightning,
        Water,
        Fire
    }
}
