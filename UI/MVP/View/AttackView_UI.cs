
using UnityEngine;
using System.Runtime.CompilerServices;
////////////////////////////////////////////////////////////////////////////////////
// Auto Generated Code
#if UNITY_EDITOR
[assembly: InternalsVisibleTo("CA.Test")]
#endif
namespace N.UI
{
    public interface IAttackView_UI : IView_UI {
        // Your logic here
    }

    public class AttackView_UI : View_UI<AttackPresenter_UI,AttackModel_UI> ,IAttackView_UI
    {
        protected override void CreatePresenter() {
            _presenter = new AttackPresenter_UI();
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
