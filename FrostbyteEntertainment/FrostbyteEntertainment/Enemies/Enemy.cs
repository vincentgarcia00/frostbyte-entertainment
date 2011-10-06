using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;


namespace Frostbyte.Enemies
{
    internal abstract class Enemy : Sprite
    {
        #region Variables

        //State
        public float health;
        protected float speed;
        protected Vector2 direction;
        
        //Elemental Properties
        protected enum elementTypes { Earth, Lightning, Water, Fire, Neutral };
        protected elementTypes elementType;

        //Movement
        protected enum movementTypes { Charge, PulseCharge, Ram, StealthCharge, StealthCamp, StealthRetreat, Retreat, TeaseRetreat, Swap, Freeze };
        protected movementTypes currentMovementType = 0;
        protected TimeSpan movementStartTime;
        bool isRamming, isCharging, isStealth, isFrozen, isAttacking  = false;

        #endregion Variables

        Enemy(string name, Actor actor) : base(name, actor) 
        {
            
        }

        public void update()
        {
            updateMovement();
            checkBackgroundCollisions();
            updateAttack();
        }

        private void checkBackgroundCollisions()
        {
            throw new NotImplementedException();
        }
        protected abstract void updateMovement();
        protected abstract void updateAttack();

        #region AI Movements
        //These are only to update position of enemy
        
        /// <summary>
        /// Update enemy position directly toward target for given duration
        /// </summary>
        private void charge(Vector2 P1Coord, Vector2 P2Coord, float aggroDistance, float speedMultiplier)
        {

        }

        /// <summary>
        /// Update enemy position directly toward target with variation of speed (sinusoidal) for given duration
        /// </summary>
        private void pulseCharge(Vector2 P1Coord, Vector2 P2Coord, float aggroDistance, float speedMultiplier)
        {

        }

        /// <summary>
        /// Charge but do not update direction for length of charge
        /// </summary>
        private void ram(Vector2 P1Coord, Vector2 P2Coord, TimeSpan duration, float aggroDistance, float speedMultiplier)
        {

        }

        /// <summary>
        /// Hide and follow player until certain distance from player
        /// </summary>
        private void stealthCharge(Vector2 P1Coord, Vector2 P2Coord, TimeSpan duration, float aggroDistance, float speedMultiplier)
        {

        }

        /// <summary>
        /// Be Invisible and still until certain distance from player
        /// </summary>
        private void stealthCamp(Vector2 P1Coord, Vector2 P2Coord, float aggroDistance)
        {

        }

        /// <summary>
        /// Be Invisible and move away until x seconds have passed
        /// </summary>
        private void stealthRetreat(Vector2 P1Coord, Vector2 P2Coord, TimeSpan duration, float safeDistance, float speedMultiplier)
        {

        }

        /// <summary>
        /// Move away until x seconds have passed or you are y distance away
        /// </summary>
        private void retreat(Vector2 P1Coord, Vector2 P2Coord, TimeSpan duration, float safeDistance, float speedMultiplier)
        {

        }

        /// <summary>
        /// Move away when x distance from target until y seconds have passed or z distance from player
        /// </summary>
        private void teaseRetreat(Vector2 P1Coord, Vector2 P2Coord, TimeSpan duration, float aggroDistance, float safeDistance, float speedMultiplier)
        {

        }

        /// <summary>
        /// Stop moving for x seconds
        /// </summary>
        private void freeze(TimeSpan duration)
        {

        }

        /// <summary>
        /// Switch Position with the target
        /// </summary>
        private void swap(Vector2 P1Coord, Vector2 P2Coord, float aggroDistance)
        {

        }

        #endregion AI Movements

        //todo:
        //fill in AI Movement Functions
        //create projectile class (projectiles modify health of enemies/players) 
        //complete checkBackgroundCollisions
    }
}
