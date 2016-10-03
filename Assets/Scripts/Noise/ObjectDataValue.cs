using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectDataValue : MonoBehaviour {
	private Dictionary<TileTypes, GameObject> _objectByValue = new Dictionary<TileTypes, GameObject>();
	private float _perlinNoiseValue;

	[SerializeField]private GameObject _wall;
	[SerializeField]private GameObject _ground;

	private void Start() {
		
	}

	public GameObject GetObjectByValue (float noiseValue) {
		_objectByValue.Clear ();
		_objectByValue.Add (TileTypes.Ground, _ground);
		_objectByValue.Add (TileTypes.Wall, _wall);
		_perlinNoiseValue = noiseValue;
		TileTypes type = _perlinNoiseValue <= 0.55f ? TileTypes.Ground : TileTypes.Wall;
		return _objectByValue[type];
	}
}

enum TileTypes
{
	Ground = 0,
	Wall = 1
}