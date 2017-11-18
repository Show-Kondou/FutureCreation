using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {


	// private static uint     _PlayerCount   = 0;

	#region Member
	// ステータス
	[Header("プレイヤーID"), SerializeField]
	private uint			_PlayerID;

	[Header("体力"),SerializeField]
	private int             _HitPoint	= 100;

	[Header("移動量"),SerializeField]
	private float           _MoveForce	= 10.0F;

	[Header( "ジャンプ力" ), SerializeField]
	private float            _JumpForce = 1.0F;

	[Header("カメラ回転量"),SerializeField]
	private float			_TurnForce	= 30.0F;

	//[Header("アイテム右"),SerializeField]
	//private 
	//[Header("アイテム左"),SerializeField]
	//private 

	// 関連クラス取得
	private PlayerMove      _Move		= null;
	private PlayerJump      _Jump       = null;
	private CameraPlayer    _Camera		= null;
	private PlayerItem      _Item       = null;

	#endregion	Member

	#region Accessor

	public int GetHP { get; set; }
	public bool IsLife { get { return (_HitPoint > 0); } }

	public uint PlayerID {
		get { return _PlayerID; }
	}

	public int PlayerHP {
		// TODO：更新してあるステータスを投げる
		get { return _HitPoint; }
	}

	public ItemManager.ItemID ItemTypeL {
		get { return _Item.ItemL.ID; }
	}
	public ItemManager.ItemID ItemTypeR {
		get { return _Item.ItemR.ID; }
	}

	#endregion Accessor

	/// <summary>
	/// 初期化関数
	/// </summary>
	private void Awake() {
		Init();
	}

	/// <summary>
	/// インスペクター変更時イベント
	/// </summary>
	private void OnValidate() {
		// Init();
	}


	/// <summary>
	/// 初期化
	/// </summary>
	private void Init() {
		// --- プレイヤー関係クラス取得
		_Move = GetPlayerComponent<PlayerMove>();
		_Jump = GetPlayerComponent<PlayerJump>();
		var camera = CameraManager.Instance.GetPlayerCamera( _PlayerID );
		_Camera = Define.NullCheck( camera );
		_Item = GetPlayerComponent<PlayerItem>();
		// --- ステータス反映
		// 移動
		_Move.PlayerID	= _PlayerID;
		_Move.MoveForce = _MoveForce;
		_Move.Camera	= _Camera;
		// ジャンプ
		_Jump.PlayerID	= _PlayerID;
		_Jump.JumpForce = _JumpForce;
		// カメラ
		_Camera.playerTrans = _Move.transform;
		_Camera.TurnForce	= _TurnForce;
		_Camera.Init();
		// アイテム
		_Item.PlayerID	= _PlayerID;
	}


	/// <summary>
	/// コンポーネント取得簡易関数
	/// </summary>
	/// <typeparam name="T">Player系クラス</typeparam>
	/// <returns>取得したコンポーネント</returns>
	T GetPlayerComponent<T>( ) {
		T obj = GetComponentInChildren<T>();
		if( obj != null ) {
			return obj;
		}
		obj = GetComponent<T>();
		if( obj != null ) {
			return obj;
		}
		Debug.LogError( obj.GetType().Name + "の取得失敗" );
		return default(T);
	}

}
