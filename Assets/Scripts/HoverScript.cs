using UnityEngine;
using System.Collections;

public class HoverScript : MonoBehaviour {

	public GameObject myOutline;
	public Sprite tSprite;
	public GameObject me;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseOver(){
		if(gameObject.name == "Button"){
			if(HitButton.canTweet){
				myOutline.SetActive(true);
			}
		} 
		else if(gameObject.tag == "Player"){
			Debug.Log("so");
			me.GetComponent<SpriteRenderer>().sprite = tSprite;
		}else {
			myOutline.SetActive(true);
		}
			
	}

	void OnMouseExit(){
		if(gameObject.tag == "Player"){
			gameObject.GetComponent<SpriteRenderer>().sprite = tSprite;
		} else{
			myOutline.SetActive(false);
		}
	}
}
