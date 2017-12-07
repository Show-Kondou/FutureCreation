using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// タイトル全体的なことする
/// </sammary>
public class Title : MonoBehaviour {

	[SerializeField]
	Camera camera;

	Animator cameraAnim;

	// Use this for initialization
	void Start () {
		//	カメラのアニメーターを取得
		cameraAnim = camera.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {


		
		//	入力待ち
		WaitInput();
	}



	/// <summary>
	/// タイトルシーン内での入力待ち処理記述
	/// </sammary>
	private void WaitInput(){
		//TODO:	おそらくシーンマネージャーの遷移開始的なのを呼ぶ
	}


	private void PlayCameraAnim(){
		//TODO:	カメラの移動アニメーション開始呼び出し
		// cameraAnim.Play();
	}


}
