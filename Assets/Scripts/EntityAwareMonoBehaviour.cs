using UnityEngine;
using System.Collections;
using System.Linq;

public class EntityAwareMonoBehaviour : BaseMonoBehaviour {

	EntityBase[] GetEntities() {
		var children = gameObject.GetComponentsInChildren<EntityBase> ();

		return children.ToArray ();
	}
}
