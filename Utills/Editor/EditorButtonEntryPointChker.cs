using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
namespace CA.Utills {
#if UNITY_EDITOR
    /// <summary>
    /// ��ư �������� Inspector���� Ȯ���ϱ� ���� Ŭ����
    /// </summary>
    public class EditorButtonEntryPointChker : MonoBehaviour
    {
        [ReadOnlyAttribute][SerializeField] private List<EntryPointName> _entryPointName_list = new();

        [Serializable]
        public struct EntryPointName {
            public EventTriggerType triggerType;
            public string className;
            public string methodName;
        }

        public void AddEntry(string className, string methodName, EventTriggerType type) {

            _entryPointName_list.Add(new EntryPointName{triggerType = type, className = className, methodName = methodName });
        }

    }

#endif
}
