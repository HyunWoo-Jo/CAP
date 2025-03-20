
using UnityEngine;
using System.Runtime.CompilerServices;
////////////////////////////////////////////////////////////////////////////////////
// Auto Generated Code
namespace CA.UI {
    public class LoadingSceneModel_UI : Model_UI {
        // Your logic here
        #region internal
        internal float startTime;
        internal const float minLoadTime = 2f; // 최소 로딩 시간

        internal bool fakeLoading = false; // 가짜 로딩
        internal float fakeProgress = 0f;
        internal float displayProgress = 0f;
        #endregion
    }
}
