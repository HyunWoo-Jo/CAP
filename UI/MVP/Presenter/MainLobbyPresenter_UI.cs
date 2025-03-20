
using UnityEngine;
using System.Runtime.CompilerServices;
using UnityEngine.EventSystems;
using CA.Utills;
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
        /// Main Panel�� ��ġ�� �����
        /// </summary>
        /// <param name="panelType"></param>
        /// <returns></returns>
        private int GetMainPanelXPos(MainLobbyModel_UI.PanelType panelType) {
            return (int)panelType * 2560; // 2560 == canvas resolution x
        }

        /// <summary>
        /// ��ư �⺻ ���
        /// </summary>
        private void StandardButtionFuntion(EventTrigger trigger, MainLobbyModel_UI.PanelType panelType) {
            // ��ŵ
            if (_model.panelType == panelType) {
                return;
            }
            //animation
            _view.AnimatedButton(_model.preButtonRectTr, false);
            _view.AnimatedButton(trigger.GetComponent<RectTransform>(), true);

            // �Ҵ�
            _model.panelType = panelType;
            _model.preButtonRectTr = trigger.GetComponent<RectTransform>();

            // Panel ��ġ ����
            _view.UpdateMainPanelPos(GetMainPanelXPos(_model.panelType));
        }
        #endregion
        #region internal
        internal void WorldPanelInit(EventTrigger playButton) {

        }

        internal void WorldButtonInit(EventTrigger worldButton) {
            // �ʱ� world�� ����
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
