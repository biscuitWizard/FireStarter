using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Linq;

public class KindlingFireGenerator : MonoBehaviour {

	public Sprite[] FireSprites;
	public int MaxFlames = 5;
	public int MinFlames = 3;

	// Use this for initialization
	void Start () {
		transform.localPosition = Vector2.zero;

		GenerateFlames ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	GameObject GenerateFlame() {
		var flameSprite = FireSprites.OrderBy (f => System.Guid.NewGuid ()).First ();
		var newGameObject = new GameObject ("Flame", typeof(SpriteRenderer));
		newGameObject.transform.SetParent (transform);

		var spriteRenderer = newGameObject.GetComponent<SpriteRenderer> ();
		spriteRenderer.sprite = flameSprite;
		spriteRenderer.sortingOrder = 5000;
		

		return newGameObject;
	}

	public void GenerateFlames() {
		// Cleanup
		foreach (var child in GetComponentsInChildren(typeof(SpriteRenderer)).Select (sr => sr.gameObject)) {
			DestroyImmediate(child);
		}

		// Generate the graphics
		var flames = Random.Range (MinFlames, MaxFlames);
		
		for (int i=0; i < flames; i++) {
			var flame = GenerateFlame ();
			
			flame.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
			flame.transform.localPosition = new Vector2(Random.Range (-2.5f, 1.8f),
			                                            Random.Range (-2.5f, -5f));
		}
	}
}