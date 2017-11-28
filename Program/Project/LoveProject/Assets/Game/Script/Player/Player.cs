/*
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

	//コンポーネントの取得フラグ
	private bool			_IsGetComponent = false;
	
	// --- コンポーネント ---
	private PlayerMove      _Move		= null;
	private PlayerJump      _Jump       = null;
	private CameraPlayer    _Camera		= null;
	private PlayerItem      _Item       = null;
	private PlayerBody		_Body		= null;
	#endregion	Member



	#region Accessor
	public uint PlayerID {
		get { return Status._PlayerID; }
	}

	/// <summary>
	/// 生存確認
	/// </summary>
	public bool IsLife {
		get { return (_Item.HitPoint > 0); }
	}
	///// <summary>
	///// 体力
	///// </summary>
	//public int PlayerHP {
	//	get { return _Item.HitPoint; }
	//}
	/// <summary>
	/// アイテムの種類
	/// </summary>
	public ItemManager.ItemType ItemTypeL {
		get { return _Item.ItemL.Type; }
	}
	public ItemManager.ItemType ItemTypeR {
		get { return _Item.ItemR.Type; }
	}
	#endregion Accessor



	/// <summary>
	/// 初期化関数
	/// </summary>
	private void Awake() {
		InitComponent();
		InitStatus();
	}

	/// <summary>
	/// インスペクター変更時イベント
	/// </summary>
	private void OnValidate() {

		if ( !_IsGetComponent ) return;

		// --- ステータス反映
		// 移動
		_Move.Status = Status;
		// ジャンプ
		_Jump.Status = Status;
		//_Jump.JumpForce = _JumpForce;
		// カメラ
		_Camera.TurnForce = Status._TurnForce;
		// アイテム
		//_Item.PlayerID = _PlayerID;
		//_Item.HitPoint = _HitPoint;
		// 体の方向
		_Body.Status = Status;
	}


	/// <summary>
	/// 初期化
	/// </summary>
	private void InitComponent() {
		// --- プレイヤー関係クラス取得
		_Move = GetPlayerComponent<PlayerMove>();
		_Jump = GetPlayerComponent<PlayerJump>();
		var camera = CameraManager.Instance.GetPlayerCamera( Status._PlayerID );
		_Camera = Define.NullCheck( camera );
		_Item = GetPlayerComponent<PlayerItem>();
		_IsGetComponent = true;
		_Body = GetPlayerComponent<PlayerBody>();
	}


	/// <summary>
	/// 初期化
	/// </summary>
	private void InitStatus() {
		Status =_InitStatus;
		//// --- ステータス反映
		// 移動
		_Move.Status = Status;
		// ジャンプ
		_Jump.Status = Status;
		//_Jump.JumpForce = _JumpForce;
		// カメラ
		_Camera.TurnForce = Status._TurnForce;
		// アイテム
		//_Item.PlayerID = _PlayerID;
		//_Item.HitPoint = _HitPoint;
		// 体の方向
		_Body.Status = Status;
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

	protected override void Execute() {
	}
}
