using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyScript : StateMachineBehaviour {

	public PlayerStatus _Status;

	// OnStateEnter is called before OnStateEnter is called on any state inside this state machine
	//override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateExit is called before OnStateExit is called on any state inside this state machine
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (stateInfo.IsName( "Jump" )) {
			_Status.SetState = PlayerStatus.STATE.STAND;
		}
		if (stateInfo.IsName( "Roll" )) {
			_Status.SetState = PlayerStatus.STATE.STAND;
		}
		if (stateInfo.IsName( "Slash_P" )) {
			_Status.SetState = PlayerStatus.STATE.STAND;
		}
		if (stateInfo.IsName( "Slash_U" )) {
			_Status.SetState = PlayerStatus.STATE.STAND;
		}
		if (stateInfo.IsName( "Shot" )) {
			_Status.SetState = PlayerStatus.STATE.STAND;
		}
		if (stateInfo.IsName( "Guard" )) {
			_Status.SetState = PlayerStatus.STATE.STAND;
		}
		if (stateInfo.IsName( "Eat" )) {
			_Status.SetState = PlayerStatus.STATE.STAND;
		}

	}

	// OnStateMove is called before OnStateMove is called on any state inside this state machine
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called before OnStateIK is called on any state inside this state machine
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMachineEnter is called when entering a statemachine via its Entry Node
	//override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash) {
	//	Debug.Log( "OnStateMachineEnter" );
	//	_Status = animator.gameObject.GetComponent<PlayerAnimation>().Status;
	//}

	// OnStateMachineExit is called when exiting a statemachine via its Exit Node
	//override public void OnStateMachineExit(Animator animator, int stateMachinePathHash) {
	//
	//}
}
