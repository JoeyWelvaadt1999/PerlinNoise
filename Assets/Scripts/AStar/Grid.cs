using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {

	[SerializeField]private float _width;
	[SerializeField]private float _height;
	private List<Node> _path = new List<Node>();
	private AStar _astar;
	public List<Node> Path {
		get { 
			return _path;
		} set { 
			_path = value;
		}
	}
	private Node[,] _grid;
	public Node[,] NodeGrid {
		get { 
			return _grid;
		}
	}

	private Vector2 _endPos;
	private float _radius = 0.4f;

	private void Start () {

		_astar = GetComponent<AStar> ();
		CreateGrid (_width, _height);
		GetEndPos ();
	} 

	private void Update () {
		_astar.Search (new Vector2 (0, 0), _endPos, this);
	}

	private void CreateGrid(float width, float height) {
		_grid = new Node[(int)width, (int)height];
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				Vector2 worldPos = new Vector2 (x, y);
				_grid [x, y] = new Node (worldPos, Physics2D.OverlapCircle (worldPos, _radius, LayerMask.GetMask("Walkable")));
			}
		}
	}

	private void GetEndPos () {
		GEP:
		int rand = Random.Range (0, (int)_width - 1);
		for (int x = 0; x < _width; x++) {
			for (int y = (int)_height - 1; y < _height; y++) {
				if (_grid [rand, y].Walkable) {
					_endPos = new Vector2 (rand, y);
				} else {
					goto GEP;
				}
			}
		}
	}

	public List<Node> GetNeighbours(Node n) {
		List<Node> _neighbours = new List<Node> ();
		if(n.Position.x - 1 >= 0) {
			_neighbours.Add (_grid[(int)n.Position.x - 1, (int)n.Position.y]);
		} 

		if (n.Position.x + 1 < _width) {
			_neighbours.Add (_grid [(int)n.Position.x + 1, (int)n.Position.y]);
		}

		if (n.Position.y - 1 >= 0) {
			_neighbours.Add (_grid [(int)n.Position.x, (int)n.Position.y - 1]);
		}

		if (n.Position.y + 1 < _height) {
			_neighbours.Add (_grid [(int)n.Position.x, (int)n.Position.y + 1]);
		}

		return _neighbours;
	}

	private void OnDrawGizmos() {
		if (_grid != null) {
//			_path = new List<Node> ();
			for (int x = 0; x < _grid.GetLength (0); x++) {
				for (int y = 0; y < _grid.GetLength (1); y++) {
					if (_path != null) {
						if (_path.Contains (_grid [x, y])) {
							Gizmos.color = Color.black;
							Gizmos.DrawWireCube (_grid [x, y].Position, Vector3.one);
							Gizmos.DrawWireSphere (_grid [x, y].Position, _radius);
						}
					}
//

				}
			}
		}
	}
}
