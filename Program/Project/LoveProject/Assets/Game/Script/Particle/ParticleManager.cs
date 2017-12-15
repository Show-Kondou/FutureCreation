using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour {

	public enum ParticleName
	{
		Hit = 0,
		Max
	};

	ParticleName Name;


	//	パーティクルのリスト
	[SerializeField] List<ParticleSystem> particlePrefab;

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

	void Awake()
	{
		if (Instance != this) {
			Destroy(this.gameObject);
		}
	}


	public void PlayParticle(ParticleName _name, Vector3 _pos){

		//TODO:	使用可能パーティクルを引っ張ってくる
		var par_obj = GetParticleObject(_name, _pos);

		//	パーティクルシステム再生開始
		par_obj.Play();
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
