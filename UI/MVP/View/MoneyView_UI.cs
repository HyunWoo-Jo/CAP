
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
    public interface IMoneyView_UI : IView_UI {
        // Your logic here

        internal void UpdateUI(string goldText);
    }

    public class MoneyView_UI : View_UI<MoneyPresenter_UI, MoneyModel_UI> , IMoneyView_UI {
        protected override void CreatePresenter() {
            _presenter = new MoneyPresenter_UI();
            _presenter.Init(this);  
        }



        // Your logic here
        #region private
        [SerializeField] private TextMeshProUGUI _moneyText;

        private void Awake() {
#if UNITY_EDITOR
            Assert.IsNotNull(_moneyText);
#endif
        }
        #endregion

        #region public
        public void SetMoney(long money) {
            _presenter.SetMoney(money);
        }
        #endregion

        #region internal
        void IMoneyView_UI.UpdateUI(string moneyText) {
            _moneyText.text = moneyText;
        }
        #endregion
    }
}
