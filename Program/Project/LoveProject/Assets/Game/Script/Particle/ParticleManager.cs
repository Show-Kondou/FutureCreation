using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour {

	public enum ParticleName
	{
		Hit = 0,	//	カスが飛び散る
		Bomb,	//	投げアイテムの爆発
		Heal,	//	回復
		Smoke,	//	足元の煙
		Pop,
		Eat,
		Kas,
		Max
	};

	ParticleName Name;


	//	パーティクルのリスト
	[SerializeField] List<ParticleSystem> particlePrefab;
	[SerializeField] List<GameObject> effectPrefab;

	//	インスタンスを格納するプール
	public MultiDictionary<ParticleName, ParticleSystem> particlePool;

	#region Singleton
	private static ParticleManager instance;
	public static ParticleManager Instance
	{
		get {
			if (instance == null) {
				instance = (ParticleManager)FindObjectOfType(typeof(ParticleManager));

				//	無ければ生成
				if (instance == null) {
					instance = new GameObject("ParticleManager").AddComponent<ParticleManager>();
				}
			}

			return instance;
		}
	}

	#endregion Singleton

	void Awake(){
		if (Instance != this) {
			Destroy(this.gameObject);
		}

		//	プールの生成
		if (particlePool == null)
			particlePool = new MultiDictionary<ParticleName, ParticleSystem>();

		//	初期のアイテムストック生成
		//	6種類を5個ずつ非表示で生成しておく
		for (int index = 0; index < particlePrefab.Count; index++) {
			for (int par_num = 0; par_num < 5; par_num++) {
				var par_obj = GetGameObject((ParticleName)index, transform.position);
				par_obj.gameObject.SetActive(false);  //	非表示へ
			}
		}
	}

	/// <summary>
	///	パーティクル再生
	/// </summary>
	public void PlayParticle(ParticleName _name, Vector3 _pos){

		//TODO:	使用可能パーティクルを引っ張ってくる
		var par_obj = GetParticleObject(_name, _pos);

		//	パーティクルシステム再生開始
		par_obj.Play();
	}


	public void PlayEffect(Vector3 _pos){
		
		var _obj = (GameObject)Instantiate(effectPrefab[0], _pos, Quaternion.identity);
		if(_obj == null) Debug.LogError("リングエフェクトがない");
	}



	///<param>
	///	オブジェクト生成	これいる！！消さないで！！
	///	type	:生成するプレハブの種別
	///	pos		:生成する座標
	///</param>
	public ParticleSystem GetGameObject(ParticleName name, Vector3 pos)
	{

		//	インスタンス生成
		var par_obj = (ParticleSystem)Instantiate(particlePrefab[(int)name], pos, Quaternion.identity);
		par_obj.transform.parent = transform;//	プールの子要素にする
		particlePool.Add(name, par_obj);   //	リストに追加

		return par_obj;
	}


	private ParticleSystem GetParticleObject(ParticleName _name, Vector3 _pos){

		ParticleSystem _obj = null;


		//	プールにtypeのオブジェクトが存在しなければ生成
		if (particlePool.ContainsKey(_name) == false) {
			//	生成する
			_obj = (ParticleSystem)Instantiate(particlePrefab[(int)_name], _pos, Quaternion.identity);
			

			//	プールの子要素にする
			_obj.transform.parent = transform;

			particlePool.Add(_name, _obj);
			return _obj;
		}

	
		List<ParticleSystem> _particles = particlePool[_name];

		//	使用可能オブジェクト検索ループ
		for (int i = 0; i < _particles.Count; i++) {
			_obj = _particles[i];

			if (_obj == null) continue;

			var par_obj = _obj.gameObject;
			//	非アクティブであれば
			if (par_obj.activeInHierarchy == false) {
				//	位置の設定
				_obj.transform.position = _pos;

				//	角度の設定
				_obj.transform.rotation = Quaternion.identity;

				//	これから使用する
				par_obj.SetActive(true);

				return _obj;
			}
		}

		//	使用できるものがなかった場合ここまでくる
		//	生成する
		_obj = (ParticleSystem)Instantiate(particlePrefab[(int)_name], _pos, Quaternion.identity);

		//	プールの子要素にする
		_obj.transform.parent = transform;

		//	リストに追加
		_particles.Add(_obj);

		return _obj;
	}
}
