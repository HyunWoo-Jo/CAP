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

        [SerializeField] private AttackView_UI _attackView;
        private Action _attackAction;
        private Action[] _skillAciton = new Action[3];
        
        public void AddAttackAction(Action action) {
            _attackAction += action;
        }
        public void RemoveAttackAction(Action action) {
            _attackAction -= action;
        }
        public void AddSkillAction(SkillSlot skillIndex, Action action) {
            _skillAciton[(int)skillIndex] += action;
        }
        public void RemoveSkillAction(SkillSlot skillIndex, Action action) {
            _skillAciton[(int)skillIndex] -= action;
        }

        private void Awake() {
#if UNITY_EDITOR
            Assert.IsNotNull(_attackView);
#endif
            // Init Button
            _attackView.AddButtonAction(AttackView_UI.ButtonType.Attack, Attack, GetType().Name, nameof(Attack));
            _attackView.AddButtonAction(AttackView_UI.ButtonType.Skill0, Skill0, GetType().Name, nameof(Skill0));
            _attackView.AddButtonAction(AttackView_UI.ButtonType.Skill1, Skill1, GetType().Name, nameof(Skill1));
            _attackView.AddButtonAction(AttackView_UI.ButtonType.Skill2, Skill2, GetType().Name, nameof(Skill2));
        }
        
        private void Attack() {
            _attackAction?.Invoke();
        }

        private void Skill0() {
            _skillAciton[0]?.Invoke();
        }

        private void Skill1() {
            _skillAciton[1]?.Invoke();
        }

        private void Skill2() {
            _skillAciton[2]?.Invoke();
        }

       
    }
}
