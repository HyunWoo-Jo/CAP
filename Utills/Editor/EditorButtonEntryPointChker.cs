using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
namespace CA.Utills {
#if UNITY_EDITOR
    /// <summary>
    /// 버튼 진입점을 Inspector에서 확인하기 위한 클래스
    /// </summary>
    public class EditorButtonEntryPointChker : MonoBehaviour
    {
        [ReadOnly][SerializeField] private List<EntryPointName> _entryPointName_list = new();

        [Serializable]
        public struct EntryPointName {
            public EventTriggerType triggerType;
            public string className;
            public string methodName;
        }

        public void AddEntry(string className, string methodName, EventTriggerType type) {

            _entryPointName_list.Add(new EntryPointName{triggerType = type, className = className, methodName = methodName });
        }

        public void Dispose() {
            Destroy(this);
        }

    }

#endif
}
