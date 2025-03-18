using UnityEngine;
using UnityEngine.AI;
using static Codice.Client.Common.WebApi.WebApiEndpoints;

namespace CA.Game
{
    public class NavAgent : MonoBehaviour
    {
        private NavMeshAgent _navAgent;
        [SerializeField] private LayerMask _targetLayer;
        private void Awake() {
            _navAgent = GetComponent<NavMeshAgent>();
        }

        private void Update() {
            if (Input.GetMouseButton(0)) {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                Debug.DrawRay(ray.origin, ray.direction * 1000f);

                if (Physics.Raycast(ray, out RaycastHit hit, 1000, _targetLayer)) {
                    // 클릭된 위치가 NavMesh 위에 유효한지 확인
                    if (NavMesh.SamplePosition(hit.point, out NavMeshHit navHit, 1.0f, NavMesh.AllAreas)) {
                        _navAgent.SetDestination(navHit.position);
                    }
                }
            }
        }

        public bool HasReachedDesitnation() {
            if (!_navAgent.pathPending &&
                _navAgent.remainingDistance <= _navAgent.stoppingDistance && // 남은 거리가 stoppingDistance 이하인지 확인
                (!_navAgent.hasPath || _navAgent.velocity.sqrMagnitude == 0f)) // 경로가 없거나 속도가 0이면 도착
                return true;
            return false;
        }
        
    }
}
