using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Frostbyte
{
    class AudioManager
    {
        public Dictionary<string, SoundEffect> soundEffects = new Dictionary<string, SoundEffect>();
        public Dictionary<string, Song> backgroundMusic = new Dictionary<string, Song>();

        public void AddBackgroundMusic(string name)
        {
            try{
                backgroundMusic[name] = This.Game.Content.Load<Song>("Audio/" + name);
            }
            catch (NoAudioHardwareException)
            {
                // Ignore it...
            }
        }

        public void PlayBackgroundMusic(string name)
        {
            if (backgroundMusic.ContainsKey(name))
            {
                try
                {
                    MediaPlayer.IsRepeating = true;
                    MediaPlayer.Play(backgroundMusic[name]);
                }
                catch (InvalidOperationException)
                {
                    MediaPlayer.Stop();
                }
            }
        }

        public void BackgroundMusicVolume(float volume)
        {
            MediaPlayer.Volume = volume;
        }

        public void AddSoundEffect(string name)
        {
            try
            {
                soundEffects[name] = This.Game.Content.Load<SoundEffect>("Audio/" + name);
            }
            catch (NoAudioHardwareException)
            {
                // Ignore it...
            }
        }

        public void PlaySoundEffect(string name)
        {
            PlaySoundEffect(name, 1f);
        }

        public void PlaySoundEffect(string name, float volume)
        {
            if (soundEffects.ContainsKey(name))
            {
                var sound = soundEffects[name].CreateInstance();
                sound.Volume = volume;
                sound.Play();
            }
        }

        public void Pause()
        {
            if (MediaPlayer.State == MediaState.Playing)
            {
                MediaPlayer.Pause();
            }
            else if (MediaPlayer.State == MediaState.Paused)
            {
                MediaPlayer.Resume();
            }
        }

        internal void Stop()
        {
            MediaPlayer.Stop();
        }
    }
}
