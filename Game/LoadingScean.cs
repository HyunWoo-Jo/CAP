using CA.UI;
using UnityEngine;
using UnityEngine.Assertions;
using CA.Data;
using CA.DesignPattern;
using UnityEngine.SceneManagement;
namespace CA.Game
{
    public class LoadingScean : MonoBehaviour
    {
        [SerializeField] private LoadingSceneView_UI _view;
        public void Awake() {
#if UNITY_EDITOR
            Assert.IsNotNull(_view);
            // Test 코드 Play Scene으로 이동
            if (SceneData.nextScene == SceneData.SceneName.None) {
                SceneData.nextScene = SceneData.SceneName.PlayScene; 
            }

#endif
        }
        private void Start() {
            SceneLoader.Instance.LoadAsync(SceneData.nextScene, false);
        }

        private void Update() {
            // UI상 로딩률이 1이면 로드
            if (_view.GetProgress() >= 1.0f) {
                SceneLoader.Instance.AllowSceneActivation(true);
            }
        }

        private void OnEnable() {
            SceneLoader.Instance.AddProgressHandler(_view.UpdateProgress); // UI 갱신 event
        }

        private void OnDisable() {
            SceneLoader.Instance.RemoveProgressHandler(_view.UpdateProgress); 
        }
    }
}
