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

        [ReadOnly] private Vector2 _inputDir = Vector3.zero; // �Է°�
        [ReadOnly] private Vector2 _firstClickPoint = Vector2.zero; // ó�� �Է� ��ġ
        private bool _isInput = false;
        private float _clampLength = 100f;
        internal Vector2 InputDir {  get { return _inputDir; } }

        public void Start() {
            // UI ������ ����
            var cntlObj = UIManager.Instance.GetUIObject<ControllerView_UI>();
            _controllerView = cntlObj.GetComponent<ControllerView_UI>();
        }
        private void Update() {
            WorkInput();
            UpdateUI();
        }
        /// <summary>
        /// Input ó��
        /// </summary>
        private void WorkInput() {
            if (Input.GetMouseButtonDown(0)) {
                
                if (EventSystem.current != null &&
                (EventSystem.current.IsPointerOverGameObject() || // UI �� �Է� ����
                (Input.touchCount > 0 && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)))) { // ����� ȯ�� ��ġ
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
        /// UI ó��
        /// </summary>
        private void UpdateUI() {
            _controllerView.UpdateDirection(_inputDir);
        }
    }
}
