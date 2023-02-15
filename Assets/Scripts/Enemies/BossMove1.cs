using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove1 : StateMachineBehaviour
{
    float timeToChange;
    float timeToAttack;
    [SerializeField] GameObject bullet;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeToChange = 0;
        timeToAttack = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeToChange += Time.deltaTime;
        if (timeToChange >= 6f)
            animator.SetTrigger("Move2");

        timeToAttack += Time.deltaTime;
        if (timeToAttack > 1.5f)
        {
            timeToAttack = 0;
            GameObject newBullet = Instantiate(bullet, new Vector3(179.88f, -2.72f, 0), Quaternion.identity);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Move2");
    }
}
