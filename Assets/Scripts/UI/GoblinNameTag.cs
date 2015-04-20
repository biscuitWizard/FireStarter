using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GoblinNameTag : MonoBehaviour {
	public string NameFormat = "{0}";
	public Text TextControl;
	private GoblinEntity _goblin;

	public GoblinEntity GetGoblin() {
		return _goblin;
	}

	public void SetGoblin(GoblinEntity goblin) {
		_goblin = goblin;
		TextControl.text = string.Format (NameFormat, goblin.Name);
	}

	public void OnClickTag() {
		// Center camera on goblin
		Camera.main.transform.position = new Vector3 (_goblin.transform.position.x, _goblin.transform.position.y - 3.4f, -10);
	}
}
