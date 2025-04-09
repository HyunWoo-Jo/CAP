using CA.Data;
using CA.Utills;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CA.Game
{
    public abstract class Skill {
        protected SkillData data;
        protected Character character;

        protected float remainingCooldown = 0; // 남은 쿨타임
        protected float castTime = 0;// 현재 케스트 타임
        protected bool isCasting = false;

        protected Coroutine clickCoroutine = null; // 클릭 되었을때 발동되는 코루틴
        protected bool isClick = false; // 클릭 여부

        private Coroutine updateCooldownTimerCoroutine = null;
        public void SetData(SkillData data, Character character) {
            this.data = data;
            this.character = character;
        }
        public void StartCooldownTimer() {
            updateCooldownTimerCoroutine = character.StartCoroutine(UpdateCoolTime());
        }
        public void Dispose() {
            if (updateCooldownTimerCoroutine != null) {
                character.StopCoroutine(updateCooldownTimerCoroutine);
            }
            if(clickCoroutine != null) {
                character.StopCoroutine(clickCoroutine);
            }
        }

        #region button

        /// <summary>
        /// Button Down
        /// </summary>
        public virtual void DownAction() {
            isClick = true; // 클릭했을경우
            if(clickCoroutine != null) { // 예외 처리
                character.StopCoroutine(clickCoroutine);
                clickCoroutine = null;
            }
            // Click Coroutine 실행
            clickCoroutine = character.StartCoroutine(ClickCoroutine());
        }
        /// <summary>
        /// Button Up
        /// </summary>
        public virtual void UpAction() {
            if (!data.isTriggerSkillOnButtonDown) OnCast();
            // 초기화
            isClick = false;
            castTime = 0;
            character.StopCoroutine(clickCoroutine);
            clickCoroutine = null;
        }
        #endregion
        #region Update

        // CastTime 갱신 ClickCoroutine 에서 호출
        private void UpdateCastTime() {
            if (Settings.isPause) return;
            if (castTime < data.castTime) castTime += Time.deltaTime;
        }

        // 쿨타임 시간 갱신 StartCooldownTimer에서 호출 생성될때 호출
        private IEnumerator UpdateCoolTime() {
            while (true) {
                if (!Settings.isPause && remainingCooldown > 0) {
                    remainingCooldown -= Time.deltaTime;
                }
                yield return null;
            }
        }

        // 클릭 상태일때 발생되는 코루틴 DownAction에서 호출
        private IEnumerator ClickCoroutine() {
            while (isClick) {
                if (IsOnCooldown()) { // 사용 가능하면
                    UpdateCastTime();// 케스팅 타임 갱신
                    if (data.isTriggerSkillOnButtonDown && IsOnCast()) OnCast(); // trigger가 true에 Cast가 완료 상태면 down 상태에서 click 호출
                }
                yield return null;
            }
        }

        /// <summary>
        /// 케스팅 완료후 호출
        /// </summary>
        private void OnCast() {     
            if (IsOnCast()) {
                DoneFullCast();
            } else {
                DoneFailCast();
            }
            // 남은 시간 셋팅
            remainingCooldown = data.cooldown;
            castTime = 0;
        }
        /// <summary>
        /// 케스트 완료후 호출 / 스킬 내용 구현부분
        /// </summary>
        protected abstract void DoneFullCast();

        /// <summary>
        /// 케스트 실패시 호출
        /// </summary>
        protected abstract void DoneFailCast();

        #endregion

        /// <summary>
        /// 스킬 기본 시작위치
        /// </summary>
        /// <returns></returns>
        protected Vector3 GetSkillOffset() {
            return character.transform.position + new Vector3(0, 1, 0);
        }

        /// <summary>
        /// 남은 쿨타임 확인
        /// </summary>
        /// <returns></returns>
        public float GetRemainingCoolTime() {
            return remainingCooldown;
        }
        /// <summary>
        /// 쿨타임 중인지 판별
        /// </summary>
        /// <returns></returns>
        protected bool IsOnCooldown() {
            return remainingCooldown <= 0f;
        }

        /// <summary>
        /// 현재 케스팅 시간 반환
        /// </summary>
        /// <returns></returns>
        public float GetCastTime() {
            return castTime;
        }

        /// <summary>
        /// 케스팅이 완료 됬는지 판별
        /// </summary>
        /// <returns></returns>
        protected bool IsOnCast() {
            return castTime >= data.castTime;
        }


    }
    public class SkillFectory {
        public static Skill Create(SkillData data, Character character) {
            Skill skill = null;
            switch (data.skillTargetingType) {
                case SkillTargetingType.Self:
                    skill = new SelfSkill();
                break;
                case SkillTargetingType.SingleTarget:
                    skill = new SingleTargetSkill();
                break;
                case SkillTargetingType.GroundTargeted:
                    skill = new GroundTargetedSkill();
                break;
                case SkillTargetingType.AreaAroundCaster:
                    skill = new AreaAroundCasterSkill();
                break;
                case SkillTargetingType.Cone:
                    skill = new ConeSkill();
                break;
                case SkillTargetingType.Projectile:
                    skill = new ProjectileSkill();
                break;
            }
            if (skill != null) {
                skill.SetData(data, character);
                skill.StartCooldownTimer();
            }
            return skill;
        }
        private class SelfSkill : Skill {
            public override void DownAction() {
                throw new System.NotImplementedException();
            }

            public override void UpAction() {
                throw new System.NotImplementedException();
            }

            protected override void DoneFailCast() {
                throw new System.NotImplementedException();
            }

            protected override void DoneFullCast() {
                throw new System.NotImplementedException();
            }
        }

        private class SingleTargetSkill : Skill {
            public override void DownAction() {
                throw new System.NotImplementedException();
            }

            public override void UpAction() {
                throw new System.NotImplementedException();
            }

            protected override void DoneFailCast() {
                throw new System.NotImplementedException();
            }

            protected override void DoneFullCast() {
                throw new System.NotImplementedException();
            }
        }

        private class GroundTargetedSkill : Skill {
            public override void DownAction() {
                throw new System.NotImplementedException();
            }

            public override void UpAction() {
                throw new System.NotImplementedException();
            }

            protected override void DoneFailCast() {
                throw new System.NotImplementedException();
            }

            protected override void DoneFullCast() {
                throw new System.NotImplementedException();
            }
        }
        private class AreaAroundCasterSkill : Skill {
            public override void DownAction() {
                throw new System.NotImplementedException();
            }

            public override void UpAction() {
                throw new System.NotImplementedException();
            }


            protected override void DoneFailCast() {
                throw new System.NotImplementedException();
            }

            protected override void DoneFullCast() {
                throw new System.NotImplementedException();
            }
        }
        private class ConeSkill : Skill {
            public override void DownAction() {
                throw new System.NotImplementedException();
            }

            public override void UpAction() {
                throw new System.NotImplementedException();
            }

            protected override void DoneFailCast() {
                throw new System.NotImplementedException();
            }

            protected override void DoneFullCast() {
                throw new System.NotImplementedException();
            }
        }
        private class ProjectileSkill : Skill {
           
            public override void DownAction() {
                base.DownAction();

                
            }

            public override void UpAction() {
                base.UpAction();

            }

            protected override void DoneFailCast() {
                if (data.failCastPrefab != null) {
                    GameObject obj = GameObject.Instantiate(data.failCastPrefab);
                    var projectile = obj.GetComponent<Projectile>();
                    projectile.SetData(GetSkillOffset() + character.transform.forward, data.range, character.transform.forward);
                }
            }

            protected override void DoneFullCast() {
                if (data.fullCastPrefab != null) {
                    GameObject obj = GameObject.Instantiate(data.fullCastPrefab);
                    var projectile = obj.GetComponent<Projectile>();
                    projectile.SetData(GetSkillOffset() + character.transform.forward, data.range, character.transform.forward);
                }
            }
        }

    }

    

    
}
