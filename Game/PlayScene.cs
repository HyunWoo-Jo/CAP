using CA.Data;
using CA.UI;
using UnityEngine;

namespace CA.Game
{
    public class PlayScene : MonoBehaviour
    {
        private void Awake() {
            // 초반 연출 Loading Scene을 거쳐왓을경우
            if (SceneData.isLoading) {
                WipeUI wipeUi = UIManager.Instance.InstantiateUI<WipeUI>(100);
                wipeUi.Wipe(WipeUI.Direction.FillRight, 0.5f, true);
            }
        }
    }
}
