﻿/*
 *	▼ File		PlayerItem.cs
 *	
 *	▼ Brief	説明
 *	
 *	▼ Author	Show Kondou
*/
using System;
using UnityEngine;

/// <summary>
/// プレイヤークラス
/// </summary>
public class Player : PlayerBase {

	//TODO : PlayerStatusClass制作

	#region Member
	[Header("ステータス"), SerializeField]
	private PlayerStatus    _InitStatus;
	[SerializeField]
	private ItemManager.ItemType _ItemL;
	[SerializeField]
	private ItemManager.ItemType _ItemR;
	//コンポーネントの取得フラグ
	private bool			_IsGetComponent = false;
	private bool _IsGameStart = false;
	
	// --- コンポーネント ---
	private PlayerMove      _Move		= null;
	private PlayerJump      _Jump       = null;
	private CameraPlayer    _Camera		= null;
	private PlayerItem      _Item       = null;
	private PlayerRoll      _Roll       = null;
	private PlayerBody		_Body		= null;
	private PlayerAnimation _Animetion	= null;
	#endregion	Member



	#region Accessor
	public uint PlayerID {
		get { return Status._PlayerID; }
	}

	public int PlayerHP {
		get { 
			if( GameScene.GameState == 0 )
				return 100;
			return Status._HitPoint;
		}
	}

	public bool IsDeath{
		get{ return Status._HitPoint <= 0; }
	}

	/// <summary>
	/// 生存確認
	/// </summary>
	public bool IsLife {
		get { return (_Item.Status._HitPoint > 0); }
	}
	/// <summary>
	/// アイテムの種類
	/// </summary>
	public ItemManager.ItemType ItemTypeL {
	//	get { return _Item.ItemTypeL; }
	get {
			if( _Item == null ) {
				return ItemManager.ItemType.None;
			}
			return _Item.ItemTypeL;
		}
	}
	public ItemManager.ItemType ItemTypeR {
		get {
			if( _Item == null ) {
				return ItemManager.ItemType.None;
			}
			return _Item.ItemTypeR;
		}
	}
	#endregion Accessor



	public void Start() {
		InitComponent();
	}
	/// <summary>
	/// 初期化関数
	/// </summary>
	//private void GameStart() {
	//	Init();
	//}

	public override void Init() {
		InitComponent();
		InitStatus();
	}

	/// <summary>
	/// インスペクター変更時イベント
	/// </summary>
	//private void OnValidate() {
	//	Status = _InitStatus;

	//	//if ( !_IsGetComponent ) return;

	//	//// --- ステータス反映
	//	//// 移動
	//	//_Move.Status = Status;
	//	//// ジャンプ
	//	//_Jump.Status = Status;
	//	////_Jump.JumpForce = _JumpForce;
	//	//// カメラ
	//	//_Camera.TurnForce = Status._TurnForce;
	//	//// アイテム
	//	//_Item.Status = Status;
	//	//// 体の方向
	//	//_Body.Status = Status;
	//	//// アニメーション
	//	//_Animetion.Status = Status;
	//}


	/// <summary>
	/// 初期化
	/// </summary>
	private void InitComponent() {
		Status = _InitStatus;
		// --- プレイヤー関係クラス取得
		_Move = GetPlayerComponent<PlayerMove>();
		_Jump = GetPlayerComponent<PlayerJump>();
		var camera = CameraManager.Instance.GetPlayerCamera( Status._PlayerID );
		_Camera = camera;//Define.NullCheck( camera );
		_Item = GetPlayerComponent<PlayerItem>();
		_Roll = GetPlayerComponent<PlayerRoll>();
		_Body = GetPlayerComponent<PlayerBody>();
		_Animetion = GetPlayerComponent<PlayerAnimation>();
		Status._CameraTrans = _Camera.transform;
		Status._Animation = _Animetion;
		_IsGetComponent = true;
	}


	/// <summary>
	/// 初期化
	/// </summary>
	private void InitStatus() {
		Status = _InitStatus;
		//// --- ステータス反映
		// 移動
		_Move.Status = Status;
		_Move.Init();
		// ジャンプ
		_Jump.Status = Status;
		_Jump.Init();
		// カメラ
		_Camera.TurnForce = Status._TurnForce;
		_Camera.PlayerID = Status._PlayerID;
		_Camera.Init( _Move.transform );
		// アイテム
		_Item.Status = Status;
		_Item.Init();
		// 回避＆ダメージ
		_Roll.Status = Status;
		_Roll.Init();
		// 体の方向
		_Body.Status = Status;
		_Body.Init();
		// アニメーション
		_Animetion.Status = Status;
		_Animetion.Init();
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

	//protected override void PlayerMasterExecute() {
	//	if (GameScene.GameState != 1) return;
	//	if( _IsGameStart == false ){
	//		GameStart();
	//		_IsGameStart = true;
	//	}

	//	_ItemL = _Item.ItemTypeL;
	//	_ItemR = _Item.ItemTypeR;
	//}

	protected override void Execute() {
		_ItemL = _Item.ItemTypeL;
		_ItemR = _Item.ItemTypeR;
	}



	public void GameStart(){
		Init();
	}

	public void GameEnd() {

	}
}
