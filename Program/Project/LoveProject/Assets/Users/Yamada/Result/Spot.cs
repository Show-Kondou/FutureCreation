using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spot : MonoBehaviour {

	IEnumerator coro;	//	コルーチン
	//	ライト投影角テーブル
	float[] stopAngle = {-30.0F,-10.0F,10.0F,30.0F};

	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			StopLight(2);
		}
	}


	/// <summary>
	///
	/// </summary>
	public void StartLight(uint pnum){
		coro = RotateLight(pnum);
		StartCoroutine(coro);
	}


	/// <summary>
	///
	/// </summary>
	public void StopLight(uint pnum){
		StopCoroutine(coro);
		var angle = transform.localEulerAngles;
		angle.z = stopAngle[pnum-1];
		transform.localEulerAngles = angle;


		//	ドラムロール停止

		//	ターン！！
		CSoundManager.Instance.PlaySE(AUDIO_LIST.GO);
		Result.Instance.PlayPlayerAnim();
	}



	private IEnumerator RotateLight(uint pnum){
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


	private IEnumerator StopLightCoroutine(uint pnum){

		yield return new WaitForSeconds(7.99f);

		StopLight(pnum);
		//	花吹雪発動
		Result.Instance.SetActiveParticle(true);

	}
}
