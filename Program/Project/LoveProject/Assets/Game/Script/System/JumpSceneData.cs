/*
 *	▼ File		JumpSceneData.cs
 *	
 *	▼ Brief	説明
 *	
 *	▼ Author	Show Kondou
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// NewBehaviourScriptクラス
/// </summary>
public class JumpSceneData : MonoBehaviour {


	#region Singleton
	// インスタンス
	static public JumpSceneData _Instance = null;
	// インスタンスのアクセサ
	static public JumpSceneData Instance {
		get {
			if( _Instance == null ) {
				var obj = FindObjectOfType<JumpSceneData>();
				_Instance = obj;// Define.NullCheck(obj);
				DontDestroyOnLoad( _Instance );
				return _Instance;
			}
			return _Instance;
		}
	}
	#endregion Singleton



	// 定数
	#region Constant
	#endregion Constant

	// メンバー
	#region Member
	// 参加プレイヤー
	private bool[] _JoinPlayer = new bool[4] { true, true, true, true };
	// 各プレイヤーがキルしたプレイヤー
	private bool[] _KillPlayer1 = new bool[4];
	private bool[] _KillPlayer2 = new bool[4];
	private bool[] _KillPlayer3 = new bool[4];
	private bool[] _KillPlayer4 = new bool[4];
	// 各プレイヤーの最終体力
	private int[] _EndPlayerHP = new int[4] {100,70,50,20};

	#endregion Member

	// アクセサ
	#region Accessor


	#endregion Accessor

	// メソッド
	#region Method
	/// <summary>
	/// 参加プレイヤー
	/// </summary>
	/// <param name="num"></param>
	public void JointPlayer(uint num) {
		_JoinPlayer[num] = true;
	}

	/// <summary>
	/// 不参加プレイヤー
	/// </summary>
	/// <param name="num"></param>
	public void OutPlayer(uint num) {
		_JoinPlayer[num] = false;
	}


	public bool GetJointPlayerNum( uint num ) {
		return _JoinPlayer[num - 1];
	}

	/// <summary>
	/// キルデータ保存
	/// </summary>
	/// <param name="kill">キルしたプレイヤー</param>
	/// <param name="killed">殺された雑魚</param>
	public void KillPlayer( uint kill, uint killed ) {
		switch( kill ) {
		case 1:
			_KillPlayer1[killed - 1] = true;
			break;
		case 2:
			_KillPlayer2[killed - 1] = true;
			break;
		case 3:
			_KillPlayer3[killed - 1] = true;
			break;
		case 4:
			_KillPlayer4[killed - 1] = true;
			break;
		}
	}

	public bool[] GetKillPlayer( uint num ) {
		switch( num ) {
		case 1: return _KillPlayer1;
		case 2: return _KillPlayer2;
		case 3: return _KillPlayer3;
		case 4: return _KillPlayer4;
		default: return null;
		}
	}

	// 最終体力保存
	public void EndPlayerHP( uint playerNum, int hp ) {
		_EndPlayerHP[playerNum] = hp;
	}

	public int GetEndPlayerHP( uint num ) {
		return _EndPlayerHP[num];
	}


	#endregion Method

	// イベント
	#region MonoBehaviour Event
	// /// <summary>
	// /// 初期化
	// /// </summary>
	// private void Start() { }

	// /// <summary>
	// /// 更新
	// /// </summary>
	// private void Update() { }



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

	// /// <summary>
	// /// 当たり判定
	// /// </summary>
	// /// <param name="coll">当たったオブジェクト</param>
	// private void OnCollisionXXX( Collision coll ) { }

	// /// <summary>
	// /// トリガー当たり判定
	// /// </summary>
	// /// <param name="coll">当たったオブジェクト</param>
	// private void OnTriggerXXX( Collider coll ) { }

	#endregion MonoBehaviour Event
}



/*
 *	▼ End of File
*/
