using UnityEngine;

public class MogallAttackBehavior : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.GetFloat("attackInterval") > 0f)
        {
            animator.SetFloat("attackInterval", animator.GetFloat("attackInterval") - Time.deltaTime);
        }
        else
        {
            animator.SetFloat("attackInterval", 2f);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetFloat("attackInterval", 2f);
    }
}
