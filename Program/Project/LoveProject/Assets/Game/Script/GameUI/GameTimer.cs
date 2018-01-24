using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour {

	//	メンバ
	private float timeLimit = 180;	//	秒

	//	アクセサ
	public float TimeLimit{get{return timeLimit;}}
	public int Minute{ get{ return (int) (timeLimit / 60.0f); } }
	public int Second{ get{ return (int)(timeLimit % 60.0f); } }

	//	シングルトン
	#region Singleton
        static GameTimer instance;
        public static GameTimer Instance{
            get{
                if (!instance){
                    instance = FindObjectOfType<GameTimer>();
                    if (!instance)  instance = new GameObject("GameTimer").AddComponent<GameTimer>();
                }
                return instance;
            }
        }
	#endregion Singleton
	
	
	// Update is called once per frame
	void Update () {
		
		//	一応残しておいて
		// int m =  (int) (timeLimit / 60.0f);
		// int s = (int)(timeLimit % 60.0f);
		// Debug.Log( m + ":" + s.ToString("00") );

		if(timeLimit < 0) return;

		timeLimit -= Time.deltaTime;	//	秒減らし



	}


}
