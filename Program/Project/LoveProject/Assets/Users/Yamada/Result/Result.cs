using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

using Data = JumpSceneData;

public class Result : MonoBehaviour {

	//	数字プレハブ
	[SerializeField]
	private Sprite[] spriteNumbers = new Sprite[10];//0~9で10個

    [SerializeField]
    private Sprite[] spritePlayerNumber = new Sprite[4];    //P1～P4 

    [Header("モニター１の分"),SerializeField]
    private GameObject[] resultBar = new GameObject[4]; //  結果表示バー４つ
    [Header("モニター２の分"),SerializeField]
    private GameObject[] resultBar2 = new GameObject[4]; //  結果表示バー４つ
    [Header("モニター３の分"),SerializeField]
    private GameObject[] resultBar3 = new GameObject[4]; //  結果表示バー４つ
    [Header("モニター４の分"),SerializeField]
    private GameObject[] resultBar4 = new GameObject[4]; //  結果表示バー４つ

    [SerializeField]
    private Sprite[] spriteBar = new Sprite[2];

    private uint joinedCount = 0;

    public int[] arrayHP = {100,72,-999,10};//new int[4];

    struct PlayerData{
        public uint    _num;       //  プレイヤー番号
        public int     _hp;        //  最終時体力
        public bool    _isJoined;  //  参加したか
        public bool[]  _killedPlayer;   //  倒したプレイヤー
    }

    PlayerData[] ranking = new PlayerData[4];

    [SerializeField]PlayerResult[] playerResult = new PlayerResult[4];
    [SerializeField]Spot[] spot = new Spot[4];

    [SerializeField]GameObject hubukiParticle;

    [SerializeField]ResultMonitor[] monitor = new ResultMonitor[4];

    bool[] hoge = new bool[]{false, false, false, false};

    [HideInInspector]
    public bool isShowRanking = false;

	// Use this for initialization
	void Start () {
        //  各モニターの表示を勝者演出にするお
        for(int i = 0; i < 4; i++){
            monitor[i].ShowPlayerAnim(true);
            monitor[i].ShowRankingUI(false);
        }
        //  紙吹雪オフ
        hubukiParticle.SetActive(false);

        //  念のため、一旦全部非表示
        for(int i = 0; i < 4; i++){
            resultBar[i].SetActive(false);
            resultBar2[i].SetActive(false);
            resultBar3[i].SetActive(false);
            resultBar4[i].SetActive(false);
        }

        //  参加人数取得
        joinedCount = 3;//CountPlayers();
        //  参加人数分再表示
        for(int cnt = 0; cnt < joinedCount; cnt++){
            resultBar[cnt].SetActive(true);
            resultBar2[cnt].SetActive(true);
            resultBar3[cnt].SetActive(true);
            resultBar4[cnt].SetActive(true);
        }

        //  参加したプレイヤーの順位付け
        RankingSort();


        for(int i = 0; i < 4; i++){
            spot[i].StartLight(ranking[0]._num);
            // /Debug.Log(ranking[i]._num + " : " + ranking[i]._hp + " : " + ranking[i]._killedPlayer[0] + ranking[i]._killedPlayer[1] + ranking[i]._killedPlayer[2] + ranking[i]._killedPlayer[3]);
        }
	}
	
	// Update is called once per frame
	void Update () {

        if(isShowRanking){
            if(AnyPlayerPushedStart()){
                Debug.LogError("ここでシーン変更呼ぶ");
            }
        }
	}  

    private bool AnyPlayerPushedStart(){
        return (InputGame.GetStartButton(1) || InputGame.GetStartButton(2) || InputGame.GetStartButton(3) || InputGame.GetStartButton(4));
    }
	
