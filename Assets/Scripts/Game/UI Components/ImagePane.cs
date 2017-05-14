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
			gameObject.SetActive(true);	
		    currentSourceDirectory = $"{CHARACTER_IMAGE_DIRECTORY}/{id}";
			image.sprite=Resources.Load<Sprite>($"{currentSourceDirectory}/default");
		};

	}
}
