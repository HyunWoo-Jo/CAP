using CA.DesignPattern;
using UnityEngine;
using UnityEngine.Assertions;

namespace CA.Game
{
    public class SoundPlayItem : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        
        private void Awake() {
#if UNITY_EDITOR
            Assert.IsNotNull(_audioSource);
#endif
        }

        public void SetClip(AudioClip clip) {
            _audioSource.clip = clip;
        }

        public void Play() {
            _audioSource.Play();
        }

        public void Play(Vector3 position) {
            _audioSource.transform.position = position;
            _audioSource.Play();
        }

        public void Mute(bool isMute) {
            _audioSource.mute = isMute;
        }

        public void SetSoundVolume(float volume) {
            _audioSource.volume = volume;
        }
        
    }
}
