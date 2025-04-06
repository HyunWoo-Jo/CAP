
using UnityEngine;
using System.Runtime.CompilerServices;
using UnityEngine.EventSystems;
using UnityEngine.Assertions;
////////////////////////////////////////////////////////////////////////////////////
// Auto Generated Code
#if UNITY_EDITOR
[assembly: InternalsVisibleTo("CA.Test")]
#endif
namespace CA.UI
{
    public interface IPauseView_UI : IView_UI {
        // Your logic here
    }

    public class PauseView_UI : View_UI<PausePresenter_UI,PauseModel_UI> ,IPauseView_UI
    {
        protected override void CreatePresenter() {
            _presenter = new PausePresenter_UI();
            _presenter.Init(this);  
        }
        
        // Your logic here
        #region private
        [SerializeField] private EventTrigger _giveUpButton;
        [SerializeField] private EventTrigger _retryButton;
        [SerializeField] private EventTrigger _exitButton;
        private void Awake() {
#if UNITY_EDITOR
            Assert.IsNotNull(_giveUpButton);
            Assert.IsNotNull(_retryButton);
            Assert.IsNotNull(_exitButton);
#endif
            _presenter.InitButton(_giveUpButton, _retryButton, _exitButton);
        }
        #endregion        

        #region public

        #endregion

        #region internal

        #endregion
    }
}
