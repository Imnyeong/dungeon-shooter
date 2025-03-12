using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonShooter
{
    [RequireComponent(typeof(Character))]
    public class CharacterAudio : MonoBehaviour
    {
        private Character player;
        [SerializeField] private AudioSource source;

        private void Start()
        {
            player = GetComponent<Character>();
            source = GetComponent<AudioSource>();
        }
        public void PlayClip(string _clipname)
        {
            SyncAudio(_clipname);
            SendPacket();
        }
        private void SendPacket()
        {
            AudioPacket packet = new AudioPacket()
            {
                playerID = player.id,
                clipName = source.clip.name
            };
            WebSocketRequest request = new WebSocketRequest()
            {
                packetType = PacketType.Audio,
                data = JsonUtility.ToJson(packet)
            };
            WebSocketManager.instance.SendPacket(JsonUtility.ToJson(request));
        }
        public void SyncAudio(string _clipname)
        {
            if (source.isPlaying)
                source.Stop();

            source.clip = Array.Find(AudioManager.instance.clips, x => x.name == _clipname);
            source.Play();
        }
    }
}
