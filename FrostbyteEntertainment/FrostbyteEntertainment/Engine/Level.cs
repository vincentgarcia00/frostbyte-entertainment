using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frostbyte
{
    class Level
    {

        #region Constructor
        public Level()
        {
            LoadBehavior = () => { };
            UpdateBehavior = () => { };
            EndBehavior = () => { };
        }
        public Level(string n, Behavior loadBehavior, Behavior updateBehavior, Behavior endBehavior, Condition winCondition)
        {
            mName = n;
            LoadBehavior = loadBehavior;
            UpdateBehavior = updateBehavior;
            EndBehavior = endBehavior;
            WinCondition = winCondition;
        }
        #endregion Constructor

        #region Behaviors
        /// <summary>
        /// Level's load action
        /// </summary>
        public Behavior LoadBehavior { get; set; }
        /// <summary>
        /// Level's update Behavior
        /// </summary>
        public Behavior UpdateBehavior { get; set; }
        /// <summary>
        /// Level's End Behavior
        /// </summary>
        public Behavior EndBehavior { get; set; }
        #endregion Behaviors

        #region Properties
        /// <summary>
        /// Get's level's name
        /// </summary>
        public string Name { get { return mName; } }
        /// <summary>
        /// Gets and Sets Level's current Background
        /// </summary>
        public Background Background { get; set; }

        /// <summary>
        /// A Condition to check if the level has been won
        /// </summary>
        public Condition WinCondition { get; set; }

        /// <summary>
        /// Tells whether the current level is loaded or not
        /// </summary>
        public bool Loaded { get; set; }
        #endregion Properties

        #region Variables
        /// <summary>
        /// Level's name
        /// </summary>
        string mName = "";

        /// <summary>
        /// Vector of all WorldObjects drawn on the level.
        /// </summary>
        internal List<WorldObject> mSprites = new List<WorldObject>();

        /// <summary>
        /// This level's actors. 
        /// </summary>
        protected Dictionary<string, Actor> mActors = new Dictionary<string, Actor>();

        /// <summary>
        /// This level's Animations
        /// </summary>
        protected Dictionary<string, Animation> mAnims = new Dictionary<string, Animation>();

        protected List<WorldObject> ToAdd = new List<WorldObject>();
        protected List<WorldObject> ToRemove = new List<WorldObject>();

        public Camera Camera = new Camera();

        #endregion Variables

        
        internal void Load()
        {
            This.Game.AudioManager.Stop();
            mSprites.Clear();
            mActors.Clear();
            mAnims.Clear();
            LoadBehavior();
            Loaded = true;
        }

        internal virtual void Update()
        {
            mSprites.Sort();
            if (Loaded)
            {
                UpdateBehavior();
                if (WinCondition())
                {
                    Unload();
                }
                foreach (Sprite sp in mSprites)
                {
                    if (!WinCondition())
                        sp.UpdateBehavior();
                    else
                    {
                        Unload();
                        return;
                    }
                }
                foreach (var item in ToRemove)
                {
                    mSprites.Remove(item);
                }
                ToRemove.Clear();
                foreach (var item in ToAdd)
                {
                    mSprites.Add(item);
                }
                ToAdd.Clear();
                Collision.update();
                foreach (Sprite sp in mSprites)
                {
                    sp.DoCollisions();
                }
            }
            else
            {
                /// \todo Show load screen
            }
        }

        internal void Unload()
        {
            This.Game.AudioManager.Stop();
            mSprites.Clear();
            mActors.Clear();
            mAnims.Clear();
            EndBehavior();
        }

        #region Methods

        #region Draw
        internal void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            This.Game.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend,
    null, null, null, null,
    Camera.GetTransformation(This.Game.GraphicsDevice));

            /** Draw Background */
            Background.Draw(gameTime);

            List<WorldObject> staticSprites = new List<WorldObject>();

            #region Draw Sprites

            foreach (var sprite in mSprites)
            {
                if (!sprite.Static)
                {
                    sprite.Draw(gameTime);
                }
                else
                {
                    staticSprites.Add(sprite);
                }
            }

            This.Game.spriteBatch.End();
            #endregion

            #region Draw Static Sprites
            if (staticSprites.Count > 0)
            {
                This.Game.spriteBatch.Begin();

                foreach (var sprite in staticSprites)
                {
                    sprite.Draw(gameTime);
                }

                This.Game.spriteBatch.End();
            }
            #endregion

            /** Draw Boundary Data */
            Collision.Draw(Camera.GetTransformation(This.Game.GraphicsDevice));
        }

        #endregion Drawing

        #region Management
        public void AddSprite(Sprite sp)
        {
            ToAdd.Add(sp);
        }

        public Sprite GetSprite(string name)
        {
            return (mSprites.Find(delegate(WorldObject s) { return s.Name == name; }) as Sprite);
        }

        /// <summary>
        /// Retrieves all sprites with the specified type.
        /// @todo Get only sprites within a certain distance of a point, for efficiency's sake.
        ///     Possibly could make use of Bruce's collision code.
        /// </summary>
        /// <param name="typename">The type name to select by.</param>
        /// <returns></returns>
        public List<Sprite> GetSpritesByType(string typename)
        {
            return (mSprites.FindAll(
                delegate(WorldObject s) { return s.GetType().Name == typename; }).ConvertAll<Sprite>(
                    delegate(WorldObject s) { return s as Sprite; }));
        }

        public void RemoveSprite(Sprite sp)
        {
            ToRemove.Add(sp);
        }

        public void AddAnimation(Animation anim)
        {
            mAnims[anim.Name] = anim;
        }

        public Animation GetAnimation(string name)
        {
            if (mAnims.ContainsKey(name))
            {
                return mAnims[name];
            }
            else
            {
                throw new AnimationDoesNotExistException(name);
            }
        }

        public void RemoveAnimation(Animation anim)
        {
            if (mAnims.ContainsKey(anim.Name))
            {
                mAnims.Remove(anim.Name);
            }
        }

        public void AddActor(string name, Actor actor)
        {
            mActors[name] = actor;
        }

        public void RemoveActor(string name)
        {
            mActors.Remove(name);
        }
        #endregion Management

        public override string ToString()
        {
            return Name;
        }
        #endregion Methods
    }
}
