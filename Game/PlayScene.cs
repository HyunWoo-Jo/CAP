using CA.UI;
using UnityEngine;

namespace CA.Game
{
    public class PlayScene : MonoBehaviour
    {
        private void Awake() {
            WipeUI wipeUi = UIManager.Instance.InstantiateUI<WipeUI>(100);
            wipeUi.Wipe(WipeUI.Direction.FillRight, 0.5f, true);
        }
    }
}
