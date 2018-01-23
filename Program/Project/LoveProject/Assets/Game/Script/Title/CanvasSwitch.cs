using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSwitch : MonoBehaviour {

	[SerializeField]
	GameObject titleCanvas = null;
	[SerializeField]
	GameObject matchingCanvas = null;

	// Use this for initialization
	void Start () {
		if(titleCanvas == null)
			Debug.LogError("タイトル画面のキャンバスがアタッチされてない");
		if(matchingCanvas == null)
			Debug.LogError("マッチング画面のキャンバスがアタッチされてない");
	}
	
	/// <summary>
	///	タイトルのCanvasのactive操作
	/// </summary>
	public void SetActiveTitleCanv(bool value){
		titleCanvas.SetActive(value);
	}


	/// <summary>
	///	マッチングのCanvasのactive操作
	/// </summary>
	public void SetActiveMatchingCanv(bool value){
		matchingCanvas.SetActive(value);
	}


	/// <summary>
	///
	/// </summary>
	public void ToTitle(){
		titleCanvas.SetActive(true);
		matchingCanvas.SetActive(false);
	}


	/// <summary>
	///
	/// </summary>
	public void ToMatching(){
		titleCanvas.SetActive(false);
		matchingCanvas.SetActive(true);
	}
}
