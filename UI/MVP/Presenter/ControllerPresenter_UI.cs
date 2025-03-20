
using UnityEngine;
using System.Runtime.CompilerServices;
////////////////////////////////////////////////////////////////////////////////////
// Auto Generated Code
#if UNITY_EDITOR
[assembly: InternalsVisibleTo("CA.Test")]
#endif
namespace CA.UI {

    public class ControllerPresenter_UI : Presenter_UI<ControllerModel_UI, IControllerView_UI> {
        // Your logic here
        #region internal
        internal void UpdateDirection(Vector2 direction) {
            Vector2 localPosition = direction;
            _view.UpdatePositionControllerCenterImage(localPosition);
        }
        #endregion
    }
}
