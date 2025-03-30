using UnityEngine;

namespace CA.Data
{
    /// <summary>
    /// � ���� �ε��ؾ��ϴ��� ����
    /// </summary>
    public static class SceneData {
        public enum SceneName {
            None,
            LoadScene,
            MainLobbyScene,
            PlayScene,
        }

        public static SceneName preScene; // ���� ��
        public static SceneName nextScene; // ���� ��
    }
}
