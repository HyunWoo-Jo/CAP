
using UnityEngine;
using System.Runtime.CompilerServices;
////////////////////////////////////////////////////////////////////////////////////
// Auto Generated Code
#if UNITY_EDITOR
[assembly: InternalsVisibleTo("CA.Test")]
#endif
namespace CA.UI
{
    public interface ILevelView_UI : IView_UI {
        // Your logic here
    }

    public class LevelView_UI : View_UI<LevelPresenter_UI,LevelModel_UI> ,ILevelView_UI
    {
        protected override void CreatePresenter() {
            _presenter = new LevelPresenter_UI();
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
