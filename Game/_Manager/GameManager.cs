using UnityEngine;
using CA.DesignPattern;
using Cysharp.Threading.Tasks;
using CA.Network;
using CA.Data;
using System;
namespace CA.Game
{
    public class GameManager : Singleton<GameManager>
    {
        private bool _isInit = false; // 초기화 여부
        public bool IsInit { get { return _isInit; } }
        public UserData userData;

        public SkillData attackData;
        public SkillData[] skillDatas = new SkillData[3];


        protected override void Awake() {
            base.Awake();
        }
        private void Start() {
            InitGame();
        }
        /// <summary>
        /// 게임을 초기화 하는 함수
        /// </summary>
        private async void InitGame() {
            /// User Data (Firebase) 읽어오기
            FirebaseManager firebaseManager = FirebaseManager.Instance;

            // Firebase가 초기화 될때 까지 대기
            while (!firebaseManager.IsInit) {
                await UniTask.WaitForEndOfFrame();
            }
            
            // Money Data를 읽어옴
            firebaseManager.ReadMoney(userData, (str ) => {
                if (string.IsNullOrEmpty(str)) { // null 일경우 새로 작성
                    userData.Money = 0;
                    firebaseManager.WriteMoney(userData, null);
                } else {
                    userData.Money = Convert.ToInt64(str); // userdata money 읽어오기
                }
            });

            // 초기화 완료
            _isInit = true;
        }
    }
}
