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
        private bool _isInit = false; // �ʱ�ȭ ����
        public bool IsInit { get { return _isInit; } }
        public UserData userData;
        
        protected override void Awake() {
            base.Awake();
        }
        private void Start() {
            InitGame();
        }
        private async void InitGame() {



            /// User Data (Firebase) �о����
            FirebaseManager firebaseManager = FirebaseManager.Instance;

            // Firebase�� �ʱ�ȭ �ɶ� ���� ���
            while (!firebaseManager.IsInit) {
                await UniTask.WaitForEndOfFrame();
            }
            
            firebaseManager.ReadMoney(userData, (str ) => {
                if (string.IsNullOrEmpty(str)) { // null �ϰ�� ���� �ۼ�
                    userData.Money = 0;
                    firebaseManager.WriteMoney(userData, null);
                } else {
                    userData.Money = Convert.ToInt64(str); // userdata money �о����
                }
            });

            // �ʱ�ȭ �Ϸ�
            _isInit = true;
        }
    }
}
