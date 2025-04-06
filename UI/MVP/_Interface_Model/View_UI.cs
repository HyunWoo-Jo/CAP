using CA.UI;
using System;
using UnityEngine;

namespace CA.UI
{
    public abstract class View_UI<Presenter, Model> : MonoBehaviour, IView_UI where Presenter : IPresenter_UI, new() where Model : IModel_UI
    {
        protected Presenter _presenter;

        public View_UI() {
            CreatePresenter();
        }

        public virtual void Close() {
            Destroy(this.gameObject);
        }

        protected abstract void CreatePresenter();

        protected  virtual void OnDisable() {
            RemoveUIMnager();
        }
        /// <summary>
        /// UI Manager에 UI 등록 / 미리 생성한 View에서만 사용
        /// </summary>
        protected void AddUIManager() {
            UIManager.Instance.AddUIDic(this.GetType().Name, this.gameObject);
        }
        /// <summary>
        /// UI Manager에 UI 등록 / 미리 생성한 View 에서 사용시 호출
        /// </summary>
        private void RemoveUIMnager() {
            UIManager.Instance.RemoveUIDic(this.GetType().Name);
        }

    }
}
