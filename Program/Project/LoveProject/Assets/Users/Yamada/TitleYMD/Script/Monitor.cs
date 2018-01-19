using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monitor : MonoBehaviour {

	[SerializeField]
	GameObject joinedUI;	//	参加をした時のUI

	[SerializeField]
	GameObject notJoinUI;	//	不参加の時のUI

	[SerializeField]
	GameObject ok_p1;	//	「OK」のUI　プレイヤー１
	[SerializeField]
	GameObject ok_p2;	//	「OK」のUI　プレイヤー2
	[SerializeField]
	GameObject ok_p3;	//	「OK」のUI　プレイヤー3
	[SerializeField]
	GameObject ok_p4;	//	「OK」のUI　プレイヤー4

	[SerializeField]
	GameObject startLabel;	//	ゲーム開始可能時イメージ

	// Use this for initialization
	void Start () {
		
	}

	/// <summary>
	///	参加状態のUI表示切替
	/// </summary>
	public void SetActiveJoined(bool value){
		joinedUI.SetActive(value);
	}


	/// <summary>
	///	不参加状態のUI表示切替	
	/// </summary>
	public void SetActiveNotJoined(bool value){
		notJoinUI.SetActive(value);
	}


	/// <summary>
	///	OKのUI表示切替
	/// </summary>
	public void ShowOKSeal(bool v1, bool v2, bool v3, bool v4){
		ok_p1.SetActive(v1);
		ok_p2.SetActive(v2);
		ok_p3.SetActive(v3);
		ok_p4.SetActive(v4);
	}



	/// <summary>
	///	ゲーム開始可能ラベル表示切替
	/// </summary>
	public void ShowStartLabel(bool value){
		startLabel.SetActive(value);
	}

}
