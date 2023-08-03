using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{

    public class AnimatorManager : MonoBehaviour
    {
        public Animator animator;
        public void PlayTargetAnimation(string targetAim, bool isInteracing)
        {
            animator.applyRootMotion = isInteracing;
            animator.SetBool("isInteracting", isInteracing);
            animator.CrossFade(targetAim, 0.2f);

        }
    }

}