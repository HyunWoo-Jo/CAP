using UnityEngine;
using Firebase.Auth;
using Firebase;
using Cysharp.Threading.Tasks;
using System;
using CA.Data;
namespace CA.Network
{
    /// <summary>
    /// Firebase Auth ����� �ϴ� Ŭ����
    /// </summary>
    public class FirebaseCAuth
    {
        private FirebaseAuth _auth;
        private FirebaseUser _user;
        
        internal string Uid {
            get { return _auth.CurrentUser.UserId; }
        }

        /// <summary>
        /// �ʱ�ȭ
        /// </summary>
        internal void Init() {
            _auth = FirebaseAuth.DefaultInstance;
        }
        // Guest �α���
        internal void GuestLogin(AuthData authData, Action susCallback, Action failCallback) {
            authData.LoadAuthData();
            if (authData.Uid.Length == 0) {
                string guestUid = SystemInfo.deviceUniqueIdentifier;
                authData.Uid = SystemInfo.deviceUniqueIdentifier;
                authData.Email = authData.Uid + "@guest.com";
                authData.Password = authData.Uid;
                authData.SaveAuthData();
            }
            string email = authData.Email;
            string password = authData.Password;
            // �α��� �õ�
            LoginUser(email, password, () => {
                // �α��� ���н�
                RegisterUser(email, password, failCallback, susCallback);

            }, susCallback);

        }
        /// <summary>
        /// ���
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="failCallback"></param>
        /// <param name="susCallback"></param>
        private async void RegisterUser(string email, string password, Action failCallback, Action susCallback) {
            await _auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
                if (task.IsCanceled || task.IsFaulted) {
                    //���� ����
                    failCallback?.Invoke();
                    return;
                }
                _user = task.Result.User;
                susCallback?.Invoke();
            });
        }

        /// <summary>
        /// �α���
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="failCallback"></param>
        /// <param name="susCallback"></param>
        private async void LoginUser(string email, string password, Action failCallback, Action susCallback) {
            await _auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
                if (task.IsCanceled || task.IsFaulted) {
                    // �α��� ����
                    failCallback?.Invoke();
                    return;
                }
                _user = task.Result.User;
                susCallback?.Invoke();
            });
        }

       
    }
}
