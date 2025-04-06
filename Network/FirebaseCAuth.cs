using UnityEngine;
using Firebase.Auth;
using Firebase;
using Cysharp.Threading.Tasks;
using System;
using CA.Data;
namespace CA.Network
{
    /// <summary>
    /// Firebase Auth 기능을 하는 클레스
    /// </summary>
    public class FirebaseCAuth
    {
        private FirebaseAuth _auth;
        private FirebaseUser _user;
        
        internal string Uid {
            get { return _auth.CurrentUser.UserId; }
        }

        /// <summary>
        /// 초기화
        /// </summary>
        internal void Init() {
            _auth = FirebaseAuth.DefaultInstance;
        }
        // Guest 로그인
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
            // 로그인 시도
            LoginUser(email, password, () => {
                // 로그인 실패시
                RegisterUser(email, password, failCallback, susCallback);

            }, susCallback);

        }
        /// <summary>
        /// 등록
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="failCallback"></param>
        /// <param name="susCallback"></param>
        private async void RegisterUser(string email, string password, Action failCallback, Action susCallback) {
            await _auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
                if (task.IsCanceled || task.IsFaulted) {
                    //생성 실패
                    failCallback?.Invoke();
                    return;
                }
                _user = task.Result.User;
                susCallback?.Invoke();
            });
        }

        /// <summary>
        /// 로그인
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="failCallback"></param>
        /// <param name="susCallback"></param>
        private async void LoginUser(string email, string password, Action failCallback, Action susCallback) {
            await _auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
                if (task.IsCanceled || task.IsFaulted) {
                    // 로그인 실패
                    failCallback?.Invoke();
                    return;
                }
                _user = task.Result.User;
                susCallback?.Invoke();
            });
        }

       
    }
}
