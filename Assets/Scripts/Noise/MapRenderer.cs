using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapRenderer : MonoBehaviour {
	private Map _map;

	void Start () {
		_map = GetComponent<Map> ();
		foreach (KeyValuePair<Vector2, GameObject> element in _map.MapIndex) {
			GameObject go = Instantiate (element.Value, element.Key, Quaternion.identity) as GameObject;
			go.transform.parent = this.transform;
		}
	}
}
