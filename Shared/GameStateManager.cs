using System;
using System.Collections.Generic;
using JumpNRunShared.Enums;
using JumpNRunShared.Events;
using JumpNRunShared.Interfaces;
using JumpNRunShared.WorldObjects;
using JumpNRunShared.WorldObjects.Enemies;
using JumpNRunShared.WorldObjects.Level;
using JumpNRunShared.WorldObjects.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using SimplePhysicsAndCollision;
using SimplePhysicsAndCollision.Collision;

namespace JumpNRunShared
{
    /// <summary>
    /// This clas holds all the current game objects. This class exists to separate object handeling from game logic.
    /// </summary>
    public class GameStateManager
    {
        private Level level;

        private bool loadContentOnCreation;

        private List<WorldObject> worldObjects;
        private GameState gameState;
        private ContentManager contentManager;
        private MovementManager movementManager;

        /// <summary>
        /// Default constructor, initializes all objects
        /// </summary>
        public GameStateManager(ContentManager contentManager, bool loadContentOnCreation)
        {
            this.level = null;
            this.worldObjects = new List<WorldObject>();
            gameState = Enums.GameState.Playing;

            this.contentManager = contentManager;

            this.loadContentOnCreation = loadContentOnCreation;
        }

        /// <summary>
        /// The current gamestate
        /// </summary>
        public GameState GameState
        {
            get { return gameState; }
            set { gameState = value; }
        }

        /// <summary>
        /// The current level
        /// <remarks>Each object inside the level will be automaticaly added to the collision tree</remarks>
        /// </summary>
        public Level Level
        {
            get { return this.level; }
            set
            {
                this.level = value;

                movementManager.SetLevelSize(level.Size);
                foreach (LevelBlock levelBlock in level.Entities)
                {
                    levelBlock.CollisionItem = movementManager.InsertItem(levelBlock);
                }
            }
        }

        /// <summary>
        /// List of all Movable objects
        /// </summary>
        public List<WorldObject> WorldObjects
        {
            get { return worldObjects; }
        }

        /// <summary>
        /// Adds players. Supports params
        /// </summary>
        /// <param name="movableObjects"></param>
        public void AddPlayers(params MovingObject[] movableObjects)
        {
            foreach (MovingObject o in movableObjects)
            {
                this.worldObjects.Add(o); 
            }
              
        }

        public WorldObject AddExternalObject(WorldObject worldObject)
        {
            AddObject(worldObject);
            return worldObject;
        }

        private void AddObject(WorldObject worldObject)
        {
            if(loadContentOnCreation)
            {
                worldObject.Load(contentManager);
            }

            worldObject.CollisionItem = movementManager.InsertItem(worldObject);
            worldObjects.Add(worldObject);
        }

        public void AdjustPosition(WorldObject levelObject)
        {
            //TODO: Add a check with the Quadtree
            foreach (WorldObject toCheck in worldObjects)
            {
                if (toCheck.IntersectRectangle.Intersects(levelObject.IntersectRectangle) && levelObject != toCheck)
                {
                    levelObject.Position = new Vector2(toCheck.Position.X + toCheck.Texture.Width,
                                                       levelObject.Position.Y);
                }
            }
        }

        /// <summary>
        /// Creates a static object, like a levelblock
        /// </summary>
        /// <param name="type"></param>
        /// <param name="textureAsset"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public LevelBlock CreateStaticObject(string type, string textureAsset, Vector2 position)
        {
            String switchType = type.ToLower();

            LevelBlock block = null;

            switch(switchType)
            {
                case "levelblock":
                    block = new LevelBlock(textureAsset, position);
                    break;
                    
                default:
                    block = new LevelBlock();
                    break;

            }

            if(loadContentOnCreation)
            {
                block.Load(contentManager);
            }
            
            block.CollisionItem = movementManager.InsertItem(block);
            level.Entities.Add(block);

            return block;
        }

