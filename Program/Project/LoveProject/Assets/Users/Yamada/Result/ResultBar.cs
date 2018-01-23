using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Data = JumpSceneData;


public class ResultBar : MonoBehaviour {

	[SerializeField]
	Image playerNum;		//	プレイヤー番号スプライト
	private int playerHp; 	//	プレイヤーの体力
	private uint pNum;		//	プレイヤー番号

	[SerializeField]
	private uint monitorNum;	//	表示モニター番号

	[SerializeField]
	Image bar;	//	背景のバー

	[SerializeField]
	MaskGauge gauge;	//	体力ゲージUI
	[SerializeField]
	Image[] digit = new Image[3];	//	体力数値
	
	[SerializeField]
	Image[] killedPlayerIcon = new Image[3];	//	自身以外のキャラアイコン３つ

	// // Use this for initialization
	// void Start () {
	// 	//	HP取得
	// 	//playerHp = Data.Instance.GetEndPlayerHP(playerNum);
	// 	playerHp = 100;
	// }
	
	// // Update is called once per frame
	// void Update () {
		
	// 	//	残りHPを反映
	// 	// gauge.TransUV(playerHp);
	// 	// HpNumberUpdate(playerHp);
	// }

	/// <summary>
	///
	/// </summary>
	public void SetKilledPlayer(bool[] _killed){
		//	アイコン走査
		int icon_num = 0;

		//	殺したフラグ配列ループ
		for(int i = 0; i < 4; i++){
			
			//	自分は殺せない
			if(pNum == (i + 1)) continue;

			//	殺した
			if(_killed[i]){
				killedPlayerIcon[icon_num].color = new Color(1.0F, 1.0F, 1.0F);
			}
			//	殺してない
			else{
				killedPlayerIcon[icon_num].color = new Color(0.3F, 0.3F, 0.3F);
			}
			icon_num++;
		}
	}

	/// <summary>
	///
	/// </summary>
	public void SetBarImage(uint pnum){
		if(pnum == monitorNum){
			bar.sprite = Result.Instance.GetBarSprite(1);
		}else{
			bar.sprite = Result.Instance.GetBarSprite(0);
		}
	}

	/// <summary>
	///
	/// </summary>
	public void SetPlayerNum(uint pnum){
		pNum = pnum;
		playerNum.sprite = Result.Instance.GetPlayerNumSprite(pnum);
	}


	/// <summary>
	///	体力情報を表示
	/// </summary>
	public void SetHp(int hp){
		//	マイナス値は処理しないお
		if(hp < 0) return;

		//	残りHPを反映
		gauge.TransUV(hp);
		HpNumberUpdate(hp);
	}
	
	/// <summary>
	/// 
	/// </sammary>
	private void HpNumberUpdate(int _Hp){

		//	プレイヤーから取得したHPを、数字UIに反映
		int _temp = _Hp;	//	セーブ
		int _index = 0;		//	ループカウンタ
		int check_value = 100;	//	前ゼロ消しの判定境界値
		
		//	数値からスプライト番号割り出しループ
		for(_index = 0; _index < 3; _index++){
			digit[_index].sprite = Result.Instance.GetNumberSprite(_Hp % 10);
			_Hp /= 10;
		}

		//	前ゼロを消すためのやつ
		//	二つしか見ないからループにする必要ないかも
		for(_index = 2; _index >= 1; _index --){
			var col = digit[_index].color;
			if(_temp >= check_value) col.a = 1;
			else col.a = 0;
			check_value /= 10;
			digit[_index].color = col;
		}
		// //	前ゼロ消し
		// if(_temp >= 10) childList[1].color = new Color(1,1,1,1);
		// else	childList[1].color = new Color(0,0,0,0);

		// if(_temp >= 100) childList[2].color = new Color(1,1,1,1);
		// else	childList[2].color = new Color(0,0,0,0);
	}
}
