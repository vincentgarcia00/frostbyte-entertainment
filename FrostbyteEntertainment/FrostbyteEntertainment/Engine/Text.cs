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
        public string Content { get; set; }
        public Color DisplayColor { get; set; }

        public Text(string name, string fontname, string content) :
            this(name, This.Game.Content.Load<SpriteFont>(fontname), content)
        {
        }

        public Text(string name, SpriteFont font, string content) :
            base(name, new Actor(new DummyAnimation(name)))
        {
            this.font = font;
            Content = content;
            Vector2 size = font.MeasureString(content);
            mActor.Animations[mActor.CurrentAnimation].Frames[mActor.Frame].Width = (int)size.X;
            mActor.Animations[mActor.CurrentAnimation].Frames[mActor.Frame].Height = (int)size.Y;
        }

        public override void Draw(GameTime gameTime)
        {
            This.Game.spriteBatch.DrawString(font, Content, Pos, DisplayColor);
        }
    }
}
