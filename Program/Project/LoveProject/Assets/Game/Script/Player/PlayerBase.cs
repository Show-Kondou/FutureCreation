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
	public enum State {
		//		状態	　　番号
		STAND,  // 立ち		　 0
		RUN,    // 走る		　 1
		JUMP,   // ジャンプ	　 2

		SLASH,  // 攻撃		　 3
		SCHOTT, // 攻撃		　 4
		GUARD,  // 攻撃		　 5

		EAT,    // 食べる	　 6
		ROLL,   // ロール	　 7
		WIN,    // 勝ち		　 8
		LOSS,   // 負け		　 9
	}
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
	public State			_State;
	[Header("カメラのトランスフォーム"),NonSerialized]
	public Transform       _CameraTrans;
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
