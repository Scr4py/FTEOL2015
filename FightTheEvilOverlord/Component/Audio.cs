
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;

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
        float timer;
        float playTimer = 2000;
        bool play;
        float volume = 1.0f;

        public void Start()
        {
            play = false;
            EventManager.OnUpdate += Update;
        }

        private void Update(GameTime gameTime)
        {
            this.timer += gameTime.ElapsedGameTime.Milliseconds;
            if (this.timer > playTimer)
            {
                GameSoundRegulation();
            }
        }

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
            if (instance.Volume >= 0.01f)
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
            if (gameInstance.Volume <= 0.5f)
            {
                gameInstance.Volume += 0.001f;
            }
        }

        public override void Destroy()
        {
            EventManager.OnUpdate -= Update;
            base.Destroy();
        }
    }
}
