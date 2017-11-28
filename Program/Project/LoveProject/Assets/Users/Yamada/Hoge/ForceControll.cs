﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceControll : MonoBehaviour {

	[SerializeField]
	private Transform shootPoint = null;
	[SerializeField]
	private Transform target = null;
	[SerializeField]
	private GameObject shootObject = null;
	
	Vector3 v0;
	
    //  ラインレンダラー関係
    LineRenderer lineRenderer;
    List<Vector3> renderLinePoints = new List<Vector3>();

	void Start(){
		
        lineRenderer = GetComponent<LineRenderer>();
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.G)){
			//ShootAction(target.position);
		}
	}



	/// <summary>
	/// 
	/// </sammary>
	public void ShootAction(/*Vector3 _targetPos*/){
		ShootFixedAngle(target.position, 60.0F);
	}



	/// <summary>
	/// 角度を指定してオブジェクトを発射
	/// </sammary>
	private void ShootFixedAngle(Vector3 _targetPos, float _angle){
		var speed_vec = ComputeVectorFromAngle(_targetPos, _angle);
		if(speed_vec <= 0.0F){
			Debug.Log("!!着地負荷");
			return;
		}

		Vector3 _vec = ConvertVectorToVector3(speed_vec, _angle, _targetPos);
		InstantiateShootObject(_vec);
	}



	/// <summary>
	/// 
	/// </sammary>
	private float ComputeVectorFromAngle(Vector3 _targetPos, float _angle){
		
		//	xz平面の距離を計算
		Vector2 start_pos = new Vector2(shootPoint.transform.position.x, shootPoint.transform.position.z);
		Vector2 target_pos = new Vector2(_targetPos.x, _targetPos.z);
		float _distance = Vector2.Distance(target_pos, start_pos);

		float x = _distance;
		float g = Physics.gravity.y;
		float y0 = shootPoint.transform.position.y;
		float y = _targetPos.y;

		//	ラジアンに変換
		float _rad = _angle * Mathf.Deg2Rad;

		float _cos = Mathf.Cos(_rad);
		float _tan = Mathf.Tan(_rad);

		float _square = g * x * x / (2 * _cos * _cos * (y - y0 - x * _tan));
		
		//	虚数の場合の計算打ち切り
		if(_square <= 0.0f) return 0.0f;

		float _v0 = Mathf.Sqrt(_square);
		return _v0;
	}



	/// <summary>
	/// 
	/// </sammary>
	private Vector3 ConvertVectorToVector3(float _v0, float _angle, Vector3 _targetPos){
		Vector3 start_pos = shootPoint.transform.position;
		Vector3 target_pos = _targetPos;
		start_pos.y = 0.0F;
		target_pos.y = 0.0F;

		Vector3 _dir = (target_pos - start_pos).normalized;
		Quaternion yaw_rot = Quaternion.FromToRotation(Vector3.right, _dir);
		Vector3 _vec = _v0 * Vector3.right;

		_vec = yaw_rot * Quaternion.AngleAxis(_angle, Vector3.forward) * _vec;

		return _vec;

	}



	/// <summary>
	/// 発射するオブジェクトの生成
	/// </sammary>
	private void InstantiateShootObject(Vector3 _shootVector){


		if(shootObject == null) {Debug.Log("shootObjectがない");return;}
		if(shootPoint == null) {Debug.Log("shootPointがない");return;}

		var _obj = Instantiate<GameObject>(shootObject, shootPoint.position, Quaternion.identity);
		var _rigidbody = _obj.GetComponent<Rigidbody>();

		Vector3 _force = _shootVector * _rigidbody.mass;

		_rigidbody.AddForce(_force, ForceMode.Impulse);
	}


	public void DestLaser(){
        // 予測線の軌道をクリア
        renderLinePoints.Clear();
        // LineRenderer で描画
        lineRenderer.positionCount = renderLinePoints.Count;
        lineRenderer.SetPositions(renderLinePoints.ToArray());
	}

	public void DrawLaser(){
		
		float x = 0;
		float y = 0;
		float z = 0;

        // 予測線の軌道をクリア
        renderLinePoints.Clear();

		var speed_vec = ComputeVectorFromAngle(target.position, 60.0F);
		Vector3 _vec = ConvertVectorToVector3(speed_vec, 60.0F, target.position);
		v0 = _vec;
		
		//弾道予測の位置に点を移動
		for (int i = 0; i < 10; i++)
		{
			var t = i * 0.5f;
			x = t * v0.x;
			z = t * v0.z;
			y = (v0.y * t) - 0.5f * (-Physics.gravity.y) * Mathf.Pow(t, 2.0f);
			

            Vector3 nextPosition = new Vector3(x,y,z);
            // 線のリストに加える
            renderLinePoints.Add(nextPosition);
		}
        // LineRenderer で描画
        lineRenderer.positionCount = renderLinePoints.Count;
        lineRenderer.SetPositions(renderLinePoints.ToArray());

	}

}
