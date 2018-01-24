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
    
    [SerializeField]
    GameObject[] player = new GameObject[4];    //  タイトルのプレイヤー

	[Header("モニター1")]
    [SerializeField]
    Monitor matchMoni_1;
	[Header("モニター2")]
    [SerializeField]
    Monitor matchMoni_2;
	[Header("モニター3")]
    [SerializeField]
    Monitor matchMoni_3;
	[Header("モニター4")]
    [SerializeField]
    Monitor matchMoni_4;


    //  参加表明したか
    public bool IsJoinedPlayer_1 = false;
    public bool IsJoinedPlayer_2 = false;
    public bool IsJoinedPlayer_3 = false;
    public bool IsJoinedPlayer_4 = false;

    // Use this for initialization
    void Start(){
        //	カメラのアニメーターを取得
        cameraAnim = rendererCamera.GetComponent<Animator>();
        
        //  NULLチェック
        if(titleCanvas == null) Debug.LogError("titleCanvasがアタッチされていませんが。");
        if(matchingCanvas == null) Debug.LogError("matchingCanvasがアタッチされていませんが。");

        titleCanvas.SetActive(true);
        matchingCanvas.SetActive(false);
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

    //  ----タイトル＆セレクト　各状態のUpdate----------------------------------------------------------------------------------------------------
    /// <summary>
    /// タイトルステートの時の更新
    /// </summary>
    private void TitleSceneUpdate(){
        
        //Debug.Log("AnimeTime: " + cameraAnim.GetCurrentAnimatorStateInfo(0).normalizedTime);
        //  4つのうちのどれかの入力あれば
        if(AnyStartButton()){
            //  アニメーション開始
            cameraAnim.SetTrigger("PushButton");

            // //  UI切替
            TitleUIsSetActive(false);
            // MatchingUIsSetActive(true);

            StartCoroutine("WaitEndCameraAnime");
        }
        
    }


    /// <summary>
    /// 参加待ちステートの時の更新
    /// </summary>
    private void MatchingSceneUpdate(){

        //  入力受付
        //  参加待ち
        WaitInputA();
        //  キャンセル待ち
        WaitInputB();

        //  OKUIの表示
        ShowOKUI();

        //  ゲーム開始可能UI
        ShowStartUI();

        //  ゲーム開始可能人数に達しているとき
        if(MeetUp()){
            //  STARTボタンの受付
            if(InputGame.GetStartButton(1)){
                PlayerOut();
				// TODO : シーン変更
				CSceneManager.Instance.LoadScene(SCENE.GAME, FADE.Fade_1);
            }
            //  シーン変更？
        }
    }

    
    //  ----プレイヤーの動作関係-----------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// ４プレイヤーの入場動作開始
    /// </summary>
    private bool PlayerStart(){
        player[0].GetComponent<PlayerTitle>().InPlayer();
        player[1].GetComponent<PlayerTitle>().InPlayer();
        player[2].GetComponent<PlayerTitle>().InPlayer();
        player[3].GetComponent<PlayerTitle>().InPlayer();

        return true;
    }


    /// <summary>
    /// 退場
    /// </summary>
    private void PlayerOut(){
        player[0].GetComponent<PlayerTitle>().OutPlayer(new Vector3(-93.0F, 95.0F, -50.0F));
        player[1].GetComponent<PlayerTitle>().OutPlayer(new Vector3(-93.0F, 89.0F, -50.0F));
        player[2].GetComponent<PlayerTitle>().OutPlayer(new Vector3(-81.0F, 95.0F, -50.0F));
        player[3].GetComponent<PlayerTitle>().OutPlayer(new Vector3(-81.0F, 89.0F, -50.0F));
    }

    //  ----キャンバスのアクティブ操作-------------------------------------------------------------------------------------------------------------
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
    /// OKのUIを参加状態のフラグを元に表示
    /// </summary>
    public void ShowOKUI(){
        matchMoni_1.ShowOKSeal(IsJoinedPlayer_1, IsJoinedPlayer_2, IsJoinedPlayer_3, IsJoinedPlayer_4);
        matchMoni_2.ShowOKSeal(IsJoinedPlayer_1, IsJoinedPlayer_2, IsJoinedPlayer_3, IsJoinedPlayer_4);
        matchMoni_3.ShowOKSeal(IsJoinedPlayer_1, IsJoinedPlayer_2, IsJoinedPlayer_3, IsJoinedPlayer_4);
        matchMoni_4.ShowOKSeal(IsJoinedPlayer_1, IsJoinedPlayer_2, IsJoinedPlayer_3, IsJoinedPlayer_4);
    }


    /// <summary>
    /// 参加者の数をもとにゲーム開始可能ラベルの表示
    /// </summary> 
    private void ShowStartUI(){
        matchMoni_1.ShowStartLabel(MeetUp() && IsJoinedPlayer_1);
        matchMoni_2.ShowStartLabel(MeetUp() && IsJoinedPlayer_2);
        matchMoni_3.ShowStartLabel(MeetUp() && IsJoinedPlayer_3);
        matchMoni_4.ShowStartLabel(MeetUp() && IsJoinedPlayer_4);
    }



    //  ----入力関係-------------------------------------------------------------------------------------------------------------------------


    /// <summary>
    /// Aボタンの入力チェック（個別）
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
        if(p1_A && !IsJoinedPlayer_1){
            //  参加した
            IsJoinedPlayer_1 = true;
            //JumpSceneData.Instance.JointPlayer(1);

            //  UIの表示切替
            matchMoni_1.SetActiveJoined(true);
            matchMoni_1.SetActiveNotJoined(false);
        }
        //  2PのA入力があったら
        if(p2_A && !IsJoinedPlayer_2){
            //  参加した
            IsJoinedPlayer_2 = true;
            //JumpSceneData.Instance.JointPlayer(2);

            //  UIの表示切替
            matchMoni_2.SetActiveJoined(true);
            matchMoni_2.SetActiveNotJoined(false);
        }
        //  3PのA入力があったら
        if(p3_A && !IsJoinedPlayer_3){
            //  参加した
            IsJoinedPlayer_3 = true;
            //JumpSceneData.Instance.JointPlayer(3);

            //  UIの表示切替
            matchMoni_3.SetActiveJoined(true);
            matchMoni_3.SetActiveNotJoined(false);
        }
        //  4PのA入力があったら
        if(p4_A && !IsJoinedPlayer_4){
            //  参加した
            IsJoinedPlayer_4 = true;
            //JumpSceneData.Instance.JointPlayer(4);

            //  UIの表示切替
            matchMoni_4.SetActiveJoined(true);
            matchMoni_4.SetActiveNotJoined(false);
        }

    }
    
    
    /// <summary>
    /// Bボタンの入力チェック（個別）
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
        if(p1_B && IsJoinedPlayer_1){
            //  やっぱやめた
            IsJoinedPlayer_1 = false;
            //JumpSceneData.Instance.OutPlayer(1);
            
            //  UIの表示切替
            matchMoni_1.SetActiveJoined(false);
            matchMoni_1.SetActiveNotJoined(true);
        }
        //  2PのB入力があったら
        if(p2_B && IsJoinedPlayer_2){
            //  やっぱやめた
            IsJoinedPlayer_2 = false;
            //JumpSceneData.Instance.OutPlayer(2);

            //  UIの表示切替
            matchMoni_2.SetActiveJoined(false);
            matchMoni_2.SetActiveNotJoined(true);
        }
        //  3PのB入力があったら
        if(p3_B && IsJoinedPlayer_3){
            //  やっぱやめた
            IsJoinedPlayer_3 = false;
            //JumpSceneData.Instance.OutPlayer(3);

            //  UIの表示切替
            matchMoni_3.SetActiveJoined(false);
            matchMoni_3.SetActiveNotJoined(true);
        }
        //  4PのB入力があったら
        if(p4_B && IsJoinedPlayer_4){
            //  やっぱやめた
            IsJoinedPlayer_4 = false;
            //JumpSceneData.Instance.OutPlayer(4);

            //  UIの表示切替
            matchMoni_4.SetActiveJoined(false);
            matchMoni_4.SetActiveNotJoined(true);
        }
    }

    /// <summary>
    /// STARTボタンの入力チェック（全プレイヤーのどれか）
    /// </summary>
    private bool AnyStartButton(){
        //1P A
        var p1_S = InputGame.GetStartButton(1);
        //2P A
        var p2_S = InputGame.GetStartButton(2);
        //3P A
        var p3_S = InputGame.GetStartButton(3);
        //4P A
        var p4_S = InputGame.GetStartButton(4);

        return (p1_S || p2_S || p3_S || p4_S);
    }
    
    /// <summary>
    /// Aボタンの入力チェック（全プレイヤーのどれか）
    /// </summary>
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


    /// <summary>
    /// Bボタンの入力チェック（全プレイヤーのどれか）
    /// </summary>
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
    /// カメラのアニメーション終了待ち     タイトル -> マッチング
    /// </summary>
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
        titleCanvas.SetActive(false);
        MatchingUIsSetActive(true);
        PlayerStart();

        // /Debug.Log("State changed to Select");
        
    }



    /// <summary>
    /// ゲーム開始可能人数集まりました。
    /// </summary>
    private bool MeetUp(){
        int join_cnt = 0;
        if(IsJoinedPlayer_1) join_cnt += 1;
        if(IsJoinedPlayer_2) join_cnt += 1;
        if(IsJoinedPlayer_3) join_cnt += 1;
        if(IsJoinedPlayer_4) join_cnt += 1;

        return (join_cnt > 1);
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
