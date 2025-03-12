using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace DungeonShooter
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource sourceBGM;
        [SerializeField] private AudioSource sourceEffect;
        public AudioClip[] clips;

        public static AudioManager instance;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }
        private void Start()
        {
            PlayBGM();
        }
        public void PlayBGM()
        {
            sourceBGM.Play();
        }
        public void PlayClip(string _clipname)
        {
            if (sourceEffect.isPlaying)
                sourceEffect.Stop();

            sourceEffect.clip = Array.Find(clips, x => x.name == _clipname);
            sourceEffect.Play();
        }
    }
}