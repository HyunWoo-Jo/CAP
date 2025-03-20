
using UnityEngine;
using System.Runtime.CompilerServices;
using System;
////////////////////////////////////////////////////////////////////////////////////
// Auto Generated Code
#if UNITY_EDITOR
[assembly: InternalsVisibleTo("CA.Test")]
#endif
namespace CA.UI {

    public class GoldPresenter_UI : Presenter_UI<GoldModel_UI, IGoldView_UI> {
        // Your logic here
        #region internal
        internal void SetGold(int gold) {
            int index = (int)Math.Max(0, Math.Log10(gold) / 3); // 1000 단위로 index 계산
            index = Math.Min(index, _model.units.Length - 1); // 배열 범위 초과 방지
            double shortNum = gold / Math.Pow(1000, index); // 소수점으로 변환
            string result = string.Format("{0:0.#}{1}", shortNum, _model.units[index]); ; // string으로 변환

            _view.UpdateUI(result);
        }
        #endregion
    }
}
