using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	[SerializeField]private GameObject _gridHolder;
	private Grid _grid;

	private void Start() {
		
		StartCoroutine (WalkPath ());
	}

	private IEnumerator WalkPath() {
		yield return new WaitForSeconds (0.1f);
		_grid = _gridHolder.GetComponent<Grid> ();
		REVERSE:
		Debug.Log (_grid.Path.Count); 
		for (int i = 0; i < _grid.Path.Count; ) {
			Vector2 v2 = transform.position;
			while (v2 != _grid.Path [i].Position) {
				Debug.Log (_grid.Path[i].Position);
				transform.position = Vector2.Lerp (transform.position, _grid.Path[i].Position, 1f);
				v2 = transform.position;
				yield return new WaitForSeconds(0.1f);
			}
			i++;
		}
		_grid.Path.Reverse ();
		goto REVERSE;
	}
}
