using CA.Data;
using CA.Network;
using CA.UI;
using UnityEngine;
using UnityEngine.Assertions;

namespace CA.Game
{
    public class MainLobbyScene : MonoBehaviour
    {
        [SerializeField] private MoneyView_UI _moneyView;
        private string backgroundClipKey = "MainBackgroundMusic.wav";
        private AudioClip backgroundClip;
        private void Awake() {
#if UNITY_EDITOR
            Assert.IsNotNull(_moneyView);
#endif
            UpdateMoneyUI(GameManager.Instance.userData.Money);
            // �ε� ���� ���Ŀ������
            if(SceneData.isLoading) {
                // �ʹ� ����
                WipeUI wipeUi = UIManager.Instance.InstantiateUI<WipeUI>(100);
                wipeUi.Wipe(WipeUI.Direction.FillRight, 0.5f, true);
            }

            // ��׶��� ���� ����
            backgroundClip = SoundManager.Instance.LoadSoundAsset(backgroundClipKey);
            SoundManager.Instance.PlayBackground(backgroundClip);
            

        }
        private void OnEnable() {
            Init();
        }
        private void OnDisable() {
            Dispose();
        }

        private void OnDestroy() {
            backgroundClip.UnloadAudioData();
            SoundManager.Instance.ReleseSoundAsset(backgroundClipKey);
        }

        private void Init() {
            // UI ���� ���
            GameManager.Instance.userData.AddUpdateAction(UpdateMoneyUI);
        }

        private void Dispose() {
            // UI ���� ����
            GameManager.Instance.userData.RemoveUpdateAction(UpdateMoneyUI);
        }
        // Money Update
        private void UpdateMoneyUI(long money) {
            _moneyView.SetMoney(money);
        }
    }
}
