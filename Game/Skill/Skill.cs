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

        protected float remainingCooldown = 0; // ���� ��Ÿ��
        protected float castTime = 0;// ���� �ɽ�Ʈ Ÿ��
        protected bool isCasting = false;

        protected Coroutine clickCoroutine = null; // Ŭ�� �Ǿ����� �ߵ��Ǵ� �ڷ�ƾ
        protected bool isClick = false; // Ŭ�� ����

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
            isClick = true; // Ŭ���������
            if(clickCoroutine != null) { // ���� ó��
                character.StopCoroutine(clickCoroutine);
                clickCoroutine = null;
            }
            // Click Coroutine ����
            clickCoroutine = character.StartCoroutine(ClickCoroutine());
        }
        /// <summary>
        /// Button Up
        /// </summary>
        public virtual void UpAction() {
            if (!data.isTriggerSkillOnButtonDown) OnCast();
            // �ʱ�ȭ
            isClick = false;
            castTime = 0;
            character.StopCoroutine(clickCoroutine);
            clickCoroutine = null;
        }
        #endregion
        #region Update

        // CastTime ���� ClickCoroutine ���� ȣ��
        private void UpdateCastTime() {
            if (Settings.isPause) return;
            if (castTime < data.castTime) castTime += Time.deltaTime;
        }

        // ��Ÿ�� �ð� ���� StartCooldownTimer���� ȣ�� �����ɶ� ȣ��
        private IEnumerator UpdateCoolTime() {
            while (true) {
                if (!Settings.isPause && remainingCooldown > 0) {
                    remainingCooldown -= Time.deltaTime;
                }
                yield return null;
            }
        }

        // Ŭ�� �����϶� �߻��Ǵ� �ڷ�ƾ DownAction���� ȣ��
        private IEnumerator ClickCoroutine() {
            while (isClick) {
                if (IsOnCooldown()) { // ��� �����ϸ�
                    UpdateCastTime();// �ɽ��� Ÿ�� ����
                    if (data.isTriggerSkillOnButtonDown && IsOnCast()) OnCast(); // trigger�� true�� Cast�� �Ϸ� ���¸� down ���¿��� click ȣ��
                }
                yield return null;
            }
        }

        /// <summary>
        /// �ɽ��� �Ϸ��� ȣ��
        /// </summary>
        private void OnCast() {     
            if (IsOnCast()) {
                DoneFullCast();
            } else {
                DoneFailCast();
            }
            // ���� �ð� ����
            remainingCooldown = data.cooldown;
            castTime = 0;
        }
        /// <summary>
        /// �ɽ�Ʈ �Ϸ��� ȣ�� / ��ų ���� �����κ�
        /// </summary>
        protected abstract void DoneFullCast();

        /// <summary>
        /// �ɽ�Ʈ ���н� ȣ��
        /// </summary>
        protected abstract void DoneFailCast();

        #endregion

        /// <summary>
        /// ��ų �⺻ ������ġ
        /// </summary>
        /// <returns></returns>
        protected Vector3 GetSkillOffset() {
            return character.transform.position + new Vector3(0, 1, 0);
        }

        /// <summary>
        /// ���� ��Ÿ�� Ȯ��
        /// </summary>
        /// <returns></returns>
        public float GetRemainingCoolTime() {
            return remainingCooldown;
        }
        /// <summary>
        /// ��Ÿ�� ������ �Ǻ�
        /// </summary>
        /// <returns></returns>
        protected bool IsOnCooldown() {
            return remainingCooldown <= 0f;
        }

        /// <summary>
        /// ���� �ɽ��� �ð� ��ȯ
        /// </summary>
        /// <returns></returns>
        public float GetCastTime() {
            return castTime;
        }

        /// <summary>
        /// �ɽ����� �Ϸ� ����� �Ǻ�
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
