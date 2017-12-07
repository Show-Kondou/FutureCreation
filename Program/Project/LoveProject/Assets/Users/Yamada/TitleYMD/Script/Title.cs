using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// タイトル全体的なことする
/// </sammary>
public class Title : MonoBehaviour
{

    [SerializeField]
    GameObject rendererCamera;

    [HideInInspector]
    public Animator cameraAnim;

    public bool IsGauss = false;
    public Gauss gauss = null;
    public float intencity = 0;

    // Use this for initialization
    void Start()
    {
        //	カメラのアニメーターを取得
        cameraAnim = rendererCamera.GetComponent<Animator>();
        cameraAnim.speed = 0;
    }

    // Update is called once per frame
    void Update()
    {

        //if (IsGauss)
        //    intencity += Time.deltaTime * 8;
        //else
        //    intencity -= Time.deltaTime * 8;

        intencity = Mathf.Clamp01(intencity);
        gauss.Resolution = (int)(intencity * 10);



        //	入力待ち
        WaitInput();
    }



    /// <summary>
    /// タイトルシーン内での入力待ち処理記述
    /// </sammary>
    private void WaitInput()
    {
        //TODO:	おそらくシーンマネージャーの遷移開始的なのを呼ぶ
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayCameraAnim();
        }
    }


    private void PlayCameraAnim()
    {
        //TODO:	カメラの移動アニメーション開始呼び出し
        cameraAnim.speed = 1;
        //cameraAnim.Play("CameraTilt", 0, 0.0f);
    }


    static Title instance;
    public static Title Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<Title>();
                if (!instance) instance = new GameObject("Title").AddComponent<Title>();
            }
            return instance;
        }
    }

    void Awake()
    {
        if (Instance && instance != this)
            Destroy(gameObject);
        
    }
}
