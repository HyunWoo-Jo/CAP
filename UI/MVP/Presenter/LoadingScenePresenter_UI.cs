
using UnityEngine;
using System.Runtime.CompilerServices;
////////////////////////////////////////////////////////////////////////////////////
// Auto Generated Code
#if UNITY_EDITOR
[assembly: InternalsVisibleTo("CA.Test")]
#endif
namespace CA.UI {

    public class LoadingScenePresenter_UI : Presenter_UI<LoadingSceneModel_UI, ILoadingSceneView_UI> {
        // Your logic here
        #region private

        #endregion

        #region internal
        internal void UpdateUI(float progress) {
            if (_model.startTime == 0) _model.startTime = Time.time; // 초기화
            float elapsedTime = Time.time - _model.startTime; // 경과 시간

            if (elapsedTime < LoadingSceneModel_UI.minLoadTime) {
                // 최소 로딩 시간 전에는 가짜 진행률 사용
                _model.fakeLoading = true;
                _model.fakeProgress = Mathf.Lerp(_model.fakeProgress, 1f, Time.deltaTime / LoadingSceneModel_UI.minLoadTime);
            } else {
                _model.fakeLoading = false;
            }
            _model.displayProgress = _model.fakeLoading ? _model.fakeProgress : progress;
            _view.UpdateUI(_model.displayProgress);
        }
        #endregion
    }
}
