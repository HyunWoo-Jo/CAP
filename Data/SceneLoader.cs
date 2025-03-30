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

        // 로딩 완료 여부
        public bool IsDone() {
            if (_loadAsyncOper != null) {
                return _loadAsyncOper.isDone;
            }
            return false;
        }
        // 퍼센트지
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

        
        // 씬 로드
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
        /// 지연후 자동 로딩을 true로 변환
        /// </summary>
        /// <param name="delay"></param>
        private async void DelayedAllowSceneActivation(float delay) {
           await UniTask.Delay(TimeSpan.FromSeconds(delay));
            AllowSceneActivation(true);
        }

        #region public
        /// <summary>
        /// 비동기 로딩
        /// </summary>
        /// <param name="nextScene"> 다음 씬 이름 </param>
        /// <param name="isAllowSceneActivation"> 바로 로딩 할것인지 여부 </param>
        /// <param name="delayTime"> delay후 isAllow를 실행시킬 시간</param>
        public void LoadAsync(SceneData.SceneName nextScene, bool isAllowSceneActivation = false, float delayTime = 0f) {
            _loadAsyncOper = SceneManager.LoadSceneAsync(nextScene.ToString());
            AllowSceneActivation(isAllowSceneActivation); // 씬 자동 전환 방지
            StartCoroutine(LoadProgress());
            if(delayTime > 0) {
                DelayedAllowSceneActivation(delayTime);
            }
        }
        public void AllowSceneActivation(bool isActivation) {
            _loadAsyncOper.allowSceneActivation = isActivation;
        }


        // 로딩 데이터 폐기
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
