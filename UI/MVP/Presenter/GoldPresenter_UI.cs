
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
            int index = (int)Math.Max(0, Math.Log10(gold) / 3); // 1000 ������ index ���
            index = Math.Min(index, _model.units.Length - 1); // �迭 ���� �ʰ� ����
            double shortNum = gold / Math.Pow(1000, index); // �Ҽ������� ��ȯ
            string result = string.Format("{0:0.#}{1}", shortNum, _model.units[index]); ; // string���� ��ȯ

            _view.UpdateUI(result);
        }
        #endregion
    }
}
