using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 各オブジェクトごとにTimeを使用するクラス
/// </summary>
abstract public class ObjectTime : MonoBehaviour {
	#region Member
	// 各オブジェクトごとにヒットストップを行うため
	private float	_deltaTime;       // 各オブジェクトのデルタタイム
	private float	_timeScale = 1.0F; // 各オブジェクトのタイムスケール
	#endregion Member


	#region Accessor
	/// <summary>
	/// 各オブジェクトのデルタタイムのアクセサ
	/// </summary>
	public float DeltaTime {
		get { return _deltaTime * _timeScale; }
	}

	/// <summary>
	/// 各オブジェクトのタイムスケールのアクセサ
	/// set でデルタタイムを更新する
	/// </summary>
	public float TimeScale {
		get { return _timeScale; }
		set { _timeScale = value; }
	}
	#endregion Accessor

	#region Method

	private void Update() {
		_deltaTime = Time.deltaTime;
		Execute();
	}

	abstract protected void Execute();

	#endregion MEthod

}

