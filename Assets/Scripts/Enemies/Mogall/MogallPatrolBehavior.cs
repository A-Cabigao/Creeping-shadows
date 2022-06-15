using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MogallPatrolBehavior : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.Play("MogallMove",1);
    }
}
