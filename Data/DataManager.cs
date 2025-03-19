using UnityEngine;
using CA.DesignPattern;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.AddressableAssets;
using Cysharp.Threading.Tasks;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Linq;
namespace N.Data
{
    public class DataManager : Singleton<DataManager> 
    {
        private Dictionary<string, object> _data_dic = new();
        private Dictionary<string, int> _dataCount_dic = new();
        private Dictionary<string, AsyncOperationHandle> _handle_dic = new();
  
        #region Addressable
        public T LoadAssetSync<T>(string key) where T : Object{
            T result = null;
            if (!_data_dic.TryGetValue(key, out object obj)) {
                AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(key);
                handle.WaitForCompletion();
                result = handle.Result;         
                _data_dic.Add(key, result);
                _dataCount_dic.Add(key, 1);
                _handle_dic.Add(key, handle);
            }
            if (result == null) {
                _dataCount_dic[key]++;
                return (T)obj;
            } else {
                return result;
            }
        }

        /// <summary>
        /// ªË¡¶
        /// </summary>
        /// <param name="key"></param>
        public void ReleaseAsset(string key) {
            if(_dataCount_dic.TryGetValue(key, out int count)) {
                _dataCount_dic[key]--;
                if(--count <= 0 && _handle_dic.TryGetValue(key, out var handle)) {
                    _data_dic.Remove(key);
                    _dataCount_dic.Remove(key);
                    _handle_dic.Remove(key);
                    Addressables.Release(handle);
                }
            }
        }

        public void ReleaseAssetAll() {
            foreach (var keyValue in _handle_dic) {
                Addressables.Release(keyValue.Value);
            }
            _data_dic.Clear();
            _dataCount_dic.Clear();
            _handle_dic.Clear();
        }
        #endregion
    }
}
