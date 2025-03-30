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
            // Test �ڵ� Play Scene���� �̵�
            if (SceneData.nextScene == SceneData.SceneName.None) {
                SceneData.nextScene = SceneData.SceneName.PlayScene; 
            }

#endif
        }
        private void Start() {
            SceneLoader.Instance.LoadAsync(SceneData.nextScene, false);
        }

        private void Update() {
            // UI�� �ε����� 1�̸� �ε�
            if (_view.GetProgress() >= 1.0f) {
                SceneLoader.Instance.AllowSceneActivation(true);
            }
        }

        private void OnEnable() {
            SceneLoader.Instance.AddProgressHandler(_view.UpdateProgress); // UI ���� event
        }

        private void OnDisable() {
            SceneLoader.Instance.RemoveProgressHandler(_view.UpdateProgress); 
        }
    }
}
