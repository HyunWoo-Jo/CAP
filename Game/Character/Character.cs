using UnityEngine;
using UnityEngine.Assertions;
using CA.Data;

namespace CA.Game
{
    public class Character : MonoBehaviour
    {
        private NavAgent _navAgent;
        [SerializeField] private Animator _anim;

        private bool _isRun = false;
        private void Awake() {
            _navAgent = GetComponent<NavAgent>();
#if UNITY_EDITOR // Assertion
            Assert.IsNotNull( _navAgent);
            Assert.IsNotNull( _anim);
#endif
           
        }


        private void Update() {
            WorkAnimation();
        }

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
    }
}
