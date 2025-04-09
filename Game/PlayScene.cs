using CA.Data;
using CA.UI;
using UnityEngine;

namespace CA.Game
{
    public class PlayScene : MonoBehaviour {
        private string backgroundClipKey = "PlayBackgroundMusic.wav";
        private AudioClip backgroundClip;
        private void Awake() {
            // �ʹ� ���� Loading Scene�� ���Ŀ������
            if (SceneData.isLoading) {
                WipeUI wipeUi = UIManager.Instance.InstantiateUI<WipeUI>(100);
                wipeUi.Wipe(WipeUI.Direction.FillRight, 0.5f, true);
            }

            //���� ����
            backgroundClip = SoundManager.Instance.LoadSoundAsset(backgroundClipKey);
            SoundManager.Instance.PlayBackground(backgroundClip);
        }

        private void OnDestroy() {
            backgroundClip.UnloadAudioData();
            SoundManager.Instance.ReleseSoundAsset(backgroundClipKey);
        }
    }
}
