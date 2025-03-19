
using UnityEngine;
using System.Runtime.CompilerServices;
////////////////////////////////////////////////////////////////////////////////////
// Auto Generated Code
#if UNITY_EDITOR
[assembly: InternalsVisibleTo("CA.Test")]
#endif
namespace N.UI
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
        
        #endregion        

        #region public

        #endregion

        #region internal

        #endregion
    }
}
