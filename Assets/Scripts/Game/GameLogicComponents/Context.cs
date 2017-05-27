using System.Collections;
using System.Text;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Context : MonoBehaviour {
	Regex eventNameRegex = new Regex(@"w\+");
	Regex eventArgsRegex = new Regex(@"((w\,?)*)");
	string[] newLineDelimiter = new string[]{ System.Environment.NewLine };
	char[] storyLineDelimiterChar = { '%' };
	char[] playerResponseDelimiterChar = { '-' };
	char[] characterResponseDelimiterChar = { '|' };
	char[] eventArgsDelimiter = { ','};
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
			if (fieldValues.Length >= 2) {
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
					Event[] eventsToPublishOnReaching = null;
					if(fieldValues.Length >= 4){
					string[] namesAndArgsOfEventsToPublishOnReaching = fieldValues[4].Trim().Split(characterResponseDelimiterChar, System.StringSplitOptions.None);
					eventsToPublishOnReaching = new Event[namesAndArgsOfEventsToPublishOnReaching.Length];
					for(int j=0;j<namesAndArgsOfEventsToPublishOnReaching.Length;j++){
						string nameAndArgs=namesAndArgsOfEventsToPublishOnReaching[j];
						try{
							string eventName = Regex.Match(nameAndArgs, @"\w*").ToString();
							string allArgs=Regex.Match(nameAndArgs, @"\((\w*,?)*\)").ToString();
							allArgs = allArgs.Substring(1, allArgs.Length-2);
							string[] eventArgs = allArgs.Split(eventArgsDelimiter, StringSplitOptions.None);
							Type eventType = Type.GetType(eventName);
							//questions: how to specify if it has multilpe parameters? maybe we ca do a test.
							ConstructorInfo constructor = eventType.GetConstructor(eventArgs.Select(eventArg => typeof(string)).ToArray());
							eventsToPublishOnReaching[j]=(Event)constructor.Invoke(eventArgs);
						}catch(Exception e){
							Debug.Log($"Badly formatted event name and args: {nameAndArgs}");
						}
					}
				}
				    
					ActionCreator.Instance.AddNodeToStory(characterName, storyLineId, introductoryText, playerResponses, characterResponses, eventsToPublishOnReaching);

				}
				catch(Exception e){
					Debug.Log (e.Message);
				}



		}

	}

}
