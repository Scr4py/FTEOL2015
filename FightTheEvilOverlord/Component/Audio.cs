﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace FightTheEvilOverlord
{
    class Audio : Component
    {
        Menue menue;
        SoundEffectInstance instance;
        SoundEffectInstance gameInstance;
        private SoundEffect effect;
        private SoundEffect menuEffect;
        private SoundEffect gameEffect;
        float volume = 1.0f;

        public void SetAndPlay(SoundEffect effect)
        {
            this.effect = effect;
            effect.Play(volume,0, 0);
        }

        public void SetMenuMusicAndPlay(SoundEffect menuEffect)
        {
            this.menuEffect = menuEffect;
            instance = menuEffect.CreateInstance();
            instance.Volume = 1;
            instance.IsLooped = true;
            instance.Play();
        }

        public void StopMusic()
        {
            for (float i = 0.01f; i < instance.Volume; i++)
            {
                instance.Volume -= 0.01f;
                if (instance.Volume == 0.01000067f)
                {
                    instance.Volume = 0;
                    if (instance.Volume == 0)
                    {
                        instance.Stop();
                    }
                }
            }
        }

        public void SetGameMusic(SoundEffect effect)
        {
            this.gameEffect = effect;
            gameInstance = gameEffect.CreateInstance();
            gameInstance.Volume = 0;
            gameInstance.IsLooped = true;
            gameInstance.Play();
        }

        public void GameSoundRegulation()
        {
            if (this.menue.Audio.volume == 0)
            {
                for (float i = 0; i < 100.0f; i++)
                {
                    gameInstance.Volume += 0.01f;
                }  
            }
            if (gameInstance.Volume == 1)
            {
               gameInstance.Volume = 1;
            }
        }

    }
}
