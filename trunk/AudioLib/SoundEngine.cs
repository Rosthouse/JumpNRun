using System;
using System.Collections.Generic;
using AudioLib.Events;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace AudioLib
{
    public class SoundEngine
    {
        private Dictionary<string, SoundEffect> effectList;
        private Dictionary<string, Song> songList;
        private string soundPath;
        private ContentManager contentManager;

        public SoundEngine(string path, ContentManager contentManager)
        {
            effectList = new Dictionary<string, SoundEffect>();
            songList = new Dictionary<string, Song>();
            soundPath = path;
            this.contentManager = contentManager;
        }
        

        public void AddSong(String key, String path)
        {
            Song song = contentManager.Load<Song>(path);
            songList.Add(key, song);
        }

        public void AddEffect(String key, String path)
        {
            SoundEffect effect = contentManager.Load<SoundEffect>(soundPath + path);
            effectList.Add(key, effect);
        }

        private void PlaySong(Song song)
        {
            MediaPlayer.Play(song);
        }

        public void PlaySong(object sender, PlaySongEventArgs e)
        {
            
            PlaySong(e.song);
        }

        private void PlaySoundEffect(SoundEffectInstance effect)
        {
            
            effect.Play();
        }

        public void PlaySoundEffect(object sender, PlaySoundEffectEventArgs e)
        {
            if(effectList.ContainsKey(e.effect))
            {
                PlaySoundEffect(effectList[e.effect].CreateInstance());
            }
        }

        public void StopSong(object sender, StopSongEventArgs e)
        {
            MediaPlayer.Stop();
        }
    }
}