    /// <summary>
    /// ランキングセット
    /// </summary>
    public void ShowRanking(){

        //  表示
        for(int i = 0; i < 4; i++){
            monitor[i].ShowPlayerAnim(false);
            monitor[i].ShowRankingUI(true);
        }

        //  値をセットして
        for(int i = 0; i < 4; i++){
            resultBar[i].GetComponent<ResultBar>().SetHp(ranking[i]._hp);
            resultBar[i].GetComponent<ResultBar>().SetPlayerNum(ranking[i]._num);
            resultBar[i].GetComponent<ResultBar>().SetKilledPlayer(ranking[i]._killedPlayer);
            resultBar[i].GetComponent<ResultBar>().SetBarImage(ranking[i]._num);

            resultBar2[i].GetComponent<ResultBar>().SetHp(ranking[i]._hp);
            resultBar2[i].GetComponent<ResultBar>().SetPlayerNum(ranking[i]._num);
            resultBar2[i].GetComponent<ResultBar>().SetKilledPlayer(ranking[i]._killedPlayer);
            resultBar2[i].GetComponent<ResultBar>().SetBarImage(ranking[i]._num);

            resultBar3[i].GetComponent<ResultBar>().SetHp(ranking[i]._hp);
            resultBar3[i].GetComponent<ResultBar>().SetPlayerNum(ranking[i]._num);
            resultBar3[i].GetComponent<ResultBar>().SetKilledPlayer(ranking[i]._killedPlayer);
            resultBar3[i].GetComponent<ResultBar>().SetBarImage(ranking[i]._num);
            
            resultBar4[i].GetComponent<ResultBar>().SetHp(ranking[i]._hp);
            resultBar4[i].GetComponent<ResultBar>().SetPlayerNum(ranking[i]._num);
            resultBar4[i].GetComponent<ResultBar>().SetKilledPlayer(ranking[i]._killedPlayer);
            resultBar4[i].GetComponent<ResultBar>().SetBarImage(ranking[i]._num);
        }

        isShowRanking = true;
    }


    /// <summary>
    /// プレイヤーのアニメーション再生
    /// </summary>
    public void PlayPlayerAnim(){

        //  １位のやつはWin   それ以外はLoseを呼ぶ
        playerResult[ranking[0]._num - 1].WinPlayer();
        playerResult[ranking[1]._num - 1].LosePlayer();
        playerResult[ranking[2]._num - 1].LosePlayer();
        playerResult[ranking[3]._num - 1].LosePlayer();

    }



    /// <summary>
    /// 最終時の体力が多い順に、プレイヤーデータをソートするよ
    /// </summary>
    private void RankingSort(){
        //  最終体力    不参加は例外値999を入れる。
        for(uint ply = 1; ply <= 4; ply++){
            // if(Data.Instance.GetJointPlayerNum(ply) == false) {
            //     arrayHP[ply-1] = 999;
            // }
            // else{
            //     arrayHP[ply-1] = Data.Instance.GetEndPlayerHP(ply);
            // }
            ranking[ply-1]._hp = arrayHP[ply-1]; 
            ranking[ply-1]._num = ply;
            //ranking[ply-1]._killedPlayer = Data.Instance.GetKillPlayer(ply);
            ranking[ply-1]._killedPlayer = hoge;
        }


        //  構造体データソート用
        var ls = new List<PlayerData>();
        for(uint ply = 1; ply <= 4; ply++){
            ls.Add(ranking[ply-1]);
        }
        ls.Sort((a,b) => b._hp - a._hp);

        for(int i = 0; i < 4; i++)
            ranking[i] = ls[i];

        //  降順ソート
        // Array.Sort(arrayHP);
        // Array.Reverse(arrayHP); 
    }


    /// <summary>
    /// 参加人数を数える
    /// </summary>
    private uint CountPlayers(){
        uint count = 0;
        for(uint p = 1; p <= 4; p++){
            if(Data._Instance.GetJointPlayerNum(p)){
                count += 1;
            }
        }
        return count;
    }


    /// <summary>
    /// バーのスプライト取得
    /// </summary>
    public Sprite GetBarSprite(int num){
        return spriteBar[num];
    }

    /// <summary>
    /// P1～P4のスプライト取得
    /// </summary>
    public Sprite GetPlayerNumSprite(uint num){
        return spritePlayerNumber[num-1];
    }

    /// <summary>
    /// 数字スプライト取得
    /// </summary>
    public Sprite GetNumberSprite(int num){
        return spriteNumbers[num];
    }



    /// <summary>
    /// 紙吹雪のパーティクルのアクティブ操作
    /// </summary>
    public void SetActiveParticle(bool value){
        hubukiParticle.SetActive(value);
    }

    #region Singleton
    static Result instance;
    public static Result Instance{
        get{
            if (!instance){
                instance = FindObjectOfType<Result>();
                if (!instance) instance = new GameObject("Result").AddComponent<Result>();
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
