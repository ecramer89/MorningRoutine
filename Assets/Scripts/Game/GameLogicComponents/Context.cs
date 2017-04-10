using System.Collections;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Context : MonoBehaviour {
	char[] delimiterChars = {'<'};
	public TextAsset createCharacters;
	public TextAsset storyLines;


	public void Load(){
		CreateCharacters();
		AddStoryLines ();
	}

	//after creating a character, we should immediately build its dialogue tree.
	//the domain will need to store the tree in memory to be able to validate communication attempts (walk down the tree)
	private void CreateCharacters(){
		string[] characterData = createCharacters.text.Split (new string[]{ System.Environment.NewLine }, System.StringSplitOptions.None);
		for (int i = 1; i < characterData.Length; i++) {
			string[] fieldValues = characterData [i].Split (delimiterChars, System.StringSplitOptions.None);
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
		string[] storyLineData = storyLines.text.Split(new string[]{System.Environment.NewLine}, System.StringSplitOptions.None);
		for (int i = 1; i<storyLineData.Length; i++) {
			string[] fieldValues = storyLineData[i].Split (delimiterChars, System.StringSplitOptions.None);
			if (fieldValues.Length >= 5) {
				try{
					int characterId = Int32.Parse(fieldValues[0]);
					int storyLineId = Int32.Parse(fieldValues[1]);
					string parentText = fieldValues[2].Trim();
					string entryPattern = @fieldValues[3].Trim();
					string text = fieldValues[4].Trim();
					StoryNodeData[] steps;

					if(fieldValues.Length > 6){
						steps = new StoryNodeData[(fieldValues.Length - 5)/2];
						for(int j=6, k=0;j<fieldValues.Length;j+=2, k++){
							string stepEntryPattern = @fieldValues[j-1];
							string stepText = fieldValues[j];
							StoryNodeData storyNodeData = new StoryNodeData(stepText, stepEntryPattern);
							steps[k]=storyNodeData;
						}
					} else steps = new StoryNodeData[]{};

					ActionCreator.Instance.AddStoryLine(characterId, storyLineId, parentText, entryPattern, text, steps);

				}
				catch(Exception e){
					Debug.Log (e.Message);
				}

			}

		}

	}

}
