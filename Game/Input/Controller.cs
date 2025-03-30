using CA.Data;
using CA.UI;
using CA.Utills;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CA.Game
{
    /// <summary>
    /// Play Scene에서 입력 정보를 제어하는 클레스
    /// </summary>
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
            if (Settings.isPause) return; // 정지 상태일 경우 리턴
            // input 처리
            WorkInput();
            // ui 처리
            UpdateUI();
        }
        /// <summary>
        /// Input 처리
        /// </summary>
        private void WorkInput() {
            if (Input.GetMouseButtonDown(0)) {
                
                if (EventSystem.current != null &&
                (EventSystem.current.IsPointerOverGameObject() || // raycast UI 위 입력 금지
                (Input.touchCount > 0 && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)))) { // 모바일 환경 터치
                    return;
                }
                _firstClickPoint = Input.mousePosition;
                _isInput = true;
            }
            if (Input.GetMouseButton(0) && _isInput) { 
                _inputDir = (Vector2)Input.mousePosition - _firstClickPoint; // 입력 방향 받아옴
                _inputDir = Vector2.ClampMagnitude(_inputDir, _clampLength); // 부드러운 이동을 위한 제한 설정
            }
            if (Input.GetMouseButtonUp(0)) { // 초기화
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
