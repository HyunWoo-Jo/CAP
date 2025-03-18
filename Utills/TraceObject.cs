using UnityEngine;
using System;
namespace CA.Game
{
    public class TraceObject : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _lerpSpeed;
        [SerializeField] private float _distance;
        private Vector3 _offset;
        private void LateUpdate() {
            if(Math.Abs(_distance) >= float.Epsilon) {
                _offset = this.transform.forward * _distance;
            }

            this.transform.position = Vector3.Lerp(this.transform.position, _target.transform.position - _offset, _lerpSpeed * Time.deltaTime);
        }

    }
}
