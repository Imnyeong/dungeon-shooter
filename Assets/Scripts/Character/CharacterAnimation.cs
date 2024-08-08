using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonShooter
{
    [RequireComponent(typeof(Character))]
    public class CharacterAnimation : MonoBehaviour
    {
        //private Character player;
        private CharacterInput inputController;

        private Animator animator;
        public AnimationType animationType;

        private void Start()
        {
            animator = GetComponentInParent<Animator>();
            inputController = GetComponentInParent<CharacterInput>();
        }
        private void FixedUpdate()
        {
            if (inputController != null)
                CheckAnimation();
        }
        public void CheckAnimation()
        {
            if (!inputController.isGround)
            {
                DoAnimation(AnimationType.Jump);
                return;
            }

            if (inputController.isGround && (inputController.moveX == 0 && inputController.moveZ == 0))
                DoAnimation(AnimationType.Idle);
            else
            {
                if(inputController.moveZ > 0)
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
                        animator.SetBool("Move", false);
                        animator.SetBool("Back", false);
                        break;
                    }
                case AnimationType.Move:
                    {
                        animator.SetBool("Move", true);
                        animator.SetBool("Back", false);
                        break;
                    }
                case AnimationType.Back:
                    {
                        animator.SetBool("Move", true);
                        animator.SetBool("Back", true);
                        break;
                    }
                case AnimationType.Jump:
                    {
                        animator.SetTrigger("Jump");
                        break;
                    }
            }
            animationType = _type;
        }
    }
}

