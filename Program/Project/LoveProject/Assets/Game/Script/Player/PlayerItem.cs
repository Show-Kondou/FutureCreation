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

	// 定数
	#region Constant
	#endregion Constant

	// メンバー
	#region Member
	private uint    _PlayerID;

	private Item    _ItemL;
	private Item    _ItemR;
	#endregion Member

	// アクセサ
	#region Accessor
	public uint PlayerID {
		set { _PlayerID = value; }
	}

	public Item ItemL {
		get { return _ItemL; }
		set { _ItemL = value; }
	}
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
	}

	private void ActionItem() {
		if( InputGame.GetPlayerItemL( _PlayerID ) ) {
			if( _ItemL == null ) return;
			_ItemL.Action();
			Debug.Log( "ActionItem" );
		}else if( InputGame.GetPlayerItemR( _PlayerID ) ) {
			if( _ItemR == null ) return;
			_ItemR.Action();
			Debug.Log( "ActionItem" );
		}
	}

	#endregion Method

	// イベント
	#region MonoBehaviour Event
	/// <summary>
	/// 初期化
	/// </summary>
	private void Start() { }



	// /// <summary>
	// /// インスペクタの変更時イベント
	// /// </summary>
	// private void OnValidate() { }

	// /// <summary>
	// /// 先初期化
	// /// </summary>
	// private void Awake() { }

	// /// <summary>
	// /// 後更新
	// /// </summary>
	// private void LateUpdate() { }

	/// <summary>
	/// 当たり判定
	/// </summary>
	/// <param name="coll">当たったオブジェクト</param>
	//private void OnCollisionEnter( Collision coll ) {
	//	if( coll.gameObject.tag != "Item" )
	//		return;
	//	Debug.Log( "ItemHit" );
	//	if( _ItemL == null ) {
	//		_ItemL = coll.gameObject.GetComponent<Item>();
	//	} else if( _ItemR == null ) {
	//		_ItemR = coll.gameObject.GetComponent<Item>();
	//	}
	//}

	/// <summary>
	/// トリガー当たり判定
	/// </summary>
	/// <param name="coll">当たったオブジェクト</param>
	private void OnTriggerEnter( Collider coll ) {
		if( coll.gameObject.tag != "Item" ) return;
		Item item = coll.gameObject.GetComponent<Item>();
		if( _ItemL == null ) {
			_ItemL = item;
			// _ItemL.Catch();
			return;
		}
		if( _ItemR == null ) {
			_ItemR = item;
			return;
		}

		// 持ちきれなかったアイテムは何もしない
	}


	#endregion MonoBehaviour Event
}



/*
 *	▼ End of File
*/
