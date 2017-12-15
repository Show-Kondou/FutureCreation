/*
 *	▼ File		PlayerItem.cs
 *	
 *	▼ Brief	説明
 *	
 *	▼ Author	Show Kondou
*/
using UnityEngine;


/// <summary>
/// PlayerItemクラス
/// </summary>
public class PlayerItem : PlayerBase {

	// メンバー
	#region Member
	// 各アイテムのキャッシュ
	private Item    _ItemL;
	private Item    _ItemR;
	private bool _ItemLFlag = false;
	private bool _ItemRFlag = false;
	private bool _EatLFlag = false;
	private bool _EatRFlag = false;
	[SerializeField]
	private Transform _HandTrans;
	private PlayerAnimation _Anime;

	// private int _HandItem = 0; // 0…なし　1…左　2…右
	#endregion Member



	// アクセサ
	#region Accessor
	/// <summary>
	/// アイテム左
	/// </summary>
	public Item ItemL {
		get { return _ItemL; }
		set { _ItemL = value; }
	}

	public ItemManager.ItemType ItemTypeL {
		get {
			if( _ItemL == null ) {
				return ItemManager.ItemType.None;
			}
			return _ItemL.Type;
		}
	}
	public ItemManager.ItemType ItemTypeR {
		get {
			if( _ItemR == null ) {
				return ItemManager.ItemType.None;
			}
			return _ItemR.Type;
		}
	}

	/// <summary>
	/// アイテム右
	/// </summary>
	public Item ItemR {
		get { return _ItemR; }
		set { _ItemR = value; }
	}
	#endregion Accessor



	// メソッド
	#region Method
	/// <summary>
	/// 更新処理
	/// </summary>
	protected override void Execute() {
		ActionItem();
		EatItem();
		// ItemBreak();
	}

	/// <summary>
	/// アイテム発動
	/// </summary>
	private void ActionItem() {
		if (Status.State != PlayerStatus.STATE.EAT) {
			// 左アイテムアクション
			if (InputGame.GetPlayerItemL( Status._PlayerID )) {
				// 所持しているか
				if (_ItemL == null)
					return;
				// 右のアイテムが使用中か
				if (_ItemRFlag)
					return;
				// アイテムスタート
				_Anime.ActionNumber = _ItemL.ActionStart();
				Status.State = PlayerStatus.STATE.ATTACK;
				_ItemLFlag = true;
			}
			// 右アイテムアクション
			else if (InputGame.GetPlayerItemR( Status._PlayerID )) {
				// 所持しているか
				if (_ItemR == null)
					return;
				// 左のアイテムが使用中か
				if (_ItemLFlag)
					return;
				_Anime.ActionNumber = _ItemR.ActionStart();
				Status.State = PlayerStatus.STATE.ATTACK;
				_ItemRFlag = true;
			}
		}


		// アイテム解除
		if( InputGame.GetPlayerUpItemL(Status._PlayerID) ) {
			if( !_ItemLFlag ) return;
			Status.SetState = PlayerStatus.STATE.STAND;
		}
		if( InputGame.GetPlayerUpItemR( Status._PlayerID ) ) {
			if( !_ItemRFlag ) return;
			Status.SetState = PlayerStatus.STATE.STAND;
		}
	}

	/// <summary>
	/// Action終了
	/// </summary>
	public void EndAction() {
		if( _ItemLFlag ) {
			_ItemL.ActionEnd();
			_ItemLFlag = false;
			if( _ItemL != null && _ItemL.IsBreak ) {
				Debug.Log("左アイテム消費");
				_ItemL = null;
				CSoundManager.Instance.PlaySE( AUDIO_LIST.BREAK );
			}
		}
		if( _ItemRFlag ) {
			_ItemR.ActionEnd();
			_ItemRFlag = false;
			if( _ItemR != null && _ItemR.IsBreak ) {
				Debug.Log("アイテム消費");
				_ItemR = null;
				CSoundManager.Instance.PlaySE( AUDIO_LIST.BREAK );
			}
		}
	}

	public void EndEat(){
		if( _EatLFlag == true ) {
			Status._HitPoint += _ItemL.EatItem();
			_ItemL = null;
		} else if( _EatRFlag == true ) {
			Status._HitPoint += _ItemR.EatItem();
			_ItemR = null;
		}
	}
	/// <summary>
	/// アイテムブレイク判定
	/// </summary>
	private void ItemBreak() {
		// アイテム終
		if( _ItemL != null && _ItemL.IsBreak )
			_ItemL = null;
		if( _ItemR != null && _ItemR.IsBreak )
			_ItemR = null;

	}

	/// <summary>
	/// アイテム食べる
	/// </summary>
	private void EatItem() {
		// 左アイテム食べる
		if ( InputGame.GetPlayerEatL( Status._PlayerID ) ) {
			if ( _ItemL == null ) return;
			if (_ItemLFlag == true) return;
			// Status._HitPoint += _ItemL.EatItem();
			Status.State = PlayerStatus.STATE.EAT;
			_EatLFlag = true;
			// _ItemL = null;
		}
		// 右アイテム食べる
		else if ( InputGame.GetPlayerEatR( Status._PlayerID ) ) {
			if ( _ItemR == null ) return;
			if (_ItemRFlag == true) return;
			//Status._HitPoint += _ItemR.EatItem();
			Status.State = PlayerStatus.STATE.EAT;
			_EatRFlag = true;
			//_ItemR = null;
		}
	}

	


	#endregion Method



	// イベント
	#region MonoBehaviour Event

	private void Start() {
		_Anime = GetComponent<PlayerAnimation>();
	}

	/// <summary>
	/// トリガー当たり判定
	/// </summary>
	/// <param name="coll">当たったオブジェクト</param>
	private void OnTriggerEnter( Collider coll ) {
		// アイテム判定
		if( coll.gameObject.tag != "Item" ) return;
		// アイテム取得
		Item item = coll.gameObject.GetComponent<Item>();
		// 先に左取得
		if( _ItemL == null ) {
			// アイテムが使用状態判定
			if( !item.Chatch( Status._PlayerID ) ) return;
			_ItemL = item;
			_ItemL.HoldHand( _HandTrans );
			CSoundManager.Instance.PlaySE(AUDIO_LIST.OBTAIN);
			return;
		}
		// 右取得
		if( _ItemR == null ) {
			// アイテムが使用状態判定
			if( !item.Chatch( Status._PlayerID ) ) return;
			_ItemR = item;
			_ItemR.HoldHand( _HandTrans );
			CSoundManager.Instance.PlaySE( AUDIO_LIST.OBTAIN );
			return;
		}
	}
	#endregion MonoBehaviour Event
}



/*
 *	▼ End of File
*/