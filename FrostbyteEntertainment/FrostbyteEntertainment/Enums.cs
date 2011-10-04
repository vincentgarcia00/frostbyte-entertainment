using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/// \file Enums.cs This is Shared with the Level Editor
namespace Frostbyte
{
    /// <summary>
    /// Orientations for any object on screen
    /// </summary>
    public enum Orientations
    {
        /// <summary>
        /// V
        /// </summary>
        Down = 0,
        /// <summary>
        /// <-
        /// </summary>
        Left,
        /// <summary>
        /// ^
        /// </summary>
        Up,
        /// <summary>
        /// ->
        /// </summary>
        Right,
        /// <summary>
        /// \
        /// </summary>
        Up_Left,
        /// <summary>
        /// /
        /// </summary>
        Up_Right,
        /// <summary>
        /// _/
        /// </summary>
        Down_Left,
        /// <summary>
        /// \_
        /// </summary>
        Down_Right
    }

    /// <summary>
    /// Possible tiles for the level
    /// </summary>
    public enum TileTypes
    {
        DEFAULT = -1,
        /// <summary>
        /// Foor Top or Side wall tiles (determined by orientation)
        /// </summary>
        Wall = 0,
        /// <summary>
        /// Bottom wall tiles 
        /// </summary>
        Bottom,
        /// <summary>
        /// A corner (may be tl, tr, bl, br) \todo mabe distinguish between types of corners (determined by orientation)
        /// </summary>
        Corner,
        /// <summary>
        /// This is for floor tiles
        /// </summary>
        Floor,
        /// <summary>
        /// Lava tile
        /// </summary>
        Lava,
        /// <summary>
        /// Water tile
        /// </summary>
        Water,
        /// <summary>
        /// For Room class (needed for editor)
        /// </summary>
        Room,
    }

    /// <summary>
    /// Whether or not the floor is Themed or not
    /// </summary>
    public enum FloorTypes
    {
        DEFAULT = -1,
        Themed = 0,
        Water,
        Lava
    }

    /// <summary>
    /// Elements / Themes
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
