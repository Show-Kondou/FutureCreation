using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// タイトル全体的なことする
/// </sammary>
public class Title : MonoBehaviour
{
	//	タイトルシーン内での状態
	enum StateInTitle{
		Title = 0,	//	タイトル状態
		Select,		//	セレクト状態
		MAx
	}
	StateInTitle currentState;

    //  タイトルシーンのカメラアニメーション用
    [SerializeField]
    private GameObject rendererCamera;  //  Animatorがコンポーネントされてるやつね
    [HideInInspector]
    public Animator cameraAnim; //  アクセスのためにパブリック

	//	タイトルシーンのみで使用するUI
	public GameObject[] titleUIs = new GameObject[4];	//	画面４つ分

    [SerializeField]
    GameObject matchingCanvas = null;  //  参 加待ち画面のキャンバス
    

    // Use this for initialization
    void Start(){
        //	カメラのアニメーターを取得
        cameraAnim = rendererCamera.GetComponent<Animator>();
        if(matchingCanvas == null) Debug.LogError("matchingCanvasがアタッチされていませんが。");
	}


	
    // Update is called once per frame
    void Update(){

        //	入力待ち
        WaitInput();

    }



	/// <summary>
	/// 全画面のタイトルUIのアクティブフラグ操作
	/// </summary>
	/// <param name="value"></param>
	public void TitleUIsSetActive(bool value){
		foreach(GameObject obj in titleUIs) {
			obj.SetActive(value);
		}
	}

	/// <summary>
	/// マッチング画面UIのアクティブフラグ操作
	/// </summary>
	/// <param name="value"></param>
	public void MatchingUIsSetActive(bool value){
		matchingCanvas.SetActive(value);
	}



    /// <summary>
    /// タイトルシーン内での入力待ち処理記述
    /// </sammary>
    private void WaitInput()
    {
        //TODO:	ゲームパッド用のインプットに変更する
        switch(currentState){
        case StateInTitle.Title: 
            //	参加受付画面に移行
            if (Input.GetKeyDown(KeyCode.Space)){
                //  アニメーション開始
                cameraAnim.SetTrigger("PushButton");
                //	タイトルUIを非表示に
                TitleUIsSetActive(false);
                //  マッチングキャンバスを表示
                MatchingUIsSetActive(true);
                //  マッチングステートへ
                currentState = StateInTitle.Select;
            }
        break;

        case StateInTitle.Select: 
            //	タイトル画面に移行
            if (Input.GetKeyDown(KeyCode.Backspace)) {
                //  アニメーション開始
                cameraAnim.SetTrigger("PushButton");
                //  マッチングキャンバスを非表示
                MatchingUIsSetActive(false);
                //	タイトルUIを表示
                TitleUIsSetActive(true);
                //  タイトルステートへ
                currentState = StateInTitle.Title;
            }
            break;
        }

    }


    #region Singleton
    static Title instance;
    public static Title Instance{
        get{
            if (!instance){
                instance = FindObjectOfType<Title>();
                if (!instance) instance = new GameObject("Title").AddComponent<Title>();
            }
            return instance;
        }
    }

    void Awake(){
        if (Instance && instance != this)
            Destroy(gameObject);
    }
    #endregion Singleton
}
