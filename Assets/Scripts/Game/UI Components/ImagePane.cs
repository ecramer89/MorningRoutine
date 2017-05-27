using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ImagePane : MonoBehaviour {

	Image image;
	static readonly string CHARACTER_IMAGE_DIRECTORY="Images/Characters";
	static readonly string DEFAULT_EXTENSION=".png";
	string currentSourceDirectory;
	Timed imageDelayBeforeDisappear;

	void Start(){
		gameObject.SetActive (false);
		image = gameObject.GetComponent<Image> ();
		imageDelayBeforeDisappear = gameObject.GetComponent<Timed> ();
		//register delegate to fetch the image data of character when the new character is set
		GameState.Instance.characterSet += (id) => {
			if(id == GlobalGameConstants.NULL_ID){ 
				imageDelayBeforeDisappear.Set(2);
				imageDelayBeforeDisappear.Done+=() => {
					gameObject.SetActive(false);
					image.sprite = null;
				};
				imageDelayBeforeDisappear.On();
			}else {
				gameObject.SetActive(true);	
			    currentSourceDirectory = $"{CHARACTER_IMAGE_DIRECTORY}/{id}";
				image.sprite=Resources.Load<Sprite>($"{currentSourceDirectory}/default");
			}
		};

	}
}
