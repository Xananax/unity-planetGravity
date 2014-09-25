using UnityEngine;
using System.Collections;

public class GravityAttractor : MonoBehaviour {

	public float gravity = 0;
	public float rotationSmoothness = 5f;
	public float density = 5.52f;
	private float volume;
	
	public void Start(){
		if(gravity==0){
			if(collider.GetType() == typeof(SphereCollider)){
				volume = (collider.bounds.size.x * Mathf.PI)/6;
			}else if(collider.GetType() == typeof(CapsuleCollider)){
				volume = ((Mathf.PI * collider.bounds.size.x * collider.bounds.size.y) * collider.bounds.size.z)/4;
			}else{
				volume = collider.bounds.size.x * collider.bounds.size.y * collider.bounds.size.z;
			}
			gravity = -((density/10) * volume)/2;
		}
	}

	public void Attract(Transform body) {
		Vector3 gravityUp = (body.position - transform.position).normalized;
		Vector3 localUp = body.up;

		body.rigidbody.AddForce(gravityUp * gravity);

		Quaternion targetRotation = Quaternion.FromToRotation(localUp,gravityUp) * body.rotation;
		body.rotation = Quaternion.Slerp(body.rotation,targetRotation,rotationSmoothness * Time.deltaTime );
	}  

}
