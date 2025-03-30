using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using CA.DesignPattern;
using Cysharp.Threading.Tasks;
namespace CA.Data
{
    public class SceneLoader : Singleton<SceneLoader> 
    {
        private string _loadSceneName;
        private AsyncOperation _loadAsyncOper;
        private Action<float> _progressHandler;

        // �ε� �Ϸ� ����
        public bool IsDone() {
            if (_loadAsyncOper != null) {
                return _loadAsyncOper.isDone;
            }
            return false;
        }
        // �ۼ�Ʈ��
        public float Progress() {
            if (_loadAsyncOper != null) {
                return _loadAsyncOper.progress;
            }
            return 0;
        }
        public void AddProgressHandler(Action<float> action) {
            _progressHandler += action;
        }
        public void RemoveProgressHandler(Action<float> action) {
            _progressHandler -= action;
        }

        
        // �� �ε�
        private IEnumerator LoadProgress() {
            while (_loadAsyncOper != null) {
                float progress = _loadAsyncOper.progress;
                _progressHandler?.Invoke(progress);
                if (progress >= 1f || _loadAsyncOper.isDone) {
                    break;
                }
                yield return null;
            }
        }
        /// <summary>
        /// ������ �ڵ� �ε��� true�� ��ȯ
        /// </summary>
        /// <param name="delay"></param>
        private async void DelayedAllowSceneActivation(float delay) {
           await UniTask.Delay(TimeSpan.FromSeconds(delay));
            AllowSceneActivation(true);
        }

        #region public
        /// <summary>
        /// �񵿱� �ε�
        /// </summary>
        /// <param name="nextScene"> ���� �� �̸� </param>
        /// <param name="isAllowSceneActivation"> �ٷ� �ε� �Ұ����� ���� </param>
        /// <param name="delayTime"> delay�� isAllow�� �����ų �ð�</param>
        public void LoadAsync(SceneData.SceneName nextScene, bool isAllowSceneActivation = false, float delayTime = 0f) {
            _loadAsyncOper = SceneManager.LoadSceneAsync(nextScene.ToString());
            AllowSceneActivation(isAllowSceneActivation); // �� �ڵ� ��ȯ ����
            StartCoroutine(LoadProgress());
            if(delayTime > 0) {
                DelayedAllowSceneActivation(delayTime);
            }
        }
        public void AllowSceneActivation(bool isActivation) {
            _loadAsyncOper.allowSceneActivation = isActivation;
        }


        // �ε� ������ ���
        public void Dispose() {
            if (_loadAsyncOper != null && !string.IsNullOrEmpty(_loadSceneName)) {
                SceneManager.UnloadSceneAsync(_loadSceneName);
                _loadAsyncOper = null;
                _loadSceneName = null;
            }
        }

        



        #endregion
    }
}
