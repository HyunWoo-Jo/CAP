
using UnityEngine;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine.Assertions;
////////////////////////////////////////////////////////////////////////////////////
// Auto Generated Code
#if UNITY_EDITOR
[assembly: InternalsVisibleTo("CA.Test")]
#endif
namespace CA.UI
{
    public interface IGoldView_UI : IView_UI {
        // Your logic here

        internal void UpdateUI(string goldText);
    }

    public class GoldView_UI : View_UI<GoldPresenter_UI,GoldModel_UI> ,IGoldView_UI
    {
        protected override void CreatePresenter() {
            _presenter = new GoldPresenter_UI();
            _presenter.Init(this);  
        }



        // Your logic here
        #region private
        [SerializeField] private TextMeshProUGUI _goldText;

        private void Awake() {
#if UNITY_EDITOR
            Assert.IsNotNull(_goldText);
#endif
        }
        #endregion

        #region public
        public void SetGold(int gold) {

        }
        #endregion

        #region internal
        void IGoldView_UI.UpdateUI(string goldText) {
            
        }
        #endregion
    }
}
