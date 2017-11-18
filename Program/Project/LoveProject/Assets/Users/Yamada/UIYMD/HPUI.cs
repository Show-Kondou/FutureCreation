using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HPUI : MonoBehaviour {

	//	表示用オブジェクトのプレハブ
	[SerializeField]private Sprite[] spriteNumbers = new Sprite[10];//0~9で10個
	[SerializeField]private Sprite[] spriteGauge = new Sprite[2];	//ゲージ枠、中で二つ。	

	//	子UIのリスト
	private List<Image> childList = new List<Image>();

	private Image gaugeImage;	//	ゲージUI 中

	[Range(0,100)]
	public int playerHp = 100;
	private int prevPlayerHp = 0;



	/// <summary>
	/// 
	/// </sammary>
	void Start(){

		//	子全取得
		foreach (Transform child in transform){
			childList.Add(child.GetComponent<Image>());
			Debug.Log(child.name);
		}

		gaugeImage = transform.Find("GaugeIn").gameObject.GetComponent<Image>();

	}


	/// <summary>
	/// 
	/// </sammary>
	void Update(){

		//	プレイヤーのHP取得
		playerHp = Mathf.Abs((int)(100 * Mathf.Sin(Time.time))) + 1;

		//	変更があったときのみ更新
		if(prevPlayerHp == playerHp) return;
		prevPlayerHp = playerHp;

		//	数字UI更新
		HpNumberUpdate(playerHp);

		//	ゲージUI更新
		GaugeUpdate(playerHp);
	}


	/// <summary>
	/// 
	/// </sammary>
	private void HpNumberUpdate(int _Hp){

		//	プレイヤーから取得したHPを、数字UIに反映
		int _temp = _Hp;	//	セーブ
		int index = 0;		//	ループカウンタ
		int check_value = 100;	//	前ゼロ消しの判定境界値
		
		//	数値からスプライト番号割り出しループ
		for(index = 0; index < 3; index++){
			childList[index].sprite = spriteNumbers[_Hp % 10];
			_Hp /= 10;
		}

		//	前ゼロを消すためのやつ
		//	二つしか見ないからループにする必要ないかも
		for(index = 2; index >= 1; index --){
			var col = childList[index].color;
			if(_temp >= check_value) col.a = 1;
			else col.a = 0;
			check_value /= 10;
			childList[index].color = col;
		}
		// //	前ゼロ消し
		// if(_temp >= 10) childList[1].color = new Color(1,1,1,1);
		// else	childList[1].color = new Color(0,0,0,0);

		// if(_temp >= 100) childList[2].color = new Color(1,1,1,1);
		// else	childList[2].color = new Color(0,0,0,0);
	}



	/// <summary>
	/// 
	/// </sammary>
	private void GaugeUpdate(int _Hp){
		gaugeImage.fillAmount = _Hp * 0.01f;
	}
}
