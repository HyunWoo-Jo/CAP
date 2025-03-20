
using UnityEngine;
using System.Runtime.CompilerServices;
using UnityEngine.EventSystems;
using UnityEngine.Assertions;
using DG.Tweening;
////////////////////////////////////////////////////////////////////////////////////
// Auto Generated Code
#if UNITY_EDITOR
[assembly: InternalsVisibleTo("CA.Test")]
#endif
namespace CA.UI
{
    public interface IMainLobbyView_UI : IView_UI {
        // Your logic here
        internal void UpdateMainPanelPos(int xPos);
        internal void AnimatedButton(RectTransform rect, bool isActive);
    }

    public class MainLobbyView_UI : View_UI<MainLobbyPresenter_UI,MainLobbyModel_UI> ,IMainLobbyView_UI
    {
        protected override void CreatePresenter() {
            _presenter = new MainLobbyPresenter_UI();
            _presenter.Init(this);  
        }

        // Your logic here
        #region private
        [Header("MainPanel")]
        [SerializeField] private RectTransform _mainCanvas;

        [Header("World")]
        [SerializeField] private EventTrigger _worldButton;
        [SerializeField] private EventTrigger _playButton;
        [Header("Skill")]
        [SerializeField] private EventTrigger _skillButton;
        [Header("Inventory")]
        [SerializeField] private EventTrigger _inventoryButton;

        private void Awake() {
#if UNITY_EDITOR
            Assert.IsNotNull(_mainCanvas);

            Assert.IsNotNull(_worldButton);
            Assert.IsNotNull(_playButton);

            Assert.IsNotNull(_skillButton);

            Assert.IsNotNull(_inventoryButton);
#endif
            // World Panel
            _presenter.WorldPanelInit(_playButton);

            // Button Button
            _presenter.WorldButtonInit(_worldButton);
            _presenter.SkillButtonInit(_skillButton);
            _presenter.InventoryButtonInit(_inventoryButton);
        }


        #endregion

        #region public

        #endregion

        #region internal
        /// <summary>
        /// Panel 위치 변경
        /// </summary>
        /// <param name="xPos"></param>
        void IMainLobbyView_UI.UpdateMainPanelPos(int xPos) {
            _mainCanvas.DOLocalMoveX(xPos, 0.2f);
        }

        /// <summary>
        /// 버튼 animation
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="isActive"></param>
        void IMainLobbyView_UI.AnimatedButton(RectTransform rect, bool isActive) {
            if (isActive) {
                rect.DOSizeDelta(new Vector2(500, 300), 0.2f).SetEase(Ease.InOutBack);
            } else {
                rect.DOSizeDelta(new Vector2(350, 200), 0.2f).SetEase(Ease.InOutBack);
            }
        }


        #endregion
    }
}
