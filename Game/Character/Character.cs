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

        private Skill _attack;
        private Skill[] _skills = new Skill[3];

        private bool _isRun = false;
        private void Awake() {
            _mover = GetComponent<IMover>();
#if UNITY_EDITOR // Assertion
            Assert.IsNotNull(_mover);
            Assert.IsNotNull( _anim);
            Assert.IsNotNull( _controller );
            Assert.IsNotNull(_attackInput);
#endif
            // Attack Skill 생성
            SkillData attackData = GameManager.Instance.attackData;
            SkillData[] skillDatas = GameManager.Instance.skillDatas;

            _attack = SkillFectory.Create(attackData, this);
           
            for (int i = 0; i < skillDatas.Length; i++) {
                if (skillDatas[i] == null) continue;
                Skill skill = SkillFectory.Create(skillDatas[i], this);
                _skills[i] = skill;
            }

            // Attack Init
            _attackInput.AddAttackAction(AttackInput.PointerType.Down, () => { _attack?.DownAction(); });
            _attackInput.AddAttackAction(AttackInput.PointerType.Up, () => { _attack?.UpAction(); });

            for (int i = 0; i < skillDatas.Length; i++) {
                int index = i;
                if (skillDatas[index] != null) {
                    _attackInput.AddSkillAction(AttackInput.SkillSlot.Slot0 + index, AttackInput.PointerType.Down, () => {
                        _skills[index]?.DownAction();
                    });
                    _attackInput.AddSkillAction(AttackInput.SkillSlot.Slot0 + index, AttackInput.PointerType.Up, () => {
                        _skills[index]?.UpAction();
                    });
                } else {
                    _attackInput.OffSkillButtonUI(AttackInput.SkillSlot.Slot0 + index);
                }
            }
        }

        public void OnDestroy() {
            // 버튼 초기화
            _attackInput.ClearAttackAction();
            for(int i = 0; i < _skills.Length; i++) {
                _attackInput.ClearSkillAction(AttackInput.SkillSlot.Slot0 + i);
            }
        }


        private void Update() {
            if (Settings.isPause) return; // 정지 상태 일 경우 리턴

            // Animation 처리
            WorkAnimation();

            // 이동 처리
            // vector2 to vector3  // Vector3(inputDir.x, 0, inputDir.y);
            Vector3 dir = _controller.InputDir.normalized; // controller의  입력정보 
            dir.z = dir.y;
            dir.y = 0;
            WorkMove(dir);
        }

        /// <summary>
        /// Animation 처리
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
        /// input 이동 처리
        /// </summary>
        private void WorkMove(Vector3 inputDir) {
            _mover.MoveDirection(inputDir);
        }

    }
}
