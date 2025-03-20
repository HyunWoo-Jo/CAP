
using UnityEngine;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Assertions;
////////////////////////////////////////////////////////////////////////////////////
// Auto Generated Code
#if UNITY_EDITOR
[assembly: InternalsVisibleTo("CA.Test")]
#endif
namespace CA.UI
{
    public interface ILoadingSceneView_UI : IView_UI {
        // Your logic here
        internal void UpdateUI(float progress);
    }

    public class LoadingSceneView_UI : View_UI<LoadingScenePresenter_UI,LoadingSceneModel_UI> ,ILoadingSceneView_UI
    {
        protected override void CreatePresenter() {
            _presenter = new LoadingScenePresenter_UI();
            _presenter.Init(this);  
        }


        // Your logic here
        #region private
        [SerializeField] private Image _loadFillImage;
        [SerializeField] private TextMeshProUGUI _progressText;
        private void Awake() {
#if UNITY_EDITOR
            Assert.IsNotNull(_loadFillImage);
            Assert.IsNotNull(_progressText);
#endif
        }

        #endregion

        #region public
        public void UpdateProgress(float progress) {
            _presenter.UpdateUI(progress);
        }

        #endregion

        #region internal
        void ILoadingSceneView_UI.UpdateUI(float progress) {
            if (progress >= 0.88f) progress = 1f; // 일정 수치 이상이면 100으로 표기
            _loadFillImage.fillAmount = progress;
            _progressText.text = ((int)(progress * 100f)).ToString() + "%";
        }
        #endregion
    }
}
