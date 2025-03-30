using UnityEngine;
using UnityEngine.Assertions;
using CA.Data;
using CA.Utills;

namespace CA.Game
{
    public class Character : MonoBehaviour
    {
        private NavAgent _navAgent;
        [SerializeField] private Animator _anim;
        [SerializeField] private Controller _controller;
        private bool _isRun = false;
        private void Awake() {
            _navAgent = GetComponent<NavAgent>();
#if UNITY_EDITOR // Assertion
            Assert.IsNotNull( _navAgent);
            Assert.IsNotNull( _anim);
            Assert.IsNotNull( _controller );
#endif
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
            if (_navAgent.HasReachedDesitnation()) {
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
            _navAgent.MoveDirection(inputDir);
        }
    }
}
