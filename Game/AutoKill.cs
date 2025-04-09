using CA.Data;
using UnityEngine;

namespace CA.Game
{
    public class AutoKill : MonoBehaviour
    {
        [SerializeField] private float _targetTime = 2f;
        private float _curTime = 0;
        private void Update() {
            if(Settings.isPause) return;
            _curTime += Time.deltaTime;
            if(_curTime >= _targetTime) {
                Destroy(gameObject);
            }
        }
    }
}
