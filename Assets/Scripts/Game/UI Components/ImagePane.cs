using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ImagePane : MonoBehaviour {

	Image image;
	static readonly string CHARACTER_IMAGE_DIRECTORY="Images/Characters";
	static readonly string DEFAULT_EXTENSION=".png";
	string currentSourceDirectory;

	void Start(){
		gameObject.SetActive (false);
		image = gameObject.GetComponent<Image> ();
		//register delegate to fetch the image data of character when the new character is set
		GameState.Instance.characterSet += (id) => {
			if(id == Constants.NULL_ID){ //would be better to set a timer, then actually remove after the timer goes off.
				gameObject.SetActive(false);
				image.sprite = null;
			}else {
				gameObject.SetActive(true);	
			    currentSourceDirectory = $"{CHARACTER_IMAGE_DIRECTORY}/{id}";
				image.sprite=Resources.Load<Sprite>($"{currentSourceDirectory}/default");
			}
		};

	}
}
