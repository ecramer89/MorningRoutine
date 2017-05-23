using System.Collections;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Context : MonoBehaviour {
	char[] storyLineDelimiterChar = { '%' };
	char[] fieldDelimiterChar = {'<'};
	public TextAsset createCharacters;
	public TextAsset storyLines;


	public void Load(){
		CreateCharacters();
		AddStoryLines ();
	}


	private void CreateCharacters(){
		string[] characterData = createCharacters.text.Split (new string[]{ System.Environment.NewLine }, System.StringSplitOptions.None);
		for (int i = 1; i < characterData.Length; i++) {
			string[] fieldValues = characterData [i].Split (fieldDelimiterChar, System.StringSplitOptions.None);
			if (fieldValues.Length >= 3) {
				int id;
				if (Int32.TryParse (fieldValues [0], out id)) {
					ActionCreator.Instance.CreateCharacter (id, fieldValues [1], fieldValues [2]);
				} else
					throw new ArgumentException ($"Invalid id for CreateCharacter:{fieldValues [0]} isn't an integer");
			}
		}

	}

	private void AddStoryLines(){
		string[] storyLineData = storyLines.text.Split(storyLineDelimiterChar, System.StringSplitOptions.None);
		for (int i = 1; i<storyLineData.Length; i++) {
			string[] fieldValues = storyLineData[i].Split (fieldDelimiterChar, System.StringSplitOptions.None);
			if (fieldValues.Length >= 5) {//parsing multiple steps
				try{
					int characterId = Int32.Parse(fieldValues[0]);
					int storyLineId = Int32.Parse(fieldValues[1]);
					string introductoryText = fieldValues[2].Trim();
					string playerResponses = fieldValues[3].Trim();
					string characterResponses = fieldValues[4].Trim();
					string[] steps = new string[fieldValues.Length - 5];
					for(int j=5;j<fieldValues.Length;j++){
						steps[j-5]=fieldValues[j];
					}
					ActionCreator.Instance.AddStoryLine(characterId, storyLineId, introductoryText, playerResponses, characterResponses, steps);

				}
				catch(Exception e){
					Debug.Log (e.Message);
				}

			}

		}

	}

}
