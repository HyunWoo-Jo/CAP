
using UnityEngine;
using UnityEngine.AI;
using CA.Utills;
namespace CA.Game
{
    public class NavAgent : MonoBehaviour {
        public enum NavType {
            None,
            Raycast,
            Direction
        }

        private NavMeshAgent _navAgent;
        [SerializeField] private LayerMask _targetLayer;
        [ReadOnly][SerializeField] private NavType _naveType = NavType.None;
      

        private void Awake() {
            _navAgent = GetComponent<NavMeshAgent>();
            
        }

        private void InitNavType(NavType navType) {
            if (navType == NavType.Direction) {
                _naveType = NavType.Direction;
                _navAgent.updateRotation = false;
                _navAgent.updatePosition = true;
            } else if (navType == NavType.Raycast) {
                _naveType = NavType.Raycast;
                _navAgent.updateRotation = true;
                _navAgent.updatePosition = true;
            }
        }

        public void MoveDirection(Vector3 direction) {
            if(!_naveType.Equals(NavType.Direction)) InitNavType(NavType.Direction);

            var inputDir = direction;

            if (inputDir.sqrMagnitude > 0) {
                _navAgent.velocity = inputDir * _navAgent.speed;
                transform.rotation = Quaternion.LookRotation(inputDir);  // 이동 방향을 바라보게 함
            } else {
                _navAgent.velocity = Vector3.zero;  // 키 입력 없으면 정지
            }
        }


        /// <summary>
        /// Ray가 _targetLayer에 적중시 유효한 위치면 navAgent 이동
        /// </summary>
        /// <param name="ray"></param>
        public void RaycastDestination(Ray ray) {
            if (!_naveType.Equals(NavType.Raycast)) InitNavType(NavType.Raycast);

            if (Physics.Raycast(ray, out RaycastHit hit, 1000, _targetLayer)) {
                // 클릭된 위치가 NavMesh 위에 유효한지 확인
                if (NavMesh.SamplePosition(hit.point, out NavMeshHit navHit, 1.0f, NavMesh.AllAreas)) {
                    _navAgent.SetDestination(navHit.position);
                }
            }
        }


        public bool HasReachedDesitnation() {
            if (_naveType.Equals(NavType.Raycast)) {
                if (!_navAgent.pathPending &&
                    _navAgent.remainingDistance <= _navAgent.stoppingDistance && // 남은 거리가 stoppingDistance 이하인지 확인
                    (!_navAgent.hasPath || _navAgent.velocity.sqrMagnitude == 0f)) // 경로가 없거나 속도가 0이면 도착
                    return true;
            } else if (_naveType.Equals(NavType.Direction)) {
                if (_navAgent.velocity.sqrMagnitude == 0f) 
                    return true;
            } else if (_naveType.Equals(NavType.None)) {
                return true;
            }
            return false;
        }
        
    }
}
