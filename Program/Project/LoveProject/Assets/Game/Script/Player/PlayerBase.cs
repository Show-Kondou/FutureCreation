/*
 *	▼ File		PlayerBase.cs
 *	
 *	▼ Brief	説明
 *	
 *	▼ Author	Show Kondou
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStatus {

	public enum STATE : int {
		//		状態	　　番号
		STAND,  // 立ち		　 0
		RUN,    // 走る		　 1
		JUMP,   // ジャンプ	　 2
		ROLL,   // ロール	　 3
		ATTACK,	// 攻撃		   4	(種類は他のステータスで判定)
		EAT,    // 食べる	　 5
		WIN,    // 勝ち		　 6
		LOSS,   // 負け		　 7
		MAX,
	};

	private int[] StatePriority = new int[(int)STATE.MAX]{0,0,0,2,3,4,4,0 };

	[Header("プレイヤーID"), SerializeField]
	public uint            _PlayerID;
	[Header("体力"),SerializeField]
	public int             _HitPoint;
	[Header("移動量"),SerializeField]
	public float           _MoveForce;
	[Header( "ジャンプ力" ), SerializeField]
	public float           _JumpForce;
	[Header("カメラ回転量"),SerializeField]
	public float           _TurnForce;
	[Header("上半身ステータス"),SerializeField]
	private STATE           _UpperState;
	[Header("下半身ステータス"),SerializeField]
	private STATE           _LowerState;
	[Header("カメラのトランスフォーム"),NonSerialized]
	public Transform       _CameraTrans;
	[Header("アニメーション"), NonSerialized]
	public PlayerAnimation _Animation;

	public bool             _IsLose = false;

	public STATE State {
		set {
			if(StatePriority[(int)_UpperState] <= StatePriority[(int)value])
				_UpperState = value;
		}
		get {
			return _UpperState;
		}
	}
	public STATE SetState {
		set{ _UpperState = value; }
	}

	public STATE LowerState {
		get { return _LowerState; }
		set { _LowerState = value; }
	}

}


/// <summary>
/// PlayerBaseクラス
/// </summary>
abstract public class PlayerBase : MonoBehaviour {

	// 定数
	#region Constant
	#endregion Constant



	// メンバー
	#region Member
	// プレイヤーステータス
	private PlayerStatus    _Status;
	// 各オブジェクトごとにヒットストップを行うため
	private float   _deltaTime;       // 各オブジェクトのデルタタイム
	private float   _timeScale = 1.0F; // 各オブジェクトのタイムスケール
	#endregion Member



	// アクセサ
	#region Accessor
	public PlayerStatus Status {
		get { return _Status; }
		set { _Status = value; }
	}
	/// <summary>
	/// 各オブジェクトのデルタタイムのアクセサ
	/// </summary>
	public float DeltaTime {
		get { return _deltaTime * _timeScale; }
	}

	/// <summary>
	/// 各オブジェクトのタイムスケールのアクセサ
	/// set でデルタタイムを更新する
	/// </summary>
	public float TimeScale {
		get { return _timeScale; }
		set { _timeScale = value; }
	}

	public void AddHitPoint(int value) {
		Status._HitPoint += value;
		if (Status._HitPoint > 100) {
			Status._HitPoint = 100;
		}
		if (Status._HitPoint <= 0) {
			Status._HitPoint = 0;
		}
	}
	#endregion Accessor

	// メソッド
	#region Method
	private void Update() {
		_deltaTime = Time.deltaTime;
		if (GameScene.GameState != 1) return;
		if( Status._HitPoint <= 0 ) return;
		PlayerMasterExecute();
		Execute();
	}
	private void LateUpdate() {
		_deltaTime = Time.deltaTime;
		if (GameScene.GameState != 1)
			return;
		if( Status._HitPoint <= 0 ) {
			Status.State = PlayerStatus.STATE.LOSS;
			Status.LowerState = PlayerStatus.STATE.LOSS;
			Status._Animation.StartLose();
			return;
		}

		LateExecute();
	}
	private void FixedUpdate() {
		if (GameScene.GameState != 1)
			return;
		if( Status._HitPoint <= 0 )
			return;

		FixedExecute();
	}

	public void DestroyPlayer() {
		// DefaultCamera
		var c = Status._CameraTrans.GetComponent<DefaultCamera>();
		c.StartDemo();
		Status._Animation.StopAnimation();
		Destroy(gameObject);
	}

	virtual protected void FixedExecute() { }
	virtual protected void PlayerMasterExecute() { }
	abstract protected void Execute();
	virtual protected void LateExecute() { }
	abstract public void Init();
	#endregion Method
}






/*
 *	▼ End of File
*/
