using UnityEngine;
using System.Collections;
using System;


public class NoiseMap : MonoBehaviour {
	[SerializeField]private string _seed;
	private float[,] _map;

	public float[,] Noise {
		get { 
			return _map;
		}
	}

	private Texture2D _tex;
	private MeshRenderer mr;

	private void Start() {
		
	}


	public void CreateNoiseMap(float width, float height, float scale) {
		mr = GetComponent<MeshRenderer> ();
		_tex = new Texture2D ((int)width, (int)height);
		mr.material.mainTexture = _tex;
		_map = new float[(int)width,(int)height];
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				_map[x,y] = Mathf.PerlinNoise(x / width  * scale, y / height  * scale);
				_tex.SetPixel (x, y, new Color (_map [x, y], _map [x, y], _map [x, y]));
			}
		}

		_tex.Apply ();

	}

	private int GetSeedByString(string seed) {
		System.Random newSeed = new System.Random (seed.GetHashCode ());
		return newSeed.Next();
	}

}
