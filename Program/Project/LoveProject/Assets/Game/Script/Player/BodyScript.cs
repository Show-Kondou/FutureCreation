using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyScript : StateMachineBehaviour {

	public PlayerStatus _Status;
	public PlayerItem   _Item;
	public PlayerAnimation _Anime;


	private int[] StateName = new int[] {
		Animator.StringToHash( "Stand" ),
		Animator.StringToHash( "Run" ),
		Animator.StringToHash( "Jump" ),
		Animator.StringToHash( "Roll" ),	
		Animator.StringToHash( "Slash_P" ),
		Animator.StringToHash( "Slash_U" ),
		Animator.StringToHash( "Shot" ),
		Animator.StringToHash( "Guard" ),
		Animator.StringToHash( "Eat" ),
	};

	// OnStateEnter is called before OnStateEnter is called on any state inside this state machine
	override public void OnStateEnter( Animator animator, AnimatorStateInfo stateInfo, int layerIndex ) {
		switch( stateInfo.fullPathHash ) {
		case :
		break;
		default:
			break;
		}
		if( stateInfo.IsName("Stand") ) {

		}
		if( stateInfo.IsName("") ) {

		}

	}

	// OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateExit is called before OnStateExit is called on any state inside this state machine
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (stateInfo.IsName( "Jump" )) {
			_Status.SetState = PlayerStatus.STATE.STAND;
			_Anime.ResetLower();
		}
		if (stateInfo.IsName( "Roll" )) {
			_Status.SetState = PlayerStatus.STATE.STAND;
			_Anime.ResetLower();

		}
		if (stateInfo.IsName( "Slash_P" )) {
			_Status.SetState = PlayerStatus.STATE.STAND;
			_Anime.ResetLower();

		}
		if (stateInfo.IsName( "Slash_U" )) {
			_Status.SetState = PlayerStatus.STATE.STAND;
			_Anime.ResetLower();

		}
		if (stateInfo.IsName( "Shot" )) {
			//_Status.SetState = PlayerStatus.STATE.STAND;
			_Anime.ResetLower();

		}
		if (stateInfo.IsName( "Guard" )) {
			_Status.SetState = PlayerStatus.STATE.STAND;
			_Anime.ResetLower();

		}
		if (stateInfo.IsName( "Eat" )) {
			_Status.SetState = PlayerStatus.STATE.STAND;
			_Anime.ResetLower();

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
	//override public void OnStateMachineEnter( Animator animator, int stateMachinePathHash ) {
	//	Debug.Log( "OnStateMachineEnter" );
	//	_Anime = animator.gameObject.GetComponent<PlayerAnimation>();
	//	_Status = _Anime.Status;
	//}


	// OnStateMachineExit is called when exiting a statemachine via its Exit Node
	//override public void OnStateMachineExit(Animator animator, int stateMachinePathHash) {
	//
	//}
}
