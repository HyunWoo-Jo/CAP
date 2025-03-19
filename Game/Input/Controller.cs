using CA.UI;
using CA.Utills;
using N.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
namespace CA.Game
{
    public class Controller : MonoBehaviour
    {
        [ReadOnly] private ControllerView_UI _controllerView; // UI

        [ReadOnly] private Vector2 _inputDir = Vector3.zero; // 입력값
        [ReadOnly] private Vector2 _firstClickPoint = Vector2.zero; // 처음 입력 위치
        private bool _isInput = false;
        private float _clampLength = 100f;
        internal Vector2 InputDir {  get { return _inputDir; } }

        public void Start() {
            // UI 가지고 오기
            var cntlObj = UIManager.Instance.GetUIObject<ControllerView_UI>();
            _controllerView = cntlObj.GetComponent<ControllerView_UI>();
        }
        private void Update() {
            WorkInput();
            UpdateUI();
        }
        /// <summary>
        /// Input 처리
        /// </summary>
        private void WorkInput() {
            if (Input.GetMouseButtonDown(0)) {
                
                if (EventSystem.current != null &&
                (EventSystem.current.IsPointerOverGameObject() || // UI 위 입력 금지
                (Input.touchCount > 0 && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)))) { // 모바일 환경 터치
                    return;
                }
                _firstClickPoint = Input.mousePosition;
                _isInput = true;
            }
            if (Input.GetMouseButton(0) && _isInput) {
                _inputDir = (Vector2)Input.mousePosition - _firstClickPoint;
                _inputDir = Vector2.ClampMagnitude(_inputDir, _clampLength);
            }
            if (Input.GetMouseButtonUp(0)) {
                _inputDir = Vector2.zero;
                _isInput = false;
            }
        }
        /// <summary>
        /// UI 처리
        /// </summary>
        private void UpdateUI() {
            _controllerView.UpdateDirection(_inputDir);
        }
    }
}
