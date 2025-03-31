
using UnityEngine;
using System.Runtime.CompilerServices;
using UnityEngine.EventSystems;
using CA.Utills;
using CA.Data;
using UnityEngine.SceneManagement;
////////////////////////////////////////////////////////////////////////////////////
// Auto Generated Code
#if UNITY_EDITOR
[assembly: InternalsVisibleTo("CA.Test")]
#endif
namespace CA.UI {

    public class MainLobbyPresenter_UI : Presenter_UI<MainLobbyModel_UI, IMainLobbyView_UI> {
        // Your logic here
        #region private
        /// <summary>
        /// Main Panel의 위치를 계산함
        /// </summary>
        /// <param name="panelType"></param>
        /// <returns></returns>
        private int GetMainPanelXPos(MainLobbyModel_UI.PanelType panelType) {
            return (int)panelType * 2560; // 2560 == canvas resolution x
        }

        /// <summary>
        /// 버튼 기본 기능
        /// </summary>
        private void StandardButtionFuntion(EventTrigger trigger, MainLobbyModel_UI.PanelType panelType) {
            // 스킵
            if (_model.panelType == panelType) {
                return;
            }
            //animation
            _view.AnimatedButton(_model.preButtonRectTr, false);
            _view.AnimatedButton(trigger.GetComponent<RectTransform>(), true);

            // 할당
            _model.panelType = panelType;
            _model.preButtonRectTr = trigger.GetComponent<RectTransform>();

            // Panel 위치 변경
            _view.UpdateMainPanelPos(GetMainPanelXPos(_model.panelType));
        }
        #endregion
        #region internal
        internal void WorldPanelInit(EventTrigger playButton) {
            // Play 버튼 초기화
            playButton.AddEventButton(EventTriggerType.PointerDown, () => {
                // Scene 이동 Main -> Play Scene
                SceneData.preScene = SceneData.SceneName.MainLobbyScene;
                SceneData.nextScene = SceneData.SceneName.PlayScene;
                // Loading Scene으로 이동
                SceneLoader.Instance.LoadAsync(SceneData.SceneName.LoadScene, false, 0.5f);

                // Effect
                var wipeUI = UIManager.Instance.InstantiateUI<WipeUI>(100); // 최상단의 생성
                wipeUI.Wipe(WipeUI.Direction.Left, 0.5f, false); // Left 방향으로 Effect 생성
                // 버튼 기능 삭제
                playButton.Clear();
            }, this.GetType().Name, nameof(WorldPanelInit));
        }

        internal void WorldButtonInit(EventTrigger worldButton) {
            // 초기 world로 셋팅
            _model.preButtonRectTr = worldButton.GetComponent<RectTransform>();
            _model.panelType = MainLobbyModel_UI.PanelType.World;

            worldButton.AddEventButton(EventTriggerType.PointerDown, () => {
                
                StandardButtionFuntion(worldButton, MainLobbyModel_UI.PanelType.World);

            }, this.GetType().Name, nameof(WorldButtonInit));
        }
        internal void SkillButtonInit(EventTrigger skillButton) {
            skillButton.AddEventButton(EventTriggerType.PointerDown, () => {

                StandardButtionFuntion(skillButton, MainLobbyModel_UI.PanelType.Skill);

            }, this.GetType().Name, nameof(SkillButtonInit));
        }
        internal void InventoryButtonInit(EventTrigger inventoryButton) {
            inventoryButton.AddEventButton(EventTriggerType.PointerDown, () => {

                StandardButtionFuntion(inventoryButton, MainLobbyModel_UI.PanelType.Inventory);

            }, this.GetType().Name, nameof(InventoryButtonInit));

           
        }

        #endregion
    }
}
