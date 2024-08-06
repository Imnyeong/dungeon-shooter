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
            //else
                //AnimationSync();
        }
        public void CheckAnimation()
        {
            if(!inputController.isGround)
            {
                animationType = AnimationType.Jump;
                animator.Play("Jump_Full_Short");
            }
            else if (inputController.moveX != 0 || inputController.moveZ != 0)
            {
                if (inputController.moveZ <= 0)
                {
                    animationType = AnimationType.Back;
                    animator.Play("Walking_B");
                }
                else
                {
                    animationType = AnimationType.Move;
                    animator.Play("Running_A");
                }
            }
            else
            {
                animationType = AnimationType.Idle;
                animator.Play("Idle");
            }
        }
    }
}

