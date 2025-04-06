using CA.Data;
using CA.UI;
using CA.Utills;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

namespace CA.Game
{
    /// <summary>
    /// Play Scene���� �Է� ������ �����ϴ� Ŭ����
    /// </summary>
    public class Controller : MonoBehaviour
    {
        [SerializeField] private ControllerView_UI _controllerView; // UI

        [ReadOnly] private Vector2 _inputDir = Vector3.zero; // �Է°�
        [ReadOnly] private Vector2 _firstClickPoint = Vector2.zero; // ó�� �Է� ��ġ
        private bool _isInput = false;
        private float _clampLength = 100f;
        internal Vector2 InputDir {  get { return _inputDir; } }

        private void Awake() {
#if UNITY_EDITOR
            Assert.IsNotNull(_controllerView);
#endif
        }
        private void Update() {
            if (Settings.isPause) return; // ���� ������ ��� ����
            // input ó��
            WorkInput();
            // ui ó��
            UpdateUI();
        }
        /// <summary>
        /// Input ó��
        /// </summary>
        private void WorkInput() {
#if UNITY_EDITOR
            // Test �� �ڵ�
            float screenMagnification = 70f;
            _inputDir.x = Input.GetAxisRaw("Horizontal") * screenMagnification;
            _inputDir.y = Input.GetAxisRaw("Vertical") * screenMagnification;
#endif

            if (Input.GetMouseButtonDown(0)) {
                
                if (EventSystem.current != null &&
                (EventSystem.current.IsPointerOverGameObject() || // raycast UI �� �Է� ����
                (Input.touchCount > 0 && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)))) { // ����� ȯ�� ��ġ
                    return;
                }
                _firstClickPoint = Input.mousePosition;
                _isInput = true;
            }
            if (Input.GetMouseButton(0) && _isInput) { 
                _inputDir = (Vector2)Input.mousePosition - _firstClickPoint; // �Է� ���� �޾ƿ�
                _inputDir = Vector2.ClampMagnitude(_inputDir, _clampLength); // �ε巯�� �̵��� ���� ���� ����
            }
            if (Input.GetMouseButtonUp(0)) { // �ʱ�ȭ
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
