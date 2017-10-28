using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// プレイヤーマスタークラス
/// </summary>
public class Player : MonoBehaviour {

	#region Member

	[Header("体力"),SerializeField]
	private int				m_HitPoint = 100;

	[Header("移動量"),SerializeField]
	private float			MOVE_FORCE = 10.0F;

	[Header("カメラ"),SerializeField]
	private PlayerCamera	m_Camera = null;
	private Transform		m_CameraTrans;


	#endregion	Member

	#region Getter

	public int GetHP { get; set; }
	public bool IsLife { get { return (m_HitPoint > 0); } }

	#endregion Getter



	#region Method
	#endregion	Method

	// Use this for initialization
	void Start () {
		Init();
		
	}
	
	// Update is called once per frame
	void Update () {
		Move();	// 移動処理

		// transform.position -= (transform.position - new Vector3( 0.0F, -0.5F, 10.0F )) / 4.0F;
	}


	void Init() {
		if( !m_Camera ) {
			Debug.LogError("カメラオブジェクト取得失敗");
		}
		m_CameraTrans = m_Camera.transform;
	}


	/// <summary>
	/// 移動処理
	/// </summary>
	void Move() {
		// TODO : input 作成の後

		Vector3 vec = Vector3.zero;	// 移動方向
		Vector3 move;				// 移動量

		// 前進
		if( Input.GetKey( KeyCode.W ) ) {
			vec += m_CameraTrans.forward;
			transform.forward = m_CameraTrans.forward;
		}
		// 後退
		if( Input.GetKey( KeyCode.S ) ) {
			vec -= m_CameraTrans.forward;
			transform.forward = -m_CameraTrans.forward;
		}
		// 左
		if( Input.GetKey( KeyCode.A ) ) {
			vec -= m_CameraTrans.right;
			transform.forward = -m_CameraTrans.right;
		}
		// 右
		if( Input.GetKey( KeyCode.D ) ) {
			vec += m_CameraTrans.right;
			transform.forward = m_CameraTrans.right;
		}
		// 移動量計算
		move = vec.normalized * MOVE_FORCE * Time.deltaTime;
		//transform.position += move;
		transform.position += move;
		// Debug.Log( "Player:" + transform.position );
		// Debug.Log( "Camera:" + m_CameraTrans.position );
		// Debug.Log( "Player - Camera:" + (transform.position - m_CameraTrans.position) );
		m_CameraTrans.position += move;
		//m_Camera.NextPos = move;
	}

}
