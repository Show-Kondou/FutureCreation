using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour {

	[SerializeField]private Sprite[] spriteNumbers = new Sprite[10];//0~9で10個
	int prevSecond = 0;	//	前フレームの時間

	private List<Image> numberImage = new List<Image>();

	void Start(){
		//	子のImageを取得
		foreach(Transform child in transform){
			numberImage.Add(child.GetComponent<Image>());
		}

		//	スプライトの初期設定
		numberImage[0].sprite = spriteNumbers[0];
		numberImage[1].sprite = spriteNumbers[0];
		numberImage[2].sprite = spriteNumbers[3];
	}

	void Update(){

		int seconds = GameTimer.Instance.Second;

		//	前と違うときだけ更新
		if(prevSecond == seconds) return;
		prevSecond = seconds;	//	タイマー保存

		//	スプライト割り当て
		for(int index = 0; index < 2; index ++){
			numberImage[index].sprite = spriteNumbers[seconds % 10];
			seconds /= 10;
		}
		numberImage[2].sprite = spriteNumbers[GameTimer.Instance.Minute];
		
	}


}
