using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectBase : MonoBehaviour {

	#region Member

	// 各オブジェクトごとにヒットストップを行うため
	private float _deltaTime;		// 各オブジェクトのデルタタイム
	private float _timeScale = 1.0F; // 各オブジェクトのタイムスケール

	private Vector3 _NextPosition;  // 次のポジション
	// [Header("スムーズ値"),SerializeField]
	private float   _Smooth = 0.1F;	// 滑らかに動かすようにする;

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

	/// <summary>
	/// 滑らか移動変数
	/// </summary>
	public Vector3 NextPosition {
		get { return _NextPosition; }
		set { _NextPosition = value; }
	}

	#endregion Accessor


	/// <summary>
	/// 更新関数
	/// </summary>
	protected virtual void Execute() { }

	/// <summary>
	/// 初期化
	/// </summary>
	private void Awake() {
		_NextPosition = transform.position;
	}

	/// <summary>
	/// Unityのイベント（更新）
	/// </summary>
	void Update() {
		// デルタタイム反映
		_deltaTime = Time.deltaTime;

		// 各更新関数
		Execute();

		// 滑らか移動
		var nowPos = transform.position;
		transform.position += (_NextPosition - nowPos) * _Smooth;
	}
}


// TODO : ObjectBase のインタフェースを制作する
