using UnityEngine;
using System.Collections;

public class MouseMove : MonoBehaviour {

	public float speed = 20f;
	private Vector3 target;

	bool follow = false;

	private Vector3 oriPos;

	// Use this for initialization
	void Start () {
		oriPos = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		target.z = transform.position.z;
		transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

		if(follow){
			transform.position = new Vector3 (target.x, target.y,0);	
		} else{
			transform.position = oriPos;
		}
	}

	void OnMouseDown(){
		follow = true;
	}

	void OnMouseUp(){
		follow = false;
	}
}
