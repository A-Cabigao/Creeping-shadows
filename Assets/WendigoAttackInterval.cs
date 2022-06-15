using UnityEngine;

public class WendigoAttackInterval : StateMachineBehaviour
{
    private float timer;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = animator.GetFloat("attackInterval");
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer -= animator.GetFloat("rageAttackInterval") * Time.deltaTime;
        animator.SetFloat("attackInterval", timer);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetFloat("attackInterval", 1.5f);
    }
}
