using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AStar : MonoBehaviour {
	private List<Node> _openList = new List<Node> ();
	private List<Node> _closedList = new List<Node> ();

	public void Search(Vector2 start, Vector2 end, Grid _grid) {
		Node _neighbour = new Node(Vector2.zero, true);
		List<Node> _neighbours = new List<Node> ();

		_openList.Add (_grid.NodeGrid [(int)start.x, (int)start.y]);

		while (_openList.Count > 0) {
			Node n = _openList [0];
			for (int i = 1; i < _openList.Count; i ++) {
				if (_openList[i].F < n.F || _openList[i].F == n.F) {
					if (_openList[i].H < n.H)
						n = _openList[i];
				}
			}

			_openList.Remove (n);
			_closedList.Add (n);

			if (n == _grid.NodeGrid[(int)end.x, (int)end.y]) {
				RetracePath(_grid.NodeGrid[(int)start.x, (int)start.y], _grid.NodeGrid[(int)end.x, (int)end.y], _grid);
				return;
			}

			foreach (Node neighbour in _grid.GetNeighbours(n)) {
				if (!neighbour.Walkable || _closedList.Contains(neighbour)) {
					continue;
				}

				int newCostToNeighbour = n.G + GetDistance(n, neighbour);
				if (newCostToNeighbour < neighbour.G || !_openList.Contains(neighbour)) {
					neighbour.G = newCostToNeighbour;
					neighbour.H = GetDistance(neighbour, _grid.NodeGrid[(int)end.x, (int)end.y]);
					neighbour.Parent = n;

					if (!_openList.Contains(neighbour))
						_openList.Add(neighbour);
				}
			}
		}
			
	}

	private void RetracePath(Node start, Node end, Grid _grid) {
		List<Node> path = new List<Node>();
		Node _currentNode = end;

		while (_currentNode != start) {
			path.Add(_currentNode);
			_currentNode = _currentNode.Parent;
		}
		path.Reverse();

		_grid.Path = path;

	}

	int GetDistance(Node nodeA, Node nodeB) {
		int dstX = (int)Mathf.Abs(nodeA.Position.x - nodeB.Position.x);
		int dstY = (int)Mathf.Abs(nodeA.Position.y - nodeB.Position.y);

		if (dstX > dstY)
			return 14*dstY + 10* (dstX-dstY);
		return 14*dstX + 10 * (dstY-dstX);
	}
}
