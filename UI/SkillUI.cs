using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CA.UI
{
    public class SkillUI : MonoBehaviour
    {
        [SerializeField] private EventTrigger _trigger;
        [SerializeField] private TextMeshProUGUI _skilTimerText;
        [SerializeField] private Image _skilImage;

        internal EventTrigger Trigger { get { return _trigger; } }
        internal TextMeshProUGUI SkilTimerText { get { return _skilTimerText; } }
        internal Image SkillImage { get { return _skilImage; } }
       
        private void Awake() {
#if UNITY_EDITOR
            Assert.IsNotNull(_trigger);
            Assert.IsNotNull(_skilTimerText);
            Assert.IsNotNull(_skilImage);
#endif
        }

    }
}
