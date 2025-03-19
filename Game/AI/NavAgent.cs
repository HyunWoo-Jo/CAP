
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
                transform.rotation = Quaternion.LookRotation(inputDir);  // �̵� ������ �ٶ󺸰� ��
            } else {
                _navAgent.velocity = Vector3.zero;  // Ű �Է� ������ ����
            }
        }


        /// <summary>
        /// Ray�� _targetLayer�� ���߽� ��ȿ�� ��ġ�� navAgent �̵�
        /// </summary>
        /// <param name="ray"></param>
        public void RaycastDestination(Ray ray) {
            if (!_naveType.Equals(NavType.Raycast)) InitNavType(NavType.Raycast);

            if (Physics.Raycast(ray, out RaycastHit hit, 1000, _targetLayer)) {
                // Ŭ���� ��ġ�� NavMesh ���� ��ȿ���� Ȯ��
                if (NavMesh.SamplePosition(hit.point, out NavMeshHit navHit, 1.0f, NavMesh.AllAreas)) {
                    _navAgent.SetDestination(navHit.position);
                }
            }
        }


        public bool HasReachedDesitnation() {
            if (_naveType.Equals(NavType.Raycast)) {
                if (!_navAgent.pathPending &&
                    _navAgent.remainingDistance <= _navAgent.stoppingDistance && // ���� �Ÿ��� stoppingDistance �������� Ȯ��
                    (!_navAgent.hasPath || _navAgent.velocity.sqrMagnitude == 0f)) // ��ΰ� ���ų� �ӵ��� 0�̸� ����
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
