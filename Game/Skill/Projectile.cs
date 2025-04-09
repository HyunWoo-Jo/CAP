using CA.Data;
using UnityEngine;

namespace CA.Game
{
    public enum Owner {
        Player,
        Enemy,
    }
    public class Projectile : MonoBehaviour
    {
        private float _range;
        private Vector3 _direction;
        private Vector3 _spawnPosition;
        public float speed;
        [SerializeField] private Owner _owner;

        [SerializeField] private GameObject _hitVFX;
        [SerializeField] private AudioClip _hitSFX;
        [SerializeField] private GameObject _disappearVFX;
        public void SetData(Vector3 spawnPosition, float range, Vector3 direction) {
            // data
            _spawnPosition = spawnPosition;
            _range = range;
            _direction = direction;

            // tr
            this.transform.position = spawnPosition;
            Quaternion lookRotation = Quaternion.LookRotation(direction.normalized, Vector3.up);
            this.transform.rotation = lookRotation;

           
        }

        private void OnDestroy() {
            Disappear();
        }

        void Update()
        {
            if(Settings.isPause) return;
            if ((_spawnPosition - transform.position).magnitude >= _range) Destroy(this.gameObject);
            // 이동
            transform.position = this.transform.position + (_direction * speed * Time.deltaTime);
            
        }

        private void Disappear() {
            // vfx 생성
            if (_disappearVFX != null) {
                GameObject vfx = GameObject.Instantiate(_disappearVFX);
                vfx.transform.position = this.transform.position;
            }
        }
    }
}
