
using UnityEngine;
using System.Runtime.CompilerServices;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.Assertions;
using System;
using CA.Utills;
////////////////////////////////////////////////////////////////////////////////////
// Auto Generated Code
#if UNITY_EDITOR
[assembly: InternalsVisibleTo("CA.Test")]
#endif
namespace CA.UI
{
    public interface IAttackView_UI : IView_UI {
        // Your logic here
        internal void ClickUIEffet(EventTrigger button);
    }

    public class AttackView_UI : View_UI<AttackPresenter_UI,AttackModel_UI> ,IAttackView_UI
    {
        protected override void CreatePresenter() {
            _presenter = new AttackPresenter_UI();
            _presenter.Init(this);

        }

        // Your logic here
        #region private
        [SerializeField] private EventTrigger _attackButton;
        [SerializeField] private List<SkillUI> _skillButtonList;

        private void Awake() {
#if UNITY_EDITOR
            Assert.IsNotNull(_attackButton);
            Assert.IsNotNull(_skillButtonList);
            Assert.IsTrue(_skillButtonList.Count == 3);
            for(int i = 0; i < 3; i++) {
                Assert.IsNotNull(_skillButtonList[i]);
            }
#endif  
            _presenter.InitButton(_attackButton, _skillButtonList);
        }


        #endregion

        #region public
        public enum ButtonType {
            Attack,
            Skill0,
            Skill1,
            Skill2,
        }
        /// <summary>
        /// 버튼에 추가 기능을 넣는 함수
        /// </summary>
        /// <param name="type"></param>
        /// <param name="action"></param>
        /// <param name="className"></param>
        /// <param name="methodName"></param>
        public void AddButtonAction(ButtonType type, Action downAction, Action upAction, string className, string methodName) {
            EventTrigger button = null;
            switch (type) {
                case ButtonType.Attack:
                    button = _attackButton;
                break;
                case ButtonType.Skill0:
                    button = _skillButtonList[0].Trigger;
                break;
                case ButtonType.Skill1:
                    button = _skillButtonList[1].Trigger;
                break;
                case ButtonType.Skill2:
                    button = _skillButtonList[2].Trigger;
                break;
            }
            _presenter.AddButtonFuntion(button, downAction, upAction, className, methodName);
        }

        public void ClearButton() {
            for(int i =0;i< _skillButtonList.Count; i++) {
                _skillButtonList[i].Trigger.Clear();
            }
        }

        /// <summary>
        /// 사용 안하는 Skill UI를 off 하는 함수
        /// </summary>
        /// <param name="index"></param>
        public void OffSkillButtonUI(int index) {
            _skillButtonList[index].gameObject.SetActive(false);
        }
        #endregion

        #region internal
        void IAttackView_UI.ClickUIEffet(EventTrigger button) {
            
        }
        #endregion
    }
}
