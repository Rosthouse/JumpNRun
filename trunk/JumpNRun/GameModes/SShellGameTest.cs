using System;
using JumpNRunShared.ObjectShell;
using JumpNRunShared.ObjectShell.Implementations;
using JumpNRunShared.ObjectShell.Implementations.LevelBlocks;
using JumpNRunShared.ObjectShell.ObjectShellFactory;
using JumpNRunShared.WorldObjects.Level;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SimplePhysicsAndCollision;
using SimplePhysicsAndCollision.Interfaces;

namespace JumpNRunClient.GameModes
{
    

    class SShellGameTest: GameMode
    {

        private SpriteBatch spriteBatch;
        private ObjectShell player;
        private MovementManager movementManager;

        private ObjectShellFactory objectShellFactory;


        public SShellGameTest(Game game) : base(game)
        {
            movementManager = new MovementManager();

            objectShellFactory = new ObjectShellFactory(Game.Content, movementManager);
        }

        public override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            player = objectShellFactory.GetObjectShell<PlayerShell>("");

            LevelBlockShell levelBlock = objectShellFactory.GetObjectShell<LevelBlockShell>("");

            movementManager.SetLevelSize(new Vector2(Game.GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height));

            player.LoadContent(Game.Content);
        }

        public override void UnloadContent()
        {
            throw new NotImplementedException();
        }

        public override void Enable()
        {
            //throw new NotImplementedException();
        }

        public override void Disable()
        {
            //throw new NotImplementedException();
        }

        public override void Unload()
        {
            //throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            player.Update(gameTime);
            player.PostController(gameTime);

            movementManager.Step(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            player.Draw(gameTime, spriteBatch);

            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
