using CA.Data;
using UnityEngine;
using CA.DesignPattern;
using UnityEngine.Assertions;

namespace CA.Game
{
    public class SoundManager : Singleton<SoundManager>
    {
        [SerializeField] private GameObject _audioPlayItemPrefab;

        private ObjectPool<SoundPlayItem> _audio_pool;

        protected override void Awake() {
            base.Awake();
            if (_audioPlayItemPrefab == null) {
                _audioPlayItemPrefab = Resources.Load<GameObject>("item/SoundPlayItem");
            }
#if UNITY_EDITOR
            Assert.IsNotNull(_audioPlayItemPrefab);
            // Resources 로드도 검증
            Assert.IsNotNull(Resources.Load<GameObject>("item/SoundPlayItem"));
#endif
            _audio_pool = ObjectPoolBuilder<SoundPlayItem>.Instance(_audioPlayItemPrefab, 20). // ObjectPool 생성
                Static().
                DontDestroy().
                Build();

        }



        /// <summary>
        /// Audio Clip 불러오기 캐싱해서 사용
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public AudioClip LoadSoundAsset(string key) {
            return DataManager.Instance.LoadAssetSync<AudioClip>(key);
        }
    }
}
