using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace AudioLib
{
    public class SoundEffectList
    {
        private Dictionary<string, SoundEffect> soundEffectList;

        public SoundEffectList()
        {
            soundEffectList = new Dictionary<string, SoundEffect>();
        }

        public void Add(string name, SoundEffect soundEffect)
        {
            soundEffectList.Add(name, soundEffect);
        }

        public void Play(string name)
        {
            SoundEffect effect = null;
            try
            {
                effect = soundEffectList[name];
            } catch(KeyNotFoundException e)
            {
                return;
            }
            

            SoundEffectInstance instance = effect.CreateInstance();
            instance.Play();
            instance.Stop(false);
        }
    }
}
