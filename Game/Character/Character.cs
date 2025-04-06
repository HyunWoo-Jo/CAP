using UnityEngine;
using UnityEngine.Assertions;
using CA.Data;
using CA.Utills;

namespace CA.Game
{
    public class Character : MonoBehaviour
    {
        private IMover _mover;
        [SerializeField] private Animator _anim;
        [SerializeField] private Controller _controller;
        [SerializeField] private AttackInput _attackInput;
        private bool _isRun = false;
        private void Awake() {
            _mover = GetComponent<IMover>();
#if UNITY_EDITOR // Assertion
            Assert.IsNotNull(_mover);
            Assert.IsNotNull( _anim);
            Assert.IsNotNull( _controller );
            Assert.IsNotNull(_attackInput);
#endif
            // Attack Init
            _attackInput.AddAttackAction(Attack);
            _attackInput.AddSkillAction(AttackInput.SkillSlot.Slot0, Skill0);
            _attackInput.AddSkillAction(AttackInput.SkillSlot.Slot1, Skill1);
            _attackInput.AddSkillAction(AttackInput.SkillSlot.Slot2, Skill2);
        }

        public void OnDestroy() {
            _attackInput.RemoveAttackAction(Attack);
            _attackInput.RemoveSkillAction(AttackInput.SkillSlot.Slot0, Skill0);
            _attackInput.RemoveSkillAction(AttackInput.SkillSlot.Slot1, Skill1);
            _attackInput.RemoveSkillAction(AttackInput.SkillSlot.Slot2, Skill2);
        }


        private void Update() {
            if (Settings.isPause) return; // ���� ���� �� ��� ����

            // Animation ó��
            WorkAnimation();

            // �̵� ó��
            // vector2 to vector3  // Vector3(inputDir.x, 0, inputDir.y);
            Vector3 dir = _controller.InputDir.normalized; // controller��  �Է����� 
            dir.z = dir.y;
            dir.y = 0;
            WorkMove(dir);
        }

        /// <summary>
        /// Animation ó��
        /// </summary>
        private void WorkAnimation() {
            if (_mover.HasReachedDesitnation()) {
                if (_isRun) {
                    _isRun = false;
                    _anim.SetBool(AnimationKey.IsRun, false);
                }
            } else {
                if (!_isRun) {
                    _isRun = true;
                    _anim.SetBool(AnimationKey.IsRun, true);
                }
            }
        }
        /// <summary>
        /// input �̵� ó��
        /// </summary>
        private void WorkMove(Vector3 inputDir) {
            _mover.MoveDirection(inputDir);
        }

        private void Attack() {

        }

        private void Skill0() {

        }
        private void Skill1() {

        }

        private void Skill2() {

        }
    }
}
