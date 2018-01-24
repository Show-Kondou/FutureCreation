/*
 *	▼ File		Fade.cs
 *	
 *	▼ Brief	説明
 *	
 *	▼ Author	Show Kondou
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Fadeクラス
/// </summary>
public class Fade : CFadeBase {

	// 定数
	#region Constant
	#endregion Constant

	// メンバー
	#region Member
	#endregion Member

	// アクセサ
	#region Accessor
	#endregion Accessor



	// ===== メソッド =====

	// //////////////////////////////
	//  [Start]
	//  ・スタート関数
	//    in ：none
	//    out：void
	//
	void Start() {
		FadeInit();
	}



	// //////////////////////////////
	//  [FadeInit]
	//  ・初期化関数
	//    in ：none
	//    out：void
	//
	public override void FadeInit() {
		base.FadeInit();
	}



	// //////////////////////////////
	//  [FadeOutUpdate]
	//  ・フェードアウト更新
	//    in ：none
	//    out：void
	//
	protected override void FadeOutUpdate() {
		base.FadeOutUpdate();
	}



	// //////////////////////////////
	//  [FadeInUpdate]
	//  ・フェードインの更新
	//    in ：none
	//    out：void
	//
	protected override void FadeInUpdate() {
		base.FadeInUpdate();
	}
}



/*
 *	▼ End of File
*/