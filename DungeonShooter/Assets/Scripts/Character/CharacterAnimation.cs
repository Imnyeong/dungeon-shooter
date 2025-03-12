using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonShooter
{
    [RequireComponent(typeof(Character))]
    public class CharacterAnimation : MonoBehaviour
    {
        private Character player;

        private Animator animator;
        public AnimationType animationType;

        private void Start()
        {
            animator = GetComponent<Animator>();
            player = GetComponent<Character>();
        }

        private void FixedUpdate()
        {
            if(player.inputController != null)
                CheckAnimation();
        }

        public void CheckAnimation()
        {
            if (!player.isLive)
                return;

            if (player.inputController.moveX == 0 && player.inputController.moveZ == 0)
                DoAnimation(AnimationType.Idle);
            else if (player.inputController.moveZ > 0)
                DoAnimation(AnimationType.Move);
            else if (player.inputController.moveZ <= 0)
                DoAnimation(AnimationType.Back);
        }

        public void DoAnimation(AnimationType _type)
        {
            if (animationType == _type)
                return;

            switch (_type)
            {
                case AnimationType.Idle:
                    {
                        animator.SetBool(StringData.AnimationMove, false);
                        animator.SetBool(StringData.AnimationBack, false);
                        break;
                    }
                case AnimationType.Move:
                    {
                        animator.SetBool(StringData.AnimationMove, true);
                        animator.SetBool(StringData.AnimationBack, false);
                        break;
                    }
                case AnimationType.Back:
                    {
                        animator.SetBool(StringData.AnimationMove, true);
                        animator.SetBool(StringData.AnimationBack, true);
                        break;
                    }
                case AnimationType.Jump:
                    {
                        animator.SetTrigger(StringData.AnimationJump);
                        player.audioController.PlayClip(StringData.ClipJump);
                        break;
                    }
                case AnimationType.Attack:
                    {
                        animator.SetTrigger(StringData.AnimationAttack);
                        player.audioController.PlayClip(StringData.ClipAttack);
                        break;
                    }
                case AnimationType.Hit:
                    {
                        animator.SetTrigger(StringData.AnimationHit);
                        player.audioController.PlayClip(StringData.ClipHit);
                        break;
                    }
                case AnimationType.Death:
                    {
                        animator.SetTrigger(StringData.AnimationDeath);
                        break;
                    }
            }
            animationType = _type;
        }
    }
}

