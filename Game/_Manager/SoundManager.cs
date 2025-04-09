using CA.Data;
using UnityEngine;
using CA.DesignPattern;
using UnityEngine.Assertions;
using System.Collections.Generic;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System;
using System.Linq;

namespace CA.Game
{
    public class SoundManager : Singleton<SoundManager>
    {
        [SerializeField] private GameObject _audioPlayItemPrefab;
        [SerializeField] private AudioSource _backgroundAudioSource;
        private ObjectPool<SoundPlayItem> _audio_pool;
        private bool _isMute; // mute ����
        private float _volume;

        protected override void Awake() {
            base.Awake();
            if (_audioPlayItemPrefab == null) {
                _audioPlayItemPrefab = Resources.Load<GameObject>("item/SoundPlayItem");
            }
#if UNITY_EDITOR
            Assert.IsNotNull(_audioPlayItemPrefab);
            // Resources �ε嵵 ����
            Assert.IsNotNull(Resources.Load<GameObject>("item/SoundPlayItem"));
#endif
            _audio_pool = ObjectPoolBuilder<SoundPlayItem>.Instance(_audioPlayItemPrefab, 20). // ObjectPool ����
                DontDestroy().
                Build();

            // �ʱ� volume ����
            _volume = _audioPlayItemPrefab.GetComponent<AudioSource>().volume;
        }



        /// <summary>
        /// Audio Clip �ҷ����� ĳ���ؼ� ���
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public AudioClip LoadSoundAsset(string key) {
            return DataManager.Instance.LoadAssetSync<AudioClip>(key);
        }
        /// <summary>
        /// Audio Clip ����
        /// </summary>
        /// <param name="key"></param>
        public void ReleseSoundAsset(string key) {
            DataManager.Instance.ReleaseAsset(key);
        }

        public void PlaySound(AudioClip clip, Vector3 position) {
            SoundPlayItem item = _audio_pool.BorrowItem();
            item.SetClip(clip);
            item.Mute(_isMute);
            item.Play(position);
            item.SetSoundVolume(_volume);
            Repay(item, clip); // �ڵ� ��ȯ
        }

        public async void Repay(SoundPlayItem item, AudioClip clip) {
            await UniTask.Delay(TimeSpan.FromSeconds(clip.length));
            _audio_pool.RepayItem(item);
        }

        public void PlayBackground(AudioClip clip) {
            float originalVolume = _backgroundAudioSource.volume;
            if (_backgroundAudioSource.clip != null) {
                // ������� ��ü
                // ���̵� �ƿ�
                _backgroundAudioSource.DOFade(0f, 0.5f).OnComplete(() => {
                    _backgroundAudioSource.clip = clip;
                    _backgroundAudioSource.Play();
                    // ���̵� ��
                    _backgroundAudioSource.DOFade(originalVolume, 0.5f);
                });
            } else {
                _backgroundAudioSource.clip = clip;
                _backgroundAudioSource.Play();
                // ���̵� ��
                _backgroundAudioSource.DOFade(originalVolume, 0.5f);
            }
        }
       

        public void MuteAll(bool isMute) {
            _isMute = isMute;
            // Audio pool item ��ȸ
            foreach (SoundPlayItem item in _audio_pool.AllObjects()) {
                item.Mute(isMute);
            }
        }
        /// <summary>
        /// sound item volume ����
        /// </summary>
        /// <param name="volume"></param>
        public void SetSoundVolume(float volume) {
            // Audio pool item ��ȸ
            foreach (SoundPlayItem item in _audio_pool.AllObjects()) {
                item.SetSoundVolume(volume);
            }
        }
        /// <summary>
        /// background mute ����
        /// </summary>
        /// <param name="isMute"></param>
        public void BackgroundMute(bool isMute) {
            _backgroundAudioSource.mute = isMute;
        }
        /// <summary>
        /// background volume ����
        /// </summary>
        /// <param name="volume"></param>
        public void SetBackgroundSoundVolume(float volume) {
            _backgroundAudioSource.volume = volume;
        }
    }
}
