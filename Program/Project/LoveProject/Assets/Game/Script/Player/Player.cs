/*
 *	▼ File		PlayerItem.cs
 *	
 *	▼ Brief	説明
 *	
 *	▼ Author	Show Kondou
*/using UnityEngine;

/// <summary>
/// プレイヤークラス
/// </summary>
public class Player : MonoBehaviour {

//TODO : PlayerStatusClass制作

	public enum PlayerState {
		STAND,	// 立ち
		RUN,	// 走る
		JUMP,	// ジャンプ
		ATTACK,	// 攻撃
		EAT,	// 食べる
		ROLL,	// ロール
		WIN,	// 勝ち
		LOSS,	// 負け
	}

	// プレイヤーステータス
	public struct PlayerStatus {
		uint _PlayerID;		
		int _HitPoint;
		float _MoveForce;
		float _JumpForce;
		float _TurnForce;
		PlayerState _State;
	};



	private PlayerState _State;

	#region Member
	// --- ステータス ---
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
	/// <summary>
	/// プレイヤーID
	/// </summary>
	public uint PlayerID {
		get { return _PlayerID; }
	}
	/// <summary>
	/// 生存確認
	/// </summary>
	public bool IsLife {
		get { return (_Item.HitPoint > 0); }
	}
	/// <summary>
	/// 体力
	/// </summary>
	public int PlayerHP {
		get { return _Item.HitPoint; }
	}
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
		_Move.PlayerID = _PlayerID;
		_Move.MoveForce = _MoveForce;
		_Move.Camera = _Camera;
		// ジャンプ
		_Jump.PlayerID = _PlayerID;
		_Jump.JumpForce = _JumpForce;
		// カメラ
		_Camera.TurnForce = _TurnForce;
		// アイテム
		_Item.PlayerID = _PlayerID;
		_Item.HitPoint = _HitPoint;
	}


	/// <summary>
	/// 初期化
	/// </summary>
	private void InitComponent() {
		// --- プレイヤー関係クラス取得
		_Move = GetPlayerComponent<PlayerMove>();
		_Jump = GetPlayerComponent<PlayerJump>();
		var camera = CameraManager.Instance.GetPlayerCamera( _PlayerID );
		_Camera = Define.NullCheck( camera );
		_Item = GetPlayerComponent<PlayerItem>();
		_IsGetComponent = true;
		_Body = GetPlayerComponent<PlayerBody>();
	}


	/// <summary>
	/// 初期化
	/// </summary>
	private void InitStatus() {
		// --- ステータス反映
		// 移動
		_Move.PlayerID = _PlayerID;
		_Move.MoveForce = _MoveForce;
		_Move.Camera = _Camera;
		// ジャンプ
		_Jump.PlayerID = _PlayerID;
		_Jump.JumpForce = _JumpForce;
		// カメラ
		_Camera.playerTrans = _Move.transform;
		_Camera.TurnForce = _TurnForce;
		_Camera.Init();
		// アイテム
		_Item.PlayerID = _PlayerID;
		_Item.HitPoint = _HitPoint;
		// 体の方向
		_Body.PlayerID = _PlayerID;
		_Body.Camera = _Camera.transform;
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
