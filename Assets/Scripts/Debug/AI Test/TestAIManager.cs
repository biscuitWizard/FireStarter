using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(TestAIManager))]
public class TestAIManager : BaseMonoBehaviour {
	public TestEntityManager Entity;
	public MapModule Map;
	
	private EntityBase _testAI;
	
	private List<Vector2> _currentPath;
	private Vector2 Destination1 = new Vector2 (5, 5);
	private Vector2 Destination2 = new Vector2(10, 10);
	private Pathfinder _pathfinder;
	
	// Use this for initialization
	void Start () {
		_testAI = Entity.CreateTestAI (Destination1);
		_pathfinder = new Pathfinder (Map, Entity);
	}
	
	// Update is called once per frame
	public override void AIUpdate () {
		if (_currentPath != null) {
			Entity.MoveEntityTo(_testAI, _currentPath[0]);
			_currentPath.RemoveAt (0);

			if (_currentPath.Count == 0) {
				_currentPath = null;
			}
		} else if (_testAI.GetTile ().Position == Destination1) {
			_currentPath = new List<Vector2> ();
			_currentPath.AddRange (_pathfinder.Navigate (_testAI.GetTile ().Position, Destination2));

		} else {
			_currentPath = new List<Vector2> ();
			_currentPath.AddRange (_pathfinder.Navigate (_testAI.GetTile ().Position, Destination1));
		}
	}
}