        /// <summary>
        /// Creates a movable object
        /// </summary>
        /// <param name="type">What kind of movable objects you want to create</param>
        /// <param name="textureAsset">The sprite for this object</param>
        /// <param name="position">The start position for this object</param>
        /// <param name="weight">What weight this object has</param>
        /// <param name="speed">acceleration</param>
        /// <param name="collisionCueName">CollisionCue</param>
        /// <param name="options">Several objects need different options, which can be added here. Since it's a param, you can add as many as you want</param>
        /// <returns></returns>
        public MovingObject CreateMovableObject(string type, string textureAsset, Vector2 position, int acceleration, float maxSpeed,  params object[] options)
        {
            String switchType = type.ToLower();

            MovingObject movingObject = null;
            
            switch (switchType)
            {
                case "player":
                    if(options.Length == 0)
                    {
                        throw new ArgumentException("You tried creating a Player, but the parametrs were not compatible");
                    }
                    int lifes = (int)options[0];
                    movingObject = new Player(textureAsset, position, acceleration, maxSpeed, lifes);
                    ((Player) movingObject).OnAction += OnAction;
                    break;
                case "greenturtle":
                    if(options.Length == 0)
                    {
                        throw new ArgumentException("You tried creating a GreenTurtle, but the parametrs were not compatible");
                    }
                    Point boundaries = (Point)options[0];
                    movingObject = new GreenTurtle(@"Graphics\defaultEnemy", level.SpawnPosition, acceleration, maxSpeed, boundaries);
                    break;
            }

            //Default actions we need to do for all created objects
//            WorldObjects.Add(movingObject);
//            if(loadContentOnCreation)
//            {
//                movingObject.Load(contentManager);
//            }
//            
//            movingObject.CollisionItem = movementManager.InsertItem(movingObject);

            AddObject(movingObject);

            return movingObject;
        }

        /// <summary>
        /// Removes a MovingObject
        /// </summary>
        /// <param name="movableObject">The player to remove</param>
        public void RemoveMovingObject(MovingObject movableObject)
        {
            worldObjects.Remove(movableObject);
        }

        /// <summary>
        /// Gets a list of all the specified movable objects inside the gamestate manager
        /// </summary>
        /// <typeparam name="T">Has to be a MovableObject or one of its subclasse<s/typeparam>
        /// <returns></returns>
        public List<T> GetAllOfType<T>() where T:MovingObject
        {
            List<T> list = new List<T>();

            foreach (MovingObject movableObject in worldObjects)
            {
                T testObject = movableObject as T;

                if(testObject != null)
                {
                    list.Add(testObject);
                }
            }

            return list;
        }

        /// <summary>
        /// Gets called on resume (from spooling state to game state)
        /// </summary>
        /// <param name="sender">The sender of this event</param>
        /// <param name="eventargs">The PauseEventArgs</param>
        public void OnResume(object sender, PausEventArgs eventargs)
        {
            foreach (MovingObject movableObject in WorldObjects)
            {
                movableObject.ClearTimeStamps(sender, eventargs);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventargs"></param>
        public void OnPause(object sender, PausEventArgs eventargs)
        {
            foreach (MovingObject movableObject in WorldObjects)
            {
                movableObject.CreateTimeStamp(sender, eventargs);
            }
        }

        public void setMovementManager(MovementManager movementManager)
        {
            this.movementManager = movementManager;
        }

        public void OnAction(object sender, ActionEventArgs actionEventArgs)
        {
            FRect queryRect = actionEventArgs.queryRect;

            List<IActionTaker> actionObjects = new List<IActionTaker>();

            movementManager.FindIntersectingObjects<IActionTaker>(queryRect, ref actionObjects);

            foreach (IActionTaker actionObject in actionObjects)
            {

            }

            CreateMovableObject("greenturtle", @"Graphics\defaultEnemy", actionEventArgs.caller.Position, 3, 3, new Point(0, 500));
        }
    }
}