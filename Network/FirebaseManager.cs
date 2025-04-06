using UnityEngine;
using CA.DesignPattern;
using Firebase.Database;
using Cysharp.Threading.Tasks;
using System;
using Firebase.Auth;
using Firebase;
using CA.Data;
namespace CA.Network
{
    [DefaultExecutionOrder(-1000)]
    public class FirebaseManager : Singleton<FirebaseManager>
    {
        private FirebaseCAuth _firebaseAuth = new ();
        private FirebaseUserData _firebaseUserData = new();
        [SerializeField] private AuthData _firebaseAuthData;

        private bool _isInit = false;
        private bool _isLogin = false;
        public string UID { get { return _firebaseAuth.Uid; } }
        public bool IsInit { get { return _isInit; } }
        public bool IsLogin {  get { return _isLogin; } }


        protected override void Awake() {
            base.Awake();
            Init();
              
        }

        private async void Init() {
            var defState = await FirebaseApp.CheckAndFixDependenciesAsync();

            // Auth 초기화
            _firebaseAuth.Init();
            // userData 초기화
            _firebaseUserData.Init();

            _isInit = true;
        }

        public async void GuestLogin() {
            // 초기화 될때까지 대기
            await UniTask.RunOnThreadPool(async () => {
                while (true) {
                    if(_isInit) { return; }
                    await UniTask.WaitForEndOfFrame();
                }
            });
            // GuestLogin 시도
            _firebaseAuth.GuestLogin(_firebaseAuthData,
                // 로그인 성공 callback
                () => {
                _isLogin = true;
                }, 
                // 로그인 실패 callback
                () => {
                
                }
            );
        }
        
        public void ReadMoney(UserData userData, Action<string> susCallback) {
            _firebaseUserData.ReadMoney(userData, _firebaseAuth.Uid, susCallback, null);

        }
        public void WriteMoney(UserData userData, Action susCallback) {
            _firebaseUserData.WriteMoney(userData, _firebaseAuth.Uid, susCallback, null);
        }
        

    }
}
