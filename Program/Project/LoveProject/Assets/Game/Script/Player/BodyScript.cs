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

		if (GameScene.GameState != 1)
			return;
		if ( stateInfo.IsName("Stand") ) {
			_Anime.ResetLower();
		}
		if( stateInfo.IsName( "Run" ) ) {
			CSoundManager.Instance.PlaySE( AUDIO_LIST.WALK );
			_Anime.ResetLower();
		}
		if( stateInfo.IsName( "Jump" ) ) {
			CSoundManager.Instance.PlaySE(AUDIO_LIST.WALK);
		}
		if( stateInfo.IsName( "Roll" ) ) {
		}
		if( stateInfo.IsName( "Slash_P" ) ) {
			CSoundManager.Instance.PlaySE( AUDIO_LIST.SHAKE );
		}
		if( stateInfo.IsName( "Slash_U" ) ) {
			CSoundManager.Instance.PlaySE( AUDIO_LIST.SHAKE );
		}
		if( stateInfo.IsName( "Shot" ) ) {
		}
		if( stateInfo.IsName( "Guard" ) ) {
			CSoundManager.Instance.PlaySE( AUDIO_LIST.STAND );
		}
		if( stateInfo.IsName( "Eat" ) ) {
		}
		if( stateInfo.IsName( "Win" ) ) {
		}
		if( stateInfo.IsName( "Lose" ) ) {
			//_Status.SetState = PlayerStatus.STATE.STAND;
			//_Status.LowerState = PlayerStatus.STATE.STAND;
			//Debug.Log( "Loss" );
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
			_Status.LowerState = PlayerStatus.STATE.STAND;
			CSoundManager.Instance.PlaySE( AUDIO_LIST.WALK );

		}
		if (stateInfo.IsName( "Roll" )) {
			_Status.SetState = PlayerStatus.STATE.STAND;
			_Status.LowerState = PlayerStatus.STATE.STAND;
		}
		/* 攻撃 */
		if (stateInfo.IsName( "Slash_P" )) {
			_Item.EndAction();
		}
		if (stateInfo.IsName( "Slash_U" )) {
			_Item.EndAction();
		}
		if (stateInfo.IsName( "Shot" )) {
			_Item.EndAction();
			CSoundManager.Instance.PlaySE( AUDIO_LIST.SHAKE );
		}
		if (stateInfo.IsName( "Guard" )) {
			_Item.EndAction();
		}

		if (stateInfo.IsName( "Eat" )) {
			_Item.EndEat();	
			_Status.SetState = PlayerStatus.STATE.STAND;
			_Status.LowerState = PlayerStatus.STATE.STAND;

			CSoundManager.Instance.PlaySE( AUDIO_LIST.CURE );
		}

		if( stateInfo.IsName( "Win" ) ) {

		}
		if( stateInfo.IsName( "Lose" ) ) {
			_Item.DestroyPlayer();
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
