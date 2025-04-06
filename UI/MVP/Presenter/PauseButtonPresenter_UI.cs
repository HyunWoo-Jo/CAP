
using UnityEngine;
using System.Runtime.CompilerServices;
using UnityEngine.EventSystems;
using CA.Utills;
using CA.Data;
////////////////////////////////////////////////////////////////////////////////////
// Auto Generated Code
#if UNITY_EDITOR
[assembly: InternalsVisibleTo("CA.Test")]
#endif
namespace CA.UI {

    public class PauseButtonPresenter_UI : Presenter_UI<PauseButtonModel_UI, IPauseButtonView_UI> {
        // Your logic here
        #region internal
        internal void InitButton(EventTrigger pauseButton) {
            pauseButton.AddEventButton(EventTriggerType.PointerDown, () => {
                UIManager.Instance.InstantiateUI<PauseView_UI>(10);
            }, GetType().Name, nameof(InitButton));

           
        }
        #endregion
    }
}
