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
            if (player.inputController != null)
                CheckAnimation();
        }
        public void CheckAnimation()
        {
            if(!player.isLive)
            {
                DoAnimation(AnimationType.Death);
                return;
            }
            if (!player.inputController.canAttack)
            {
                DoAnimation(AnimationType.Attack);
                return;
            }
            if (!player.inputController.isGround)
            {
                DoAnimation(AnimationType.Jump);
                return;
            }
            if (player.inputController.isGround && (player.inputController.moveX == 0 && player.inputController.moveZ == 0))
                DoAnimation(AnimationType.Idle);
            else
            {
                if(player.inputController.moveZ > 0)
                    DoAnimation(AnimationType.Move);
                else
                    DoAnimation(AnimationType.Back);
            }
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
                        break;
                    }
                case AnimationType.Attack:
                    {
                        animator.SetTrigger(StringData.AnimationAttack);
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

