using CA.Data;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace CA.UI
{
    public class WipeUI : MonoBehaviour, IView_UI
    {
        public enum Direction {
            Right,
            Left,
            FillLeft, // 채워져있을때 0 방향으로 <-
            FillRight, // ->
        }

        [SerializeField] private Image _wipePanel;
        private Direction _direction = Direction.Left;
        private float _targetTime = 0.5f;
        private float _time = 0;
        private int _progressID = Shader.PropertyToID("_Progress");
        private void Awake() {
#if UNITY_EDITOR
            Assert.IsNotNull(_wipePanel);
#endif
        }

        private void OnDisable() {
            Dispose();
        }

        public void Wipe(Direction direction, float targetTime, bool isAutoClose) {
            _direction = direction;
            _targetTime = targetTime;
            StartCoroutine(CoroutineWipe(isAutoClose));
        }

        private IEnumerator CoroutineWipe(bool isAutoClose) {
            float start = 0f;
            float end = 0f;
            if (_direction == Direction.Right) {
                end = -1;
            } else if (_direction == Direction.Left) {
                end = 1;
            } else if (_direction == Direction.FillLeft) {
                start = -1;
                end = 0;
            } else if (_direction == Direction.FillRight) {
                start = 1;
                end = 0;
            }
            SetMatPro(start);
            while (true) {
                _time += Time.deltaTime;
                // wipe 값 
                float wipe = Mathf.Lerp(start, end, Mathf.Clamp(_time / _targetTime, 0, 1));
                SetMatPro(wipe);
                if (_time >= _targetTime) { break; }
                yield return null;
            }
            if (isAutoClose) {
                gameObject.SetActive(false);
            }
        }
        private void SetMatPro(float value) {
            _wipePanel.material.SetFloat(_progressID, value);
        }
       

        public void Close() {
            Destroy(this.gameObject);
        }

        private void Dispose() {
            UIManager.Instance.CloseUI<WipeUI>(this.gameObject);
        }
    }
}
