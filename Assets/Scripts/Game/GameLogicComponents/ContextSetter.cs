using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextSetter : MonoBehaviour {
	public GameObject[] contexts;
	void Start () {

		GameState.Instance.playerFetched += () => {
			if(GameState.Instance.NarrationState.narrationOn){
				GameState.Instance.narrationSet += () =>{
					LoadRandomContext();
				};
			} else {
				LoadRandomContext();
			}
		};
	}


	void LoadRandomContext(){
		int random = Random.Range(0, contexts.Length);
		Context context = contexts[random].GetComponent<Context>();
		context.Load();
	}
	

}
