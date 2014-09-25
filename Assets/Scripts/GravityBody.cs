using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class GravityBody : MonoBehaviour {
 
	public GravityAttractor attractor;
	private Transform myTransform;

	void Start () {
		rigidbody.useGravity = false;
		rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
		myTransform = transform;
	}

	void FixedUpdate () {
		if (attractor){
			attractor.Attract(myTransform);
		}
	}
	
	void OnTriggerEnter(Collider col){
		GravityAttractor obj = col.GetComponent("GravityAttractor") as GravityAttractor;
		if(obj){
			attractor = obj;
		}
	}
	
	void OnTriggerExit(Collider col){
		attractor = null;
	}
	
}
