using UnityEngine;

namespace CA.Data
{
    /// <summary>
    /// 어떤 씬을 로드해야하는지 정보
    /// </summary>
    public static class SceneData {
        public enum SceneName {
            None,
            LoadScene,
            MainLobbyScene,
            PlayScene,
        }

        public static SceneName preScene; // 이전 씬
        public static SceneName nextScene; // 다음 씬
    }
}
