using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TerribleTulipShootAnimationEvents : StateMachineBehaviour
{
    [SerializeField] UnityEvent onStateEnter = new UnityEvent();
    [SerializeField] UnityEvent onStateUpdate = new UnityEvent();
    [SerializeField] UnityEvent onStateExit = new UnityEvent();
    [SerializeField] UnityEvent onStateMove = new UnityEvent();
    [SerializeField] UnityEvent onStateIk = new UnityEvent();
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
     onStateEnter.Invoke();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        onStateUpdate.Invoke();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        onStateExit.Invoke();
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        onStateMove.Invoke();
    }

    // OnStateIK is called right after Animator.OnAnimatorIK()
    override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        onStateIk.Invoke();
    }
}
