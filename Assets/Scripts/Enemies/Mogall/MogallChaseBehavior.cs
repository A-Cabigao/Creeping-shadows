using UnityEngine;

public class MogallChaseBehavior : StateMachineBehaviour
{
    private float timer = 5f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetFloat("chaseTimer", timer);
        animator.Play("MogallMove", 1);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetBool("isPlayerSpotted"))
            timer = 5f;
        else
        {
            if(!GameMaster.gameIsPaused)
                timer -= Time.deltaTime;
        }

        animator.SetFloat("chaseTimer", timer);
    }
}
