using UnityEngine;
using NaughtyAttributes;
using System.Collections;

namespace Core
{
    public class AudioManager : Singleton<AudioManager>
    {
        [BoxGroup("Audio sources")]
        [SerializeField] private AudioSource mainAudio, soundAudio1, soundAudio2, soundAudio3;

        #region SOUNDS

        [BoxGroup("Main sounds")]
        [SerializeField] private AudioClip[] musics;

        [BoxGroup("Cick button")]
        [SerializeField] private AudioClip[] clickButton;
        [BoxGroup("Lose sound")]
        [SerializeField] private AudioClip loseSound;
        [BoxGroup("Win sound")]
        [SerializeField] private AudioClip winSound;

        #endregion SOUNDS

        private AudioClip currentSoundClip;
        private AudioClip currentMusicClip;

        private int numberButtonClick = 0, numberMusic = 0;
        private int countButtonClick, countMusic;

        private const float stepChangeVolume = .05f;
        private const float timeStepChangeVolume = .05f;

        private float volumeMusic = 1f;
        private float volumeSound = 1f;

        protected override void AfterAwaik()
        {
            countButtonClick = clickButton.Length - 1;
            countMusic = musics.Length - 1;
        }

        public void ChangeMusicVolume(float value)
        {
            if(value > 0.4f)
            {
                value = 0.4f;
            }

            volumeMusic = value;
            mainAudio.volume = value;
        }

        public void ChangeSoundVolume(float value)
        {
            volumeSound = value;
            soundAudio1.volume = value;
            soundAudio2.volume = value;
            soundAudio3.volume = value;
        }

        public void DisableVolume()
        {
            ChangeMusicVolume(0);
            ChangeSoundVolume(0);
        }

        public void EnableVolume()
        {
            ChangeMusicVolume(1);
            ChangeSoundVolume(1);
        }

        public void PlayMusic()
        {
            currentMusicClip = musics[numberMusic];

            numberMusic++;

            if (numberMusic > countMusic)
            {
                numberMusic = 0;
            }

            mainAudio.clip = currentMusicClip;
            mainAudio.Play();

            float lengthMusic = mainAudio.clip.length;
            Invoke(nameof(PlayMusic), lengthMusic);
        }

        public void PlayUISound(TypeUISound typeSound)
        {
            if (typeSound == TypeUISound.ButtonClick)
            {
                PlaySound(clickButton[numberButtonClick]);

                numberButtonClick++;

                if (numberButtonClick > countButtonClick)
                {
                    numberButtonClick = 0;
                }
            }
            else if (typeSound == TypeUISound.LoseSound)
            {
                PlaySound(loseSound);
            }
            else if (typeSound == TypeUISound.WinSound)
            {
                PlaySound(winSound);
            }
        }

        private void PlaySound(AudioClip audio)
        {
            currentSoundClip = audio;

            PlaySound();
        }

        private void PlaySound()
        {
            AudioSource source = soundAudio1;

            if (soundAudio1.isPlaying)
            {
                if (soundAudio2.isPlaying)
                {
                    source = soundAudio3;
                }
                else
                {
                    source = soundAudio2;
                }
            }
            else
            {
                source = soundAudio1;
            }

            source.clip = currentSoundClip;
            source.Play();
        }

        private IEnumerator CoChangeAudio()
        {
            while (mainAudio.volume > 0)
            {
                mainAudio.volume -= stepChangeVolume;
                yield return new WaitForSeconds(timeStepChangeVolume);
            }

            StartCoroutine(CoPlayMusic());
        }

        private IEnumerator CoTakeOffMusic()
        {
            while (mainAudio.volume > 0)
            {
                mainAudio.volume -= stepChangeVolume;
                yield return new WaitForSeconds(timeStepChangeVolume);
            }
        }

        private IEnumerator CoPlayMusic()
        {
            mainAudio.clip = currentMusicClip;
            mainAudio.Play();

            while (mainAudio.volume < volumeMusic)
            {
                mainAudio.volume += stepChangeVolume;
                yield return new WaitForSeconds(timeStepChangeVolume);
            }

            mainAudio.volume = volumeMusic;
        }
    }
}
