using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextSetter : MonoBehaviour {
	public GameObject[] contexts;
	void Start () {


		GameState.Instance.playerFetched += () => {
			//pick a random context, tell it to initialize
			int random = Random.Range(0, contexts.Length);
			Context context = contexts[random].GetComponent<Context>();
			context.Load();
			//for each other context, unload the assets.

		};
	}
	

}
