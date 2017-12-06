using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {

	[Header("ポップポイント"), HideInInspector]
	public List<StagePop> popPoint = new List<StagePop>();	//	ポップする場所オブジェ

	//	ステージ配置可能アイテム数
	private const int MaxPopItemNum = 15;

	[Header("生成間隔(秒)"), SerializeField]
	private uint limitTime;	//	生成間隔

	private float timer = 0.0F;	//	タイマのカウンター

	private uint sumPriority = 0;

	private List<float> percent = new List<float>();	//	％変換後の箱


	#region Singleton
        static StageManager instance;
        public static StageManager Instance{
            get{
                if (!instance){
                    instance = FindObjectOfType<StageManager>();
                    if (!instance)  instance = new GameObject("StageManager").AddComponent<StageManager>();
                }
                return instance;
            }
        }
		
		//	Awakeより前に呼ばれる
		// [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		// static void OnBeforeSceneLoadRuntimeMethod (){
		// }


		void Awake(){
			if (Instance && instance != this)
				Destroy(gameObject);
		}
	#endregion Singleton

	// Use this for initialization
	void Start () {
		
		foreach(StagePop p  in popPoint){
			sumPriority += p.popPriority;
		}

		ConvPercent();
	}
	
	// Update is called once per frame
	void Update () {
		
		timer += Time.deltaTime;
		if(timer > limitTime){
			timer = 0;
			Pop();
		}

	}



	/// <summary>
	/// 
	/// </sammary>
	private void Pop(){

        //  ステージ生成可能上限数ではないかを見る
        if (ItemManager.Instance.CanPopItem() == false) return;

		//	popPosの生成確率から、生成するPosを計算で選ぶ
		int index = PickPopPoint();

		//	popPosのリストから生成するお菓子を確率で選出
		var type = ItemManager.Instance.PickItem();
		
		//	生成
		popPoint[index].Pop(type);	

	}

	
	/// <summary>
	/// 生成するポップポイントを選ぶ
	/// </sammary>
	private int PickPopPoint(){

		//	判定値を生成(ランダム)
		int judgeValue = Random.Range(0, 100);	//	0~99までの100個

		uint rangeBottom = 0;
		uint rangeTop = 0;

		int current = 0;
		for(current = 0; current < percent.Count; current++){
			
			//	判定範囲設定
			rangeBottom = rangeTop;
			rangeTop += (uint)percent[current];

			//	判定内だ
			if(judgeValue >= rangeBottom && judgeValue <= rangeTop){
				//	あたり～
				return current;
			}
		}

		return current;
	}


	/// <summary>
	/// 各ポップ位置の持つポップ率をパーセントに変換
	/// </sammary>
	private void ConvPercent(){
		foreach(StagePop p in popPoint){
			percent.Add(Mathf.Round((100.0F * p.popPriority) / sumPriority));
		}
	}


}
