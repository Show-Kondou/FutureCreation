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

	private int[] StatePriority = new int[(int)STATE.MAX]{0,0,1,1,1,1,2,2 };

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
	[Header("ステータス"),SerializeField]
	private STATE			_State;
	[Header("カメラのトランスフォーム"),NonSerialized]
	public Transform       _CameraTrans;
	[Header("アニメーション"), NonSerialized]
	public PlayerAnimation _Animation;

	public STATE State {
		set {
			if(StatePriority[(int)_State] <= StatePriority[(int)value])
				_State = value;
		}
		get {
			return _State;
		}
	}
	public STATE SetState {
		set{ _State = value; }
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
	#endregion Accessor

	// メソッド
	#region Method
	private void Update() {
		_deltaTime = Time.deltaTime;
		Execute();
	}
	private void LateUpdate() {
		_deltaTime = Time.deltaTime;
		LateExecute();
	}
	private void FixedUpdate() {
		FixedExecute();
	}
	virtual protected void FixedExecute() { }
	abstract protected void Execute();
	virtual protected void LateExecute() { }
	#endregion Method
}



/*
 *	▼ End of File
*/