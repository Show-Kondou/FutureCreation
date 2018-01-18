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
    GameObject titleCanvas = null;  //  タイトル画面のキャンバス
    [SerializeField]
    GameObject matchingCanvas = null;  //  参加待ち画面のキャンバス
    
    //  参加表明したか
    bool IsJoinedPlayer_1 = false;
    bool IsJoinedPlayer_2 = false;
    bool IsJoinedPlayer_3 = false;
    bool IsJoinedPlayer_4 = false;

    // Use this for initialization
    void Start(){
        //	カメラのアニメーターを取得
        cameraAnim = rendererCamera.GetComponent<Animator>();
        
        //  NULLチェック
        if(titleCanvas == null) Debug.LogError("titleCanvasがアタッチされていませんが。");
        if(matchingCanvas == null) Debug.LogError("matchingCanvasがアタッチされていませんが。");
	}


	
    // Update is called once per frame
    void Update(){

        switch(currentState){
        case StateInTitle.Title:
            TitleSceneUpdate();
        break;

        case StateInTitle.Select:
            MatchingSceneUpdate();
        break;
        }

    }



    /// <summary>
    /// タイトルステートの時の更新
    /// </summary>
    private void TitleSceneUpdate(){
        
        //Debug.Log("AnimeTime: " + cameraAnim.GetCurrentAnimatorStateInfo(0).normalizedTime);
        //  4つのうちのどれかの入力あれば
        if(AnyInputA()){
            //  アニメーション開始
            cameraAnim.SetTrigger("PushButton");

            // //  UI切替
            TitleUIsSetActive(false);
            // MatchingUIsSetActive(true);

            StartCoroutine("WaitEndCameraAnime");
        }
        
    }

    IEnumerator WaitEndCameraAnime(){

        //  アニメーションステートの切り替わり待ち
        while(cameraAnim.GetCurrentAnimatorStateInfo(0).IsName("New State 0") == false){
            yield return null;
        }

        //  アニメーション終了待ち
        while(cameraAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1){
            yield return null;
        }

        //  ステート切替
        currentState = StateInTitle.Select;
            //  UI切替
            //TitleUIsSetActive(false);
            MatchingUIsSetActive(true);
        Debug.Log("State changed to Select");
        
    }



    /// <summary>
    /// 参加待ちステートの時の更新
    /// </summary>
    private void MatchingSceneUpdate(){
        
    }


	/// <summary>
	/// 全画面のタイトルUIのアクティブフラグ操作
	/// </summary>
	/// <param name="value"></param>
	public void TitleUIsSetActive(bool value){
		foreach(GameObject obj in titleUIs) {
			obj.SetActive(value);
		}
        //titleCanvas.SetActive(value);
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


    private bool AnyInputA(){
        //1P A
        var p1_A = InputGame.GetPlayerJump(1);
        //2P A
        var p2_A = InputGame.GetPlayerJump(2);
        //3P A
        var p3_A = InputGame.GetPlayerJump(3);
        //4P A
        var p4_A = InputGame.GetPlayerJump(4);

        return (p1_A || p2_A || p3_A || p4_A);
    }


    private bool AnyInputB(){
        //1P B
        var p1_B = InputGame.GetPlayerRoll(1);
        //2P B
        var p2_B = InputGame.GetPlayerRoll(2);
        //3P B
        var p3_B = InputGame.GetPlayerRoll(3);
        //4P B
        var p4_B = InputGame.GetPlayerRoll(4);

        return (p1_B || p2_B || p3_B || p4_B);
    }
    

    /// <summary>
    /// 4プレイヤー分のAボタン入力を取得
    /// </summary>
    private void WaitInputA(){

        //1P A
        var p1_A = InputGame.GetPlayerJump(1);
        //2P A
        var p2_A = InputGame.GetPlayerJump(2);
        //3P A
        var p3_A = InputGame.GetPlayerJump(3);
        //4P A
        var p4_A = InputGame.GetPlayerJump(4);

        //  1PのA入力があったら
        if(p1_A){
            //  UIの表示切替
        }
        //  2PのA入力があったら
        if(p2_A){
            //  UIの表示切替
        }
        //  3PのA入力があったら
        if(p3_A){
            //  UIの表示切替
        }
        //  4PのA入力があったら
        if(p4_A){
            //  UIの表示切替
        }

    }
    /// <summary>
    /// 4プレイヤー分のBボタン入力を取得
    /// </summary>
    private void WaitInputB(){
        //1P B
        var p1_B = InputGame.GetPlayerRoll(1);
        //2P B
        var p2_B = InputGame.GetPlayerRoll(2);
        //3P B
        var p3_B = InputGame.GetPlayerRoll(3);
        //4P B
        var p4_B = InputGame.GetPlayerRoll(4);
        
        //  1PのB入力があったら
        if(p1_B){
            //  UIの表示切替
        }
        //  2PのB入力があったら
        if(p2_B){
            //  UIの表示切替
        }
        //  3PのB入力があったら
        if(p3_B){
            //  UIの表示切替
        }
        //  4PのB入力があったら
        if(p4_B){
            //  UIの表示切替
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
