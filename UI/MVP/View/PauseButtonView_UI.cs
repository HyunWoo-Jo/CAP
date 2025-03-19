
using UnityEngine;
using System.Runtime.CompilerServices;
using UnityEngine.EventSystems;
using UnityEngine.Assertions;
////////////////////////////////////////////////////////////////////////////////////
// Auto Generated Code
#if UNITY_EDITOR
[assembly: InternalsVisibleTo("CA.Test")]
#endif
namespace N.UI
{
    public interface IPauseButtonView_UI : IView_UI {
        // Your logic here
    }

    public class PauseButtonView_UI : View_UI<PauseButtonPresenter_UI,PauseButtonModel_UI> ,IPauseButtonView_UI
    {
        protected override void CreatePresenter() {
            _presenter = new PauseButtonPresenter_UI();
            _presenter.Init(this);  
        }

        // Your logic here
        #region private
        [SerializeField] private EventTrigger _pauseButton;
        private void Awake() {
#if UNITY_EDITOR
            Assert.IsNotNull(_pauseButton);
#endif
        }
        #endregion        

        #region public

        #endregion

        #region internal

        #endregion
    }
}
