using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {

	[SerializeField]
	GameObject[] playerUI = new GameObject[4];


	
	// Use this for initialization
	void Awake () {
		if (Instance && instance != this)
			Destroy(gameObject);
	}
	


	/// <summary>
	///	UI全部表示
	/// </summary>
	public void ShowAll(){
		playerUI[0].SetActive(true);
		playerUI[1].SetActive(true);
		playerUI[2].SetActive(true);
		playerUI[3].SetActive(true);
	}


	/// <summary>
	///	UI全部非表示
	/// </summary>
	public void HideAll(){
		playerUI[0].SetActive(false);
		playerUI[1].SetActive(false);
		playerUI[2].SetActive(false);
		playerUI[3].SetActive(false);
	}

	/// <summary>
	///	指定のプレイヤー番号のUIを表示へ
	/// </summary>
	public void ShowGameUI(int player_num){
		playerUI[player_num].SetActive(true);
	}
	public void ShowGameUI(int p_num, int p_num2){
		playerUI[p_num].SetActive(true);
		playerUI[p_num2].SetActive(true);
	}
	public void ShowGameUI(int p_num, int p_num2, int p_num3){
		playerUI[p_num].SetActive(true);
		playerUI[p_num2].SetActive(true);
		playerUI[p_num3].SetActive(true);
	}



	/// <summary>
	///	指定のプレイヤー番号のUIを非表示へ
	/// </summary>
	public void HideGameUI(int player_num){
		playerUI[player_num].SetActive(false);
	}
	public void HideGameUI(int p_num, int p_num2){
		playerUI[p_num].SetActive(false);
		playerUI[p_num2].SetActive(false);
	}
	public void HideGameUI(int p_num, int p_num2, int p_num3){
		playerUI[p_num].SetActive(false);
		playerUI[p_num2].SetActive(false);
		playerUI[p_num3].SetActive(false);
	}

	//	シングルトン
	#region Singleton
        static GameUI instance;
        public static GameUI Instance{
            get{
                if (!instance){
                    instance = FindObjectOfType<GameUI>();
                    if (!instance)  instance = new GameObject("GameUI").AddComponent<GameUI>();
                }
                return instance;
            }
        }
	#endregion Singleton
}
