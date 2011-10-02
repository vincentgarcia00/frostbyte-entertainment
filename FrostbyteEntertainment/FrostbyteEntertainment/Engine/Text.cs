using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frostbyte.Engine
{
    class Text : Sprite
    {
        SpriteFont font;
        internal string Content { get; set; }
        internal Color DisplayColor { get; set; }

        internal Text(string name, string fontname, string content) :
            this(name, This.Game.Content.Load<SpriteFont>(fontname), content)
        {
        }

        internal Text(string name, SpriteFont font, string content) :
            base(name, new Actor(new DummyAnimation(name)))
        {
            this.font = font;
            Content = content;
            Vector2 size = font.MeasureString(content);
            mActor.Animations[mActor.CurrentAnimation].Frames[mActor.Frame].Width = (int)size.X;
            mActor.Animations[mActor.CurrentAnimation].Frames[mActor.Frame].Height = (int)size.Y;
        }

        internal override void Draw(GameTime gameTime)
        {
            This.Game.spriteBatch.DrawString(font, Content, Pos, DisplayColor);
        }
    }
}
