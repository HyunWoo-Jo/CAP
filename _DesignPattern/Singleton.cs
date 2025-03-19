using UnityEngine;

namespace CA.DesignPattern {
    [DefaultExecutionOrder(-100)] 
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        public static T Instance { get { 
                if(_instance == null) {
                    var obj = new GameObject {
                        isStatic = true,
                        name = typeof(T).Name,
                    };
                    var t = obj.AddComponent<T>();
                    return t;
                }
                return _instance; } }

        protected virtual void Awake() {
            if(_instance == null) {
                _instance = this.GetComponent<T>();
                DontDestroyOnLoad(this);
            } else {
                Destroy(this.gameObject);
            }
        }

    }
}
