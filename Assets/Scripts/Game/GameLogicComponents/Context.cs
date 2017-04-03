using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Context : MonoBehaviour {
	public TextAsset PlaceInfo;
	public TextAsset NPCInfo;
	public TextAsset[] CharacterScripts;


	public void Load(){

		CreateNPCs();
	}

	private void CreateNPCs(){
		string[] NPCData =  NPCInfo.text.Split(new string[]{System.Environment.NewLine}, System.StringSplitOptions.None);
		for (int i = 0; i < NPCData.Length; i++) {
			Debug.Log (NPCData [i]);
		}

	}

}
