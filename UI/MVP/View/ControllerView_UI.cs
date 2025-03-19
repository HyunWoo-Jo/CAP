
using UnityEngine;
using System.Runtime.CompilerServices;
using UnityEngine.Assertions;
////////////////////////////////////////////////////////////////////////////////////
// Auto Generated Code
#if UNITY_EDITOR
[assembly: InternalsVisibleTo("CA.Test")]
#endif
namespace N.UI
{
    public interface IControllerView_UI : IView_UI {
        // Your logic here
        internal void UpdatePositionControllerCenterImage(Vector2 localPosition);
    }

    public class ControllerView_UI : View_UI<ControllerPresenter_UI,ControllerModel_UI> ,IControllerView_UI
    {
        protected override void CreatePresenter() {
            _presenter = new ControllerPresenter_UI();
            _presenter.Init(this);  
        }

        // Your logic here
        #region private
        [SerializeField] private RectTransform _centerImage;

        private void Awake() {
#if UNITY_EDITOR // Assertion
            Assert.IsNotNull(_centerImage);
#endif
            AddUIManager();
        }
        #endregion        

        #region public
        /// <summary>
        /// Direction 갱신
        /// </summary>
        /// <param name="direction"></param>
        public void UpdateDirection(Vector2 direction) {
            _presenter.UpdateDirection(direction);
        }

        #endregion

        #region internal
        /// <summary>
        /// center image 위치 갱신
        /// </summary>
        /// <param name="localPosition"></param>
        void IControllerView_UI.UpdatePositionControllerCenterImage(Vector2 localPosition) {
            // 위치 변경
            _centerImage.localPosition = localPosition;
        }
        #endregion
    }
}
