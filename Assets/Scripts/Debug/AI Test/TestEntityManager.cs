using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestEntityManager : EntityManager {
	
	public GameObject TestAIPrefab;
	
	private List<EntityBase> _testAIs = new List<EntityBase>();
	
	public EntityBase CreateTestAI(Vector2 location) {
		var test = Instantiate (TestAIPrefab);
		var testEntity = test.GetComponent<TestAIEntity> ();
		
		_testAIs.Add (testEntity);
		MoveEntityTo (testEntity, location);
		
		return testEntity;
	}
}