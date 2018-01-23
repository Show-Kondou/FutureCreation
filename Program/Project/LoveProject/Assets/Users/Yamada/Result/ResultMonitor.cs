using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultMonitor : MonoBehaviour {

	[SerializeField]
	GameObject playerAnim;

	[SerializeField]
	GameObject rankingUI;

	/// <summary>
	///	演出のやつ表示切替
	/// </summary>
	public void ShowPlayerAnim(bool value){
		playerAnim.SetActive(value);
	}


	/// <summary>
	///	ランキングのやつ表示切替
	/// </summary>
	public void ShowRankingUI(bool value){
		rankingUI.SetActive(value);
	}
}
