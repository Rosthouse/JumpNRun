using System;
using Microsoft.Xna.Framework.Graphics;

namespace Editor
{
    class DeviceManager : IGraphicsDeviceService

    {
        private GraphicsDevice graphicsDevice;


        public GraphicsDevice GraphicsDevice
        {
            get { return graphicsDevice; }
        }

        public event EventHandler<EventArgs> DeviceDisposing;
        public event EventHandler<EventArgs> DeviceReset;
        public event EventHandler<EventArgs> DeviceResetting;
        public event EventHandler<EventArgs> DeviceCreated;


        public DeviceManager(IntPtr handle, int backBufferWidth, int backBufferHeight)
        {
            PresentationParameters pp = new PresentationParameters();

            
            pp.IsFullScreen = false;
            pp.BackBufferWidth = backBufferWidth;
            pp.BackBufferHeight = backBufferHeight;

            pp.DeviceWindowHandle = handle;
            
            graphicsDevice = new GraphicsDevice(GraphicsAdapter.DefaultAdapter, GraphicsProfile.HiDef, pp);

            graphicsDevice.Disposing += graphicsDevice_Disposing;
            graphicsDevice.DeviceReset += graphicsDevice_DeviceReset;
            graphicsDevice.DeviceResetting += graphicsDevice_DeviceResetting;

            OnDeviceCreated(this, EventArgs.Empty);
            
            
        }
        
        private void OnDeviceCreated(object sender, EventArgs e)
        {
            if(DeviceCreated != null)
            {
                DeviceCreated(sender, e);
            }
        }

        private void graphicsDevice_Disposing(object sender, EventArgs e)
        {
            if(DeviceDisposing != null)
            {
                DeviceDisposing(sender, e);
            }
        }

        private void graphicsDevice_DeviceReset(object sender, EventArgs e)
        {
            if(DeviceReset != null)
            {
                DeviceReset(sender, e);
            }
        }

        private void graphicsDevice_DeviceResetting(object sender, EventArgs e)
        {
            if(DeviceResetting != null)
            {
                DeviceResetting(sender, e);
            }
        }
    }
}