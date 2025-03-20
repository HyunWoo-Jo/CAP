using UnityEngine;

namespace CA.UI
{
    public interface IModel_UI {}
    public interface IView_UI {
        public void Close();
    }
    public interface IPresenter_UI {
        public IPresenter_UI Init(IView_UI view);
        }
}
