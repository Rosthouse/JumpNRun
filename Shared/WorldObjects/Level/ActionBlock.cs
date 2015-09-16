using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JumpNRunShared.Interfaces;
using Microsoft.Xna.Framework.Audio;

namespace JumpNRunShared.WorldObjects.Level
{
    class ActionBlock: LevelBlock, IActionTaker
    {
        public void Action()
        {
            PlaySound("Activated");
        }

        public override void Load(Microsoft.Xna.Framework.Content.ContentManager contentManager)
        {
            soundEffectList.Add("dance", contentManager.Load<SoundEffect>(@"Audio\Effects\smb_coin"));

            base.Load(contentManager);
        }
    }
}
