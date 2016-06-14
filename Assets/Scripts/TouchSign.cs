using UnityEngine;
using System.Collections;

public class TouchSign : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		Debug.Log("HEY");
		GameObject.Find("Game Manager").GetComponent<HappinessSystem>().CallNewRound();
	}
}
