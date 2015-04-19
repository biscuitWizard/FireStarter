using UnityEngine;
using System.Collections;
using System.Linq;

public class MouseControls : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(1)) {
			Debug.Log ("Mouse Button Down!");

			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			var hits = Physics2D.RaycastAll(Vector2.zero, Vector2.up);
			var terrainHits = hits.Where(hit => hit.transform.tag.Equals ("Terrain"));

			Debug.Log ("Hits: " + hits.Count());

			if(terrainHits.Any ()) {
				Debug.Log ("Found Tile");
			}

		}
	}
}
