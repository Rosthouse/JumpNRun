using System;
using System.Collections.Generic;
using AudioLib;
using JumpNRunShared.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using SimplePhysicsAndCollision.Interfaces;
using TimeStamp = JumpNRunShared.TimeTravel.TimeStamp;
using TimeStampList = JumpNRunShared.TimeTravel.TimeStampList;

namespace JumpNRunShared.WorldObjects
{
    public abstract class MovingObject : WorldObject, IPhysicsObject 
    {
        #region Members


        //Flags
        protected bool isActive;
        private bool createTimeStamp;

        //Position
        protected Vector3 prevPosition;


        //Movement
        protected Vector3 movementVector;
        protected Vector3 previousMovement;

        protected float acceleration;
        protected float maxSpeed;

        //Input
        protected Vector2 inputVector;
        private Vector2 previousInput;
        
        //Timestamps
        private TimeStampList timeStampList;

        #endregion - Members

        #region Constructors


        public MovingObject(string textureAsset, Vector2 position, float acceleration, float maxSpeed)
            : base(textureAsset, position)
        {
            this.acceleration = acceleration;
            this.maxSpeed = maxSpeed;


            this.timeStampList = new TimeStampList();
            createTimeStamp = true;

            soundEffectList = new SoundEffectList();
            movementVector = Vector3.Zero;
            previousMovement = movementVector;



            inputVector = Vector2.Zero;
            previousInput = inputVector; 
        }

        #endregion - Constructors

        #region Accessors

        #region Position

        public Vector2 PrevPosition
        {
            get { return new Vector2(prevPosition.X, prevPosition.Y); }
        }

        #endregion Position

        #region Movement

        public Vector2 MovementVector
        {
            get { return new Vector2(movementVector.X, movementVector.Y); }
            set { this.movementVector = new Vector3(value.X, value.Y, movementVector.Z); }
        }

        public bool IsMoving
        {
            get
            {
                if(prevPosition == this.TimePosition)
                {
                    return false;
                }
                return true;
            }
        }

        public float Acceleration
        {
            get { return acceleration; }
            set { this.acceleration = value; }
        }

        public float MaxSpeed
        {
            get { return maxSpeed; }
            set { this.maxSpeed = maxSpeed; }
        }


        #endregion Movement

        #region Input
        //Input
        public Vector2 InputVector
        {
            get { return inputVector; }
        }

        public Vector2 PreviousPosition
        {
            get { return new Vector2(prevPosition.X, prevPosition.Y); }
            set { prevPosition = new Vector3(value, position.Z); }
        }

        #endregion Input

        #region Timestamps
        public TimeStampList TimeStamps
        {
            get { return timeStampList; }
            set { this.timeStampList = value; }
        }

        #endregion TimeStamps

        

        #endregion - Accessors

        #region Object Logic

        public override void Update(GameTime gameTime)
        {
            //Todo: Silly thing here, the movement Vector shouldn't be touched by the object anymore
            //movementVector = new Vector3(inputVector, gameTime.ElapsedGameTime.Milliseconds);

            if(previousInput != inputVector)
            {
                CreateTimeStamp((float)gameTime.TotalGameTime.TotalMilliseconds, this.PrevPosition);
            }

            this.TimePosition = new Vector3(TimePosition.X, TimePosition.Y,
                                            TimePosition.Z + gameTime.ElapsedGameTime.Milliseconds);
            previousMovement = movementVector;
            previousInput = inputVector;
        }

        private void CreateTimeStamp(float time, Vector2 position)
        {
            Console.WriteLine("Time: " + TimePosition.Z + "; Position: {"+ TimePosition.X + "/" +TimePosition.Y  + "}; Type: " + this.GetType());
            timeStampList.Push(new TimeStamp(this.TimePosition));
            createTimeStamp = false;
        }


        public void CreateTimeStamp()
        {
            createTimeStamp = true;
        }

        public void CreateTimeStamp(object sender, PausEventArgs e)
        {
            CreateTimeStamp((float)e.time, this.Position);
        }

        public void ClearTimeStamps(object sender, PausEventArgs e)
        {
            timeStampList = new TimeStampList();
            //TODO: Find a proper way to tell the object to make a timestamp
            CreateTimeStamp((float)e.time, this.Position);
        }

        #endregion - Object Logic
    }
}