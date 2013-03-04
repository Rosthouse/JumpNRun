using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Editor
{
    public class XnaForms
    {
        private DeviceManager deviceManager;
        private ContentManager contentManager;
        private GameServiceContainer services;
        private GameTime gameTime;

        public GraphicsDevice GraphicsDevice
        {
            get { return deviceManager.GraphicsDevice; }
        }

        public ContentManager Content
        {
            get { return contentManager; }
        }

        public GameServiceContainer Services
        {
            get { return services; }
        }

        public GameTime GameTime
        {
            get { return gameTime; }
        }


        public XnaForms(IntPtr handle, int width, int height)
        {
            deviceManager = new DeviceManager(handle, width, height);
            

            services = new GameServiceContainer();
            services.AddService(typeof (IGraphicsDeviceService), deviceManager);

            contentManager = new ContentManager(services);

            gameTime = new GameTime();
        }

        public void Resize(int width, int height)
        {
            if(width > 0 && height > 0)
            {
                PresentationParameters pp = GraphicsDevice.PresentationParameters;
                pp.BackBufferWidth = width;
                pp.BackBufferHeight = height;

                GraphicsDevice.Reset(pp);
            }
        }
    }
}