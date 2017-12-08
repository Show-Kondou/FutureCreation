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
    

    // Use this for initialization
    void Start(){
        //	カメラのアニメーターを取得
        cameraAnim = rendererCamera.GetComponent<Animator>();
	}


	
    // Update is called once per frame
    void Update(){

        //	入力待ち
        WaitInput();

    }



	/// <summary>
	/// 全画面のタイトルUIのアクティブ操作
	/// </summary>
	/// <param name="value"></param>
	private void TitleUIsSetActive(bool value){
		foreach(GameObject obj in titleUIs) {
			obj.SetActive(value);
		}
	}

    /// <summary>
    /// タイトルシーン内での入力待ち処理記述
    /// </sammary>
    private void WaitInput()
    {
        //TODO:	ゲームパッド用のインプットに変更する

		//	参加受付画面に移行
        if (Input.GetKeyDown(KeyCode.Space)){
            //  アニメーション開始
            cameraAnim.SetTrigger("PushButton");
			//	タイトルUIを非表示に
			TitleUIsSetActive(false);
        }

		//	タイトル画面に移行
		else if (Input.GetKeyDown(KeyCode.Backspace)) {
			//  アニメーション開始
			cameraAnim.SetTrigger("PushButton");
			//	タイトルUIを表示
			TitleUIsSetActive(true);
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
