using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBase : MonoBehaviour {

	// 各オブジェクトごとにヒットストップを行うため
	private float deltaTime;		// 各オブジェクトのデルタタイム
	private float timeScale = 1.0F; // 各オブジェクトのタイムスケール

	/// <summary>
	/// 各オブジェクトのデルタタイムのアクセサ
	/// </summary>
	public float DeltaTime {
		get { return deltaTime * timeScale; }
	}

	/// <summary>
	/// 各オブジェクトのタイムスケールのアクセサ
	/// set でデルタタイムを更新する
	/// </summary>
	public float TimeScale {
		get { return timeScale; }
		set { timeScale = value; }
	}
	

	/// <summary>
	/// 更新関数
	/// </summary>
	protected virtual void Execute() { }

	/// <summary>
	/// Unityのイベント（更新）
	/// </summary>
	void Update() {
		deltaTime = Time.deltaTime;	// デルタタイム反映
		Execute();
	}
}


// TODO : ObjectBase のインタフェースを制作する
