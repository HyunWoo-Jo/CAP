using UnityEngine;

namespace CA.Data
{
    public enum SkillTargetingType {
        Self,          // �ڽ� ���
        SingleTarget,  // ���� ��� (�� �Ǵ� �Ʊ�)
        GroundTargeted,// ���� ��� (�� Ŭ��)
        AreaAroundCaster, // ������ �߽� ����
        Cone,          // ��ä�� ���� (������ ����)
        Projectile     // ����ü
    }

    [CreateAssetMenu(fileName = "New Skill", menuName = "Scriptable Objects/Skill")]
    public class SkillData : ScriptableObject
    {
        [Header("Data")]
        public string skillName;
        public string descripton;
        public float cooldown; // ��Ÿ��
        public float castTime; // �ɽ�Ʈ �ð� / ��� ���ϸ� 0
        public float damage; // �Ϲ� ������
        public float fullCastDamage; // �ִ� �ɽ�Ʈ �Ϸ������� ������

        public bool isTriggerSkillOnButtonDown; // true: ��ư Down �� ��ų ���� / false: ��ư Up �� ��ų ����

        [Header("Range")]
        public SkillTargetingType skillTargetingType; // ��ų ����
        public float range; // �ִ� ��Ÿ�
        public float areaOfEffectRaduius; // ���� �ݰ�
        public float coneAngle; // ��ä�� ����

        [Header("Image / Effect")]
        public Sprite icon; // ������
        public GameObject castVFX; // �ɽ�Ʈ ���� ����Ʈ
        public GameObject failCastVFX; // �ɽ�Ʈ ���� ����Ʈ
        public GameObject fullCastVFX; // �ɽ�Ʈ ���� ����Ʈ
       

        [Header("Animation")]
        public AnimationClip animationClip;
        public string animationTriggerName;

        [Header("Audio")]
        public AudioClip castSFX; // �ɽ�Ʈ ���� ����
        public AudioClip failCastSFX; // �ɽ�Ʈ ���� ����
        public AudioClip fullCastSFX; // �ɽ�Ʈ ���� ����

        [Header("Prefab")]
        public GameObject failCastPrefab; // �ɽ�Ʈ ���� ������Ʈ
        public GameObject fullCastPrefab; // �ɽ�Ʈ ���� ������Ʈ
    }
}
