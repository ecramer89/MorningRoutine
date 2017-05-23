using System.Collections;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Context : MonoBehaviour {
	string[] newLineDelimiter = new string[]{ System.Environment.NewLine };
	char[] storyLineDelimiterChar = { '%' };
	char[] playerResponseDelimiterChar = { '-' };
	char[] characterResponseDelimiterChar = { '|' };
	char[] fieldDelimiterChar = {'<'};
	public TextAsset createCharacters;
	public TextAsset storyLines;


	public void Load(){
		CreateCharacters();
		AddStoryLines ();
	}


	private void CreateCharacters(){
		string[] characterData = createCharacters.text.Split (newLineDelimiter, System.StringSplitOptions.None);
		for (int i = 1; i < characterData.Length; i++) {
			string[] fieldValues = characterData [i].Split (fieldDelimiterChar, System.StringSplitOptions.None);
			if (fieldValues.Length == 2) {
				ActionCreator.Instance.CreateCharacter (fieldValues [0], fieldValues [1]);
			}
		}

	}

	private void AddStoryLines(){
		string[] storyLineData = storyLines.text.Split(storyLineDelimiterChar, System.StringSplitOptions.None);
		for (int i = 1; i<storyLineData.Length; i++) {
			string[] fieldValues = storyLineData[i].Split (newLineDelimiter, System.StringSplitOptions.None);
			if (fieldValues.Length < 4) throw new Exception($"Badly formatted storyline. Check the input text file."); 
				try{
					string characterName = fieldValues[0];
					string storyLineId = Guid.NewGuid().ToString();
					string introductoryText = fieldValues[1].Trim();
					string[] playerResponses = fieldValues[2].Trim().Split(playerResponseDelimiterChar, System.StringSplitOptions.None);
					string[] characterResponses = fieldValues[3].Trim().Split(characterResponseDelimiterChar, System.StringSplitOptions.None);
	                //get events and bound arguments.
				    //convert event names into the constructor type using reflection
				    //create delegates with bound arguments that will invoke the event constructor. one of the bound arguments needs to be a 
				//another delegate that will fetch the player id from the gloval context

					ActionCreator.Instance.AddStoryLine(characterName, storyLineId, introductoryText, playerResponses, characterResponses);

				}
				catch(Exception e){
					Debug.Log (e.Message);
				}



		}

	}

}
