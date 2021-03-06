﻿  //_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
//
//  [CAudioData]
//
//  ファイル名：CAudioData.cs
//  説　　　明：オーディオ関係のデータ
//  制　作　者：Show Kondou
//
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
//  - 更新履歴 -
//  2016  12/07　… 新規作成
//
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
using UnityEngine;
using System.Collections.Generic;


// ===== 定数 =====
public enum AUDIO_LIST {
	/* BGM */
	BGM_01,			// BGM
	BGM_02,			// BGM
	BGM_03,         // BGM
	BGM_T,			// Title
	BGM_R,			// Result

	/* SE */
	EAT,		// 食べる
	DRAMROLL,	//	ドラムロール
	BAAN,
	BREAK,		// 武器が壊れた
	COLLAPSE,	// 倒れた
	CURE,		// 回復
	DAMAGE,		// 攻撃が当たった
	EXPLOSION,  // 投げ物の爆発
	READY,	// レディ
	GO,		// ゲームスタート
	IN,		// 観戦中のカメラ移動
	NOHIT,	// ガード成功時
	OBTAIN,	// 拾う
	OPEN,	// ドアを開ける音
	ROLL,	// ローリング
	SHAKE,	// 切る、投げる
	STAND,	// ガード
	WALK,	// 走り、ジャンプ

	CANCEL,
	INPLAYER,
	LIGHTON,
	MOVE,
	RESULTITEM,
	START,


	/* 最大数 */
	MAX
}



// ======================================
//  CAudioData
// ======================================
public class CAudioData {
	// ===== メンバ =====
	// 基本ファイルパス
	static string FILE_PATH = "Audio/";// = "Assets/Resources/Audio/";

	// オーディオファイル名
	static List<string> m_FileName =  new List<string>{
		/* BGM */
		"BGM/bgm_01_ok",			// BGM
		"BGM/bgm_02",			// BGM
		"BGM/bgm_03",			// BGM
		"BGM/bgm_T",			// BGM
		"BGM/bgm_R",			// BGM
	
		/* SE */
		"SE/Eat",		// 食べる
		"SE/DramRoll",	// DramRoll
		"SE/go",	// Baan
		"SE/break",	// 武器が壊れた
		"SE/collapse",	// 倒れた
		"SE/cure",		// 回復
		"SE/damage",		// 攻撃が当たった
		"SE/explosion",	// 投げ物の爆発
		"SE/Ready",		// レディ
		"SE/Go",		// ゲームスタート
		"SE/in",		// 観戦中のカメラ移動
		"SE/nohit_02",	// ガード成功時
		"SE/obtain",	// 拾う
		"SE/open",	// ドアを開ける音
		"SE/roll",	// ローリング
		"SE/shake",	// 切る、投げる
		"SE/stand",	// ガード
		"SE/walk",	// 走り、ジャンプ

		"SE/Cancel",
		"SE/InPlayer",
		"SE/LightOn",
		"SE/Move",
		"SE/ResultItem",
		"SE/Start",
		null,
	};



	// ===== メソッド =====

	// //////////////////////////////
	//  [CAudioData]
	//  ・コンストラクタ
	//    in ：none
	//    out：none
	//
	CAudioData() {
		if( m_FileName.Count != (int)AUDIO_LIST.MAX ) {
			Debug.Log( "オーディオのファイル数と定数が一致しませんでした。" );
			return;
		}
	}




	// //////////////////////////////
	//  [GetFileName]
	//  ・ファイル名取得
	//    in ：AUDIO_LIST
	//    out：string
	//
	static public string GetFileName( AUDIO_LIST value ) {
		return FILE_PATH + m_FileName[(int)value];
	}
	// int
	static public string GetFileName( int value ) {
		return FILE_PATH + m_FileName[value];
	}

}





