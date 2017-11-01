using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : ObjectBase {


	#region Member
	// ステータス
	[Header("プレイヤーID"), SerializeField]
	private static uint     _PlayerID	= 0;

	[Header("体力"),SerializeField]
	private int             _HitPoint	= 100;

	[Header("移動量"),SerializeField]
	private float           _MoveForce	= 10.0F;

	[Header("カメラ回転量"),SerializeField]
	private float			_TurnForce	= 30.0F;

	//[Header("アイテム右"),SerializeField]
	//private
	//[Header("アイテム左"),SerializeField]
	//private 

	// 関連クラス取得
	private PlayerMove      _Move		= null;
	private PlayerCamera    _Camera		= null;

	#endregion	Member

	#region Getter

	public int GetHP { get; set; }
	public bool IsLife { get { return (_HitPoint > 0); } }

	#endregion Getter

	/// <summary>
	/// 初期化関数
	/// </summary>
	private void Awake() {
		// --- プレイヤー関係クラス取得
		_Move = GetPlayerComponent<PlayerMove>();
		_Camera = GetPlayerComponent<PlayerCamera>();

		// --- ステータス反映
		// 移動
		_Move.MoveForce = _MoveForce;
		_Move.Camera	= _Camera;
		// カメラ
		_Camera.TurnForce = _TurnForce;
	}


	 T GetPlayerComponent<T>() {
		T obj = GetComponentInChildren<T>();
		if( obj == null ) {
			Debug.LogError( obj.GetType().Name + "の取得失敗" );
			return default(T);
		}
		return obj;
	}

}
