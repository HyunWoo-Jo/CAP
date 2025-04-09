using UnityEngine;

namespace CA.Data
{
    public enum SkillTargetingType {
        Self,          // 자신 대상
        SingleTarget,  // 단일 대상 (적 또는 아군)
        GroundTargeted,// 지점 대상 (땅 클릭)
        AreaAroundCaster, // 시전자 중심 범위
        Cone,          // 부채꼴 범위 (시전자 전방)
        Projectile     // 투사체
    }

    [CreateAssetMenu(fileName = "New Skill", menuName = "Scriptable Objects/Skill")]
    public class SkillData : ScriptableObject
    {
        [Header("Data")]
        public string skillName;
        public string descripton;
        public float cooldown; // 쿨타임
        public float castTime; // 케스트 시간 / 사용 안하면 0
        public float damage; // 일반 데미지
        public float fullCastDamage; // 최대 케스트 완료했을때 데미지

        public bool isTriggerSkillOnButtonDown; // true: 버튼 Down 시 스킬 실행 / false: 버튼 Up 시 스킬 실행

        [Header("Range")]
        public SkillTargetingType skillTargetingType; // 스킬 종류
        public float range; // 최대 사거리
        public float areaOfEffectRaduius; // 범위 반경
        public float coneAngle; // 부채꼴 범위

        [Header("Image / Effect")]
        public Sprite icon; // 아이콘
        public GameObject castVFX; // 케스트 도중 이펙트
        public GameObject failCastVFX; // 케스트 실패 이펙트
        public GameObject fullCastVFX; // 케스트 성공 이펙트
       

        [Header("Animation")]
        public AnimationClip animationClip;
        public string animationTriggerName;

        [Header("Audio")]
        public AudioClip castSFX; // 케스트 도중 사운드
        public AudioClip failCastSFX; // 케스트 실패 사운드
        public AudioClip fullCastSFX; // 케스트 성공 사운드

        [Header("Prefab")]
        public GameObject failCastPrefab; // 케스트 실패 오브젝트
        public GameObject fullCastPrefab; // 케스트 성공 오브젝트
    }
}
