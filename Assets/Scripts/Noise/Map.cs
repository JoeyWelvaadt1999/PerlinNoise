using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour {
	private Dictionary<Vector2, GameObject> _mapIndex = new Dictionary<Vector2, GameObject>();
	public Dictionary<Vector2, GameObject> MapIndex {
		get { 
			return _mapIndex;
		}	
	}

	private NoiseMap _noiseMap;
	private ObjectDataValue _dataValue;

	private float[,] _map;

	[SerializeField]private float _width;
	[SerializeField]private float _height;
	[SerializeField]private float _scale;

	private void Awake() {
		_dataValue = GetComponent<ObjectDataValue> ();
		_noiseMap = GetComponent<NoiseMap> ();
	
		_noiseMap.CreateNoiseMap (_width,_height,_scale);
		_map = _noiseMap.Noise;
		for (int x = 0; x < _width; x++) {
			for (int y = 0; y < _height; y++) {
				_mapIndex.Add (new Vector2 (x, y), _dataValue.GetObjectByValue (_map [x, y]));
			}
		}
	}
		
}
