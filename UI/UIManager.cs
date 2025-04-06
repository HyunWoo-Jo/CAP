using UnityEngine;
using CA.DesignPattern;
using CA.Data;
using System.Collections.Generic;
namespace CA.UI
{
    /// <summary>
    /// UI ���� �� ���� �ϴ� Ŭ����
    /// </summary>
    public class UIManager : Singleton<UIManager> {
        private Canvas _mainCanvas; // ���� �˹���
        private Dictionary<string, string> _key_dic = new(); // ui�� Addressable Key�� ���� ���ִ� 
        private Dictionary<string, GameObject> _view_dic = new(); // ������ view�� ����
        protected override void Awake() {
            base.Awake();
            if (_key_dic.Count == 0) AddKey();
        }
        /// <summary>
        /// Main Canvas �˻�
        /// </summary>
        private void FindMainCanvas() {
            GameObject mainCanvasObj = GameObject.Find("MainCanvas");
            _mainCanvas = mainCanvasObj.GetComponent<Canvas>();
        }

        /// <summary>
        /// UI ����
        /// </summary>
        /// <typeparam name="View"></typeparam>
        /// <param name="order"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public View InstantiateUI<View>(int order, bool isActive = true) where View : MonoBehaviour {
            if (_mainCanvas == null) FindMainCanvas();
            string typeName = typeof(View).Name;
            // Key �Ҵ�
            if (_key_dic.Count == 0) AddKey();
            string key = GetKey(typeName);
            if (!string.IsNullOrEmpty(key)) { // key�� ������ ���� �Ҵ�
                View view = InstanceUI<View>(key, isActive, order);
                return view;
            }
            return null;
        }
        /// <summary>
        /// UI ����
        /// </summary>
        /// <typeparam name="View"></typeparam>
        /// <param name="obj"></param>
        public void CloseUI<View>(GameObject obj) {
            string key = typeof(View).Name;
            if (_view_dic.ContainsKey(key)) {
                _view_dic[key].GetComponent<IView_UI>().Close();
                _view_dic.Remove(key);
            }
        }
        /// <summary>
        /// Dic�� UI ���
        /// </summary>
        /// <typeparam name="View"></typeparam>
        /// <param name="obj"></param>
        public void AddUIDic(string className,GameObject obj) {
            _view_dic.Add(className, obj);
        }
        /// <summary>
        /// Dic�� UI ����
        /// </summary>
        /// <param name="className"></param>
        public void RemoveUIDic(string className) {
            _view_dic.Remove(className);
        }

        public GameObject GetUIObject<View>(){
            if (_view_dic.TryGetValue(typeof(View).Name, out var obj)) return obj;
            return null;
        }

        private View InstanceUI<View>(string key, bool isAtive, int order) {
            GameObject prefab = DataManager.Instance.LoadAssetSync<GameObject>(key);
            GameObject uiObj = Instantiate(prefab);
            uiObj.transform.SetParent(_mainCanvas.transform);
            uiObj.transform.localPosition = Vector3.zero;
            uiObj.transform.localScale = Vector3.one;
            uiObj.gameObject.SetActive(isAtive);
            // ���
            AddUIDic(typeof(View).Name, uiObj);
            // anchor ����
            RectTransform rt = uiObj.GetComponent<RectTransform>();
            rt.anchorMin = new Vector2(0, 0);
            rt.anchorMax = new Vector2(1, 1);
            rt.pivot = new Vector2(0.5f, 0.5f);
            rt.offsetMin = new Vector2(0, 0);
            rt.offsetMax = new Vector2(1, 1);
            
            if (uiObj.TryGetComponent<Canvas>(out var canvas)) {
                canvas.overrideSorting = true;
                canvas.sortingOrder = order;
            }
            return uiObj.GetComponent<View>();
        }


        private string GetKey(string typeName) {
            _key_dic.TryGetValue(typeName, out string key);
            return key;
        }


        // UI Key ���
        private void AddKey() {
            _key_dic.Add(nameof(WipeUI), "Wipe_UI.prefab");
            _key_dic.Add(nameof(PauseView_UI), "Pause_UI.prefab");
        }
    }
}
