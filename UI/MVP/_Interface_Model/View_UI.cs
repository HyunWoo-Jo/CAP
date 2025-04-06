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
        /// UI Manager�� UI ��� / �̸� ������ View������ ���
        /// </summary>
        protected void AddUIManager() {
            UIManager.Instance.AddUIDic(this.GetType().Name, this.gameObject);
        }
        /// <summary>
        /// UI Manager�� UI ��� / �̸� ������ View ���� ���� ȣ��
        /// </summary>
        private void RemoveUIMnager() {
            UIManager.Instance.RemoveUIDic(this.GetType().Name);
        }

    }
}
