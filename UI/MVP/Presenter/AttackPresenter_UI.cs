
using UnityEngine;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using CA.Utills;
using System;
////////////////////////////////////////////////////////////////////////////////////
// Auto Generated Code
#if UNITY_EDITOR
[assembly: InternalsVisibleTo("CA.Test")]
#endif
namespace CA.UI {

    public class AttackPresenter_UI : Presenter_UI<AttackModel_UI, IAttackView_UI> {
        // Your logic here
        #region internal
        internal void InitButton(EventTrigger attackButton, List<SkillUI> skillButtonList) {
            // �⺻ button feedback effect �Ҵ�
            attackButton.AddEventButton(EventTriggerType.PointerDown, () => {
                _view.ClickUIEffet(attackButton);
            }, GetType().Name, nameof(InitButton));
            foreach (var skillButton in skillButtonList) {
                skillButton.Trigger.AddEventButton(EventTriggerType.PointerDown, () => {
                    _view.ClickUIEffet(skillButton.Trigger);
                }, GetType().Name, nameof(InitButton));
            }
        }
        internal void AddButtonFuntion(EventTrigger button, Action downAction, Action upAction, string className, string methodName) {
            button.AddEventButton(EventTriggerType.PointerDown, downAction, className, methodName);
            button.AddEventButton(EventTriggerType.PointerUp, upAction, className, methodName);
        }
        #endregion
    }
}
