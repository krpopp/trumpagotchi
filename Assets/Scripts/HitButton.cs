using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HitButton : MonoBehaviour {

	public Color activeColor;
	public Color stopColor;

	public GameObject tweetText;

	public Text tweetFont;

	public string[] tweets;

	public static bool canTweet = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(PhoneScript.poopNumer >= 1){

		} else if (PhoneScript.poopNumer <= 0){
			gameObject.GetComponent<SpriteRenderer>().color = stopColor;
			canTweet = false;
			tweetText.SetActive(false);
		}
	}

	void OnMouseDown(){
		if(PhoneScript.poopNumer >= 1){
			PhoneScript.letClick = true;
		} else if (PhoneScript.poopNumer <= 0){
			PhoneScript.letClick = false;
		}
	}

	public void setText(){
		gameObject.GetComponent<SpriteRenderer>().color = activeColor;
		tweetText.SetActive(true);
		canTweet = true;
		tweetFont.text = tweets[Random.Range(0, tweets.Length)];
	}
}
