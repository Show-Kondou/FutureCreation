using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
///	スポットライトといいつつ違うこと少しやっちゃってる
/// </summary>
public class Spot : MonoBehaviour {

	IEnumerator coro;	//	コルーチン
	//	ライト投影角テーブル
	float[] stopAngle = {-30.0F,-10.0F,10.0F,30.0F};

	
	/// <summary>
	///	スポットライトの動き呼び出し
	/// </summary>
	public void StartLight(uint pnum){
		coro = RotateLight(pnum);
		StartCoroutine(coro);
	}


	/// <summary>
	///	スポットライトの停止
	/// </summary>
	public void StopLight(uint pnum){
		StopCoroutine(coro);
		var angle = transform.localEulerAngles;
		angle.z = stopAngle[pnum-1];
		transform.localEulerAngles = angle;


		//	ドラムロール停止

		//	ターン！！
		CSoundManager.Instance.PlaySE(AUDIO_LIST.BAAN);
		//	プレイヤー演出開始
		Result.Instance.PlayPlayerAnim();
		StartCoroutine("EndPlayerScene");
	}


	/// <summary>
	///	スポットライトの動きコルーチン
	/// </summary>
	private IEnumerator RotateLight(uint pnum){
		while (Time.timeScale != 1.0F) {
			Debug.Log( Time.timeScale );
			yield return null;
		}
		//	ドラムロール発動
		CSoundManager.Instance.PlaySE(AUDIO_LIST.DRAMROLL);
		StartCoroutine(StopLightCoroutine(pnum));

		while(true){
			var angle = transform.localEulerAngles;
			angle.z = 30 * Mathf.Cos(Time.time);
			transform.localEulerAngles = angle;

			yield return null;
		}
	}


	/// <summary>
	///	スポットライト停止コルーチン
	/// </summary>
	private IEnumerator StopLightCoroutine(uint pnum){

		yield return new WaitForSeconds(2.5f);

		StopLight(pnum);
		//	花吹雪発動
		Result.Instance.SetActiveParticle(true);

	}


	/// <summary>
	///	プレイヤーの演出終了コルーチン
	/// </summary>
	private IEnumerator EndPlayerScene(){

		yield return new WaitForSeconds(5.0f);
		Result.Instance.ShowRanking();
	}
}
