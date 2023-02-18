using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove2 : StateMachineBehaviour
{
    [SerializeField] GameObject blockSpawner;
    float timeToChange;
    GameObject reference;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeToChange = 0;
        reference = Instantiate(blockSpawner);
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeToChange += Time.deltaTime;
        if (timeToChange >= 6f)
            animator.SetTrigger("Move1");
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(reference);
        animator.ResetTrigger("Move1");
    }
}
