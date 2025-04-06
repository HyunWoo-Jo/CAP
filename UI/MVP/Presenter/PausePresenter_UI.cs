
using UnityEngine;
using System.Runtime.CompilerServices;
using UnityEngine.EventSystems;
using CA.Utills;
using CA.Data;
////////////////////////////////////////////////////////////////////////////////////
// Auto Generated Code
#if UNITY_EDITOR
[assembly: InternalsVisibleTo("CA.Test")]
#endif
namespace CA.UI {

    public class PausePresenter_UI : Presenter_UI<PauseModel_UI, IPauseView_UI> {
        // Your logic here
        #region internal
        internal void InitButton(EventTrigger giveUpButton, EventTrigger retryButton, EventTrigger exitButton) {
            giveUpButton.AddEventButton(EventTriggerType.PointerDown, () => {
                // Scene 이동 Main -> Play Scene
                SceneData.preScene = SceneData.SceneName.PlayScene;
                SceneData.nextScene = SceneData.SceneName.MainLobbyScene;
                // Loading Scene으로 이동
                SceneLoader.Instance.LoadAsync(SceneData.SceneName.LoadScene, false, 0.5f);
                // Effect
                var wipeUI = UIManager.Instance.InstantiateUI<WipeUI>(100); // 최상단의 생성
                wipeUI.Wipe(WipeUI.Direction.Left, 0.5f, false); // Left 방향으로 Effect 생성

            }, GetType().Name, nameof(InitButton));

            retryButton.AddEventButton(EventTriggerType.PointerDown, () => {
                // Loading Scene으로 이동
                SceneLoader.Instance.LoadAsync(SceneData.SceneName.LoadScene, false, 0.5f);

                // Effect
                var wipeUI = UIManager.Instance.InstantiateUI<WipeUI>(100); // 최상단의 생성
                wipeUI.Wipe(WipeUI.Direction.Left, 0.5f, false); // Left 방향으로 Effect 생성
            }, GetType().Name, nameof(InitButton));

            exitButton.AddEventButton(EventTriggerType.PointerDown, () => {
                UIManager.Instance.CloseUI<PauseView_UI>(UIManager.Instance.GetUIObject<PauseView_UI>());
            }, GetType().Name, nameof(InitButton));
        }
        #endregion
    }
}
