using UnityEngine;
using System.Collections;

public class InteractScript : MonoBehaviour {

	public string objectName;

	public float myValue;

	public GameObject phone;
	public GameObject phonePic;

	static bool clickedPhone;

	public Animator trumpAnim;
	public GameObject phoneHand;

	public static bool canClickPhone = true;

	public GameObject tweetText;

	public StateMachine StateScript;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown(){
		if(gameObject.name == "Food Area"){
			StateScript.SetState(StateMachine.State.WalkEat);
			canClickPhone = false;
		}
		if(gameObject.name == "Orange Shower"){
			StateScript.SetState(StateMachine.State.WalkShower);
			canClickPhone = false;
		}
		if(canClickPhone){
			if(gameObject.name == "Trump" && !clickedPhone){
				StateScript.SetState(StateMachine.State.Phone);
				trumpAnim.SetBool("phone", true);
				phoneHand.SetActive(true);
				phonePic.SetActive(true);
				phone.GetComponent<PhoneScript>().phoneON = true;
				clickedPhone = true;
			}
				if (gameObject.name == "Exit"  && clickedPhone){
				//StateScript.SetState(statez.State.WalkCenter);
				Debug.Log("sup");
				tweetText.SetActive(false);
				trumpAnim.SetBool("phone", false);
				phoneHand.SetActive(false);
				phonePic.SetActive(false);
				clickedPhone = false;
			}
		}
	}

}