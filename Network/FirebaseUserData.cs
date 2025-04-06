using CA.Data;
using Cysharp.Threading.Tasks;
using Firebase.Database;
using System;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace CA.Network
{
    public class FirebaseUserData
    {
        private DatabaseReference _databaseReference;

        internal void Init() {
            _databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        }

        internal async void ReadMoney(UserData userData, string uid, Action<string> susCallback, Action<Exception> failCallback) {
            try {
                var task = await _databaseReference.Child("UserData").Child(uid).Child("Money").GetValueAsync().AsUniTask();
                susCallback?.Invoke(task.GetRawJsonValue());
            } catch (Exception ex) {
                failCallback?.Invoke(ex);
            }
        }
        internal async void WriteMoney(UserData userData, string uid, Action susCallback, Action<Exception> failCallback) {
            DatabaseReference dataRef = _databaseReference.Child("UserData").Child(uid);// root 설정
            string jsonData = JsonConvert.SerializeObject(new { Money = userData.Money });

            try {
                // firebase 에 전송
                await dataRef.SetRawJsonValueAsync(jsonData).AsUniTask();
                // 성공
                susCallback?.Invoke();
            } catch (Exception ex) {
                // 실패
                failCallback?.Invoke(ex);
            }
        }
    }
}
