using UnityEngine;
using System;
using CA.UI;
using UnityEngine.Assertions;
namespace CA.Game
{
    public class AttackInput : MonoBehaviour
    {
        public enum SkillSlot {
            Slot0 = 0,
            Slot1 = 1,
            Slot2 = 2,
        }
        public enum PointerType {
            Down,
            Up,
        }

        [SerializeField] private AttackView_UI _attackView;
        private Action _attackDownAction;
        private Action _attackUpAction;
        private Action[] _skillDownAcitons = new Action[3];
        private Action[] _skillUpAcitons = new Action[3];
        
        public void AddAttackAction(PointerType pointerType, Action action) {
            if (pointerType == PointerType.Down) {
                _attackDownAction += action;
            } else if (pointerType == PointerType.Up) {
                _attackUpAction += action;
            }
        }
        public void ClearAttackAction() {
            _attackDownAction = null;
            _attackUpAction = null;
        }
        public void AddSkillAction(SkillSlot skillIndex, PointerType pointerType, Action action) {
            if (pointerType == PointerType.Down) {
                _skillDownAcitons[(int)skillIndex] += action;
            } else if (pointerType == PointerType.Up) {
                _skillUpAcitons[(int)skillIndex] += action;
            }
            
        }
        public void ClearSkillAction(SkillSlot skillIndex) {
            _skillDownAcitons[(int)skillIndex] = null;
            _skillUpAcitons[(int)skillIndex] = null;
        }
        public void OffSkillButtonUI(SkillSlot slot) {
            _attackView.OffSkillButtonUI((int)slot);
        }

        private void Awake() {
#if UNITY_EDITOR
            Assert.IsNotNull(_attackView);
#endif
            // Attack Button Init
            _attackView.AddButtonAction(AttackView_UI.ButtonType.Attack, 
                () => { _attackDownAction?.Invoke(); },
                () => { _attackUpAction?.Invoke(); }, 
                GetType().Name, "Attack");

            // Skill Button Init
            for(int i =0;i< _skillDownAcitons.Length; i++) {
                int index = i;
                _attackView.AddButtonAction(AttackView_UI.ButtonType.Skill0 + index,
                    () => { _skillDownAcitons[index]?.Invoke(); },
                    () => { _skillUpAcitons[index]?.Invoke(); },
                     GetType().Name,
                     "Awake"
                    );
            }
        }

        private void OnDestroy() {
            _attackView.ClearButton();
        }

       
    }
}
