using UnityEngine;
using System.Collections;

public class GameModule : BaseMonoBehaviour {

	public MapModule Map;
	public bool StartGameOnLoad = false;
	public FireManager Fire;
	public WeatherManager Weather;
	//public GoblinManager Goblin;
	//public PoliceManager Police;
	//public FiremenManager Firemen;

	// Use this for initialization
	void Start () {
		// Test purposes
		if (StartGameOnLoad) {
			NewGame ();
		}
	}

	public void NewGame() {
		Map.GenerateMap ();
		Weather.GenerateWeather ();
		Fire.GenerateFires ();
		//Goblin.GenerateGoblins();
		//Police.GeneratePolice();
		//Firemen.GenerateFiremen();
		Messenger.Broadcast ("newGameStarted");
	}
}
