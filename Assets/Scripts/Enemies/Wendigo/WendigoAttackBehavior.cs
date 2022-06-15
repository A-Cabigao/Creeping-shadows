using UnityEngine;

public class WendigoAttackBehavior : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<WendigoAttack>().Attack();
    }
}
