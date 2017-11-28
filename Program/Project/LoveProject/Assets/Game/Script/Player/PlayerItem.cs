/*
 *	▼ File		PlayerItem.cs
 *	
 *	▼ Brief	説明
 *	
 *	▼ Author	Show Kondou
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// PlayerItemクラス
/// </summary>
public class PlayerItem : ObjectTime {

	// メンバー
	#region Member
	private uint    _PlayerID;
	private int		_HitPoint;
	private Player.PlayerState _State;
	// 各アイテムのキャッシュ
	private Item    _ItemL;
	private Item    _ItemR;
	#endregion Member



	// アクセサ
	#region Accessor
	/// <summary>
	/// プレイヤーID
	/// </summary>
	public uint PlayerID {
		set { _PlayerID = value; }
	}
	/// <summary>
	/// アイテム左
	/// </summary>
	public Item ItemL {
		get { return _ItemL; }
		set { _ItemL = value; }
	}
	/// <summary>
	/// アイテム右
	/// </summary>
	public Item ItemR {
		get { return _ItemR; }
		set { _ItemR = value; }
	}
	public int HitPoint {
		get { return _HitPoint; }
		set { _HitPoint = value; }
	}
	/// <summary>
	/// 状態
	/// </summary>
	public Player.PlayerState State {
		set { _State = value; }
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
	}

	/// <summary>
	/// アイテム発動
	/// </summary>
	private void ActionItem() {
		// 左アイテムアクション
		if( InputGame.GetPlayerItemL( _PlayerID ) ) {
			if( _ItemL == null ) return;
			_ItemL.Action();
			// アイテム終
			if( _ItemL.IsBreak )  _ItemL = null;
			Debug.Log( "ActionItem" );
		}
		// 右アイテムアクション
		else if ( InputGame.GetPlayerItemR( _PlayerID ) ) {
			if( _ItemR == null ) return;
			_ItemR.Action();
			if( _ItemR.IsBreak )
				_ItemR = null;
			Debug.Log( "ActionItem" );
		}
	}

	/// <summary>
	/// アイテム食べる
	/// </summary>
	private void EatItem() {
		// 左アイテム食べる
		if ( InputGame.GetPlayerEatL( _PlayerID ) ) {
			if ( _ItemL == null ) return;
			_HitPoint += _ItemL.EatItem();
			_ItemL = null;
		}
		// 右アイテム食べる
		else if ( InputGame.GetPlayerEatR( _PlayerID ) ) {
			if ( _ItemR == null ) return;
			_HitPoint += _ItemR.EatItem();
			_ItemR = null;
		}
	}
	#endregion Method



	// イベント
	#region MonoBehaviour Event
	/// <summary>
	/// トリガー当たり判定
	/// </summary>
	/// <param name="coll">当たったオブジェクト</param>
	private void OnTriggerEnter( Collider coll ) {
		if( coll.gameObject.tag != "Item" ) return;
		Item item = coll.gameObject.GetComponent<Item>();
		// TODO:手の位置に持ってくる
		if( _ItemL == null ) {
			_ItemL = item;
			_ItemL.Chatch( _PlayerID );
			return;
		}
		if( _ItemR == null ) {
			_ItemR = item;
			_ItemL.Chatch( _PlayerID );
			return;
		}
	}
	#endregion MonoBehaviour Event
}



/*
 *	▼ End of File
*/