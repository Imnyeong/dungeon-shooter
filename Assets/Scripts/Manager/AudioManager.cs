using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace DungeonShooter
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource source;
        [SerializeField] private AudioClip[] clips;

        public static AudioManager instance;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }
        public void PlayClip(string _clipname)
        {
            if (source.isPlaying)
                source.Stop();

            source.clip = Array.Find(clips, x => x.name == _clipname);
            source.Play();
        }
    }
}