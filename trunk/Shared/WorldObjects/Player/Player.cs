using System;
using JumpNRunShared.Events;
using JumpNRunShared.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using SimplePhysicsAndCollision.Collision;
using SimplePhysicsAndCollision.Interfaces;

namespace JumpNRunShared.WorldObjects.Player
{
    public delegate void OutOfLivesEventHandler(object sender, EventArgs e);

    public class Player : AnimatedObject, IActionEmitter
    {
        #region Events

        public event OutOfLivesEventHandler OutOfLives;
        public event OnActionEvent OnAction;

        #endregion Events

        #region Members

        //Flags
        private bool isOnGround;
        private bool isJumping;

        //Jump
        public const int MaxJumpTime = 200;
        private int jumpTimer;

        //Lifes
        public int lifes;

        #endregion - Members

        #region Constructors


        public Player(string textureAsset, Vector2 position, float acceleration, float maxSpeed, int lifes)
            : base(textureAsset, position, acceleration, maxSpeed)
        {
            isJumping = false;
            isOnGround = true;

            jumpTimer = 0;
            this.lifes = lifes;
        }
        
        #endregion - Constructors

        #region Accessors

        //TODO: Do I need Accessors here?

        #endregion Accessors

        #region Object Logic

        private KeyboardState lastKeyBoardState = Keyboard.GetState();

        public override void Load(Microsoft.Xna.Framework.Content.ContentManager contentManager)
        {
            soundEffectList.Add("jump", contentManager.Load<SoundEffect>(@"Audio\Effects\smb_jumpsmall"));

            base.Load(contentManager);
        }

        public override void Update(GameTime gameTime)
        {
            currentAnimation = 0;
            KeyboardState keyboardState = Keyboard.GetState();
            inputVector = Vector2.Zero;
           

            if(PrevPosition.Y == Position.Y)
            {
                isOnGround = true;
            }
            else
            {
                isOnGround = false;
            }

            if(keyboardState.IsKeyDown(Keys.E))
            {
                FireActionEvent();
            }

            if (keyboardState.IsKeyDown(Keys.A))
            {
                inputVector.X = -1; 
                currentAnimation = 1;
                looksRight = false;
            }
            if (keyboardState.IsKeyDown(Keys.D))
            {
                inputVector.X = 1;
                currentAnimation = 1;
                looksRight = true;
            }
            if (keyboardState.IsKeyDown(Keys.W) /*&& isOnGround*/)
            {
                MovementVector = new Vector2(MovementVector.X, -maxSpeed);
                isJumping = true;
                soundEffectList.Play("jump");
            }
            if(keyboardState.IsKeyUp(Keys.W) && !isOnGround)
            {
                AbortJump();
            }

            inputVector += DoJump(gameTime);

            Animations[CurrentAnimation].Update(gameTime, looksRight);

            base.Update(gameTime);
        }

        public override void OnCollision(WorldObject collisionObject)
        {
            throw new NotImplementedException();
        }

        private Vector2 DoJump(GameTime gameTime)
        {
            Vector2 jumpVector = Vector2.Zero;

            if (!isOnGround)
            {
                currentAnimation = 2;
            }

            if(isJumping)
            {

                jumpTimer += gameTime.ElapsedGameTime.Milliseconds;
                if(jumpTimer >= MaxJumpTime)
                {
                    isJumping = false;
                    jumpTimer = 0;
                } else
                {
                    //Todo: This needs to be rewriten, since the movement will be transfered to the physics library
                    jumpVector.Y -= Math.Abs(3*10);
                }
            }

            return jumpVector;
        }

        #endregion - Object Logic

        #region Helper Methods

        public void AbortJump()
        {
            jumpTimer = MaxJumpTime;
        }

        public override void killObject(AnimatedObject killer)
        {
            lifes--;
            base.killObject(killer);
            if(lifes < 0)
            {
                if(OutOfLives != null)
                {
                    OutOfLives(this, new EventArgs());
                }
            }
        }

        public override void  createAnimations()
        {
            AddAnimation(new Animation("idle", new Point(5, 0),
                                       new Vector2(((Texture.Width / 6)), Texture.Height),
                                       new Point(0, 0), new Point(0, 0)));
            AddAnimation(new Animation("walk", new Point(5, 0), new Vector2(((Texture.Width / 6)), Texture.Height),
                                       new Point(0, 0), new Point(4, 0)));
            AddAnimation(new Animation("jump", new Point(5, 0),
                                       new Vector2(((Texture.Width / 6)), Texture.Height),
                                       new Point(4, 0), new Point(4, 0)));
        }

        #endregion - Helper Methods

        #region EventHandeling

        public void FireActionEvent()
        {
            ActionEventArgs eventArgs = new ActionEventArgs(this, new FRect());

            OnAction(this, eventArgs);
        }

        #endregion EventHandeling
    }
}