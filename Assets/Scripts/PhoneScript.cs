using UnityEngine;
using System.Collections;

public class PhoneScript : MonoBehaviour {

	public bool phoneON;

	public float myValue = 60;
	public static int poopNumer = 0;

	public static bool letClick = false;

	public HitButton buttHit;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(phoneON){
			Debug.Log("show phone");
		}
		if(letClick && poopNumer >= 1){
			myValue += poopNumer * 5;
			poopNumer = 0;
			letClick = false;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Poop"){
			Debug.Log("touched poop");
			Destroy(other.gameObject);
			poopNumer++;
			buttHit.setText();
			Debug.Log(poopNumer);
		}
	}
}
