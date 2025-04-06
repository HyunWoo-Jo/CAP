using System;
using UnityEngine;

namespace CA.Data
{
    [CreateAssetMenu(fileName = "UserData", menuName = "Scriptable Objects/UserData")]
    public class UserData : ScriptableObject 
    {
        private long _money;
        private Action<long> _updateAction;
        public long Money { 
            get { return _money; } 
            set { 
                _money = value;
                _updateAction?.Invoke(_money);  
            } 
        }

        public void AddUpdateAction(Action<long> updateAction) {
            _updateAction += updateAction;
        }

        public void RemoveUpdateAction(Action<long> updateAction) {
            _updateAction -= updateAction;
        }
        
            
    }
}
