using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CA.Utills
{
    public static class EventTriggerUtills 
    {
       public static void AddEventButton(this EventTrigger eventTrigger, EventTrigger.Entry entry, string className, string methodName) {
            eventTrigger.triggers.Add(entry);
#if UNITY_EDITOR
            var pointChker =  eventTrigger.gameObject.GetComponent<EditorButtonEntryPointChker>();
            if(pointChker == null) pointChker = eventTrigger.gameObject.AddComponent<EditorButtonEntryPointChker>();
            pointChker.AddEntry(className, methodName, entry.eventID);
#endif
        }
        public static void AddEventButton(this EventTrigger eventTigger, EventTriggerType type, Action action,string className, string methodName) {
            EventTrigger.Entry entry = new();
            entry.eventID = type;
            entry.callback.AddListener((e) => { action(); });
            AddEventButton(eventTigger, entry, className, methodName);
        }

        public static void Clear(this EventTrigger eventTigger) {
            eventTigger.triggers.Clear();
#if UNITY_EDITOR
            eventTigger.GetComponent<EditorButtonEntryPointChker>().Dispose();
#endif
        }
    }
}
