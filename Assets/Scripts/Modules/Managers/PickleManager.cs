using UnityEngine;
using System;
using System.Linq;
using System.Collections;

[RequireComponent(typeof(EntityManager))]
public class PickleManager : MonoBehaviour {
	public EntityManager EntityManager;

	public void EatPickleAtLocation(Vector2 location){

		PickleEntity pickle = (PickleEntity)GetClosestPickleInRange (location, 0);
		EntityManager.DestroyEntity (pickle);
	}

	public bool IsPicklePresent(Vector2 location) {

		return EntityManager.GetPickles ().Any (p => p.GetLocation () == location);
	}

	public bool IsPickleInRange(Vector2 location, int distance = 5){

		if (GetClosestPickleInRange (location, distance) != null) {
			return true;
		} else {
			return false;
		}
	}

	public EntityBase GetClosestPickleInRange(Vector2 location, int distance = 5) {
		var picklesInRange = EntityManager.GetPickles ().Select (p => {
			var pickleLocation = p.GetLocation();
			var xd = pickleLocation.x - location.x;
			var yd = pickleLocation.y - location.y;

			return new PickleDistance(p, Mathf.Sqrt(Mathf.Pow (xd, 2) + Mathf.Pow (yd,2)));
		})
			.Where (pd => pd.Distance <= distance)
			.OrderBy (pd => pd.Distance);

		var pickle = picklesInRange.FirstOrDefault ();
		return pickle != null
			? pickle.Pickle
				: null;
	}

	private class PickleDistance {
		public EntityBase Pickle;
		public float Distance;

		public PickleDistance(EntityBase pickle, float distance) {
			Pickle = pickle;
			Distance = distance;
		}
	}
}
