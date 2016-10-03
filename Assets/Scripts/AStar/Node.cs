using UnityEngine;
using System.Collections;

public class Node {
	private Vector2 _position;
	private bool _walkable;

	public Node(Vector2 position, bool walkable) {
		_position = position;
		_walkable = walkable;
	}

	public Vector2 Position {
		get { 
			return _position;
		}
	}

	public bool Walkable {
		get {
			return _walkable;
		}
	}

	public int G { get; set;}
	public int H { get; set;}
	public int F { get {return G + H; } }
	public Node Parent {get; set;}
}
