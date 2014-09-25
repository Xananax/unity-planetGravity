using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float moveSpeed = 15f;
	public float jumpMaxStrength = 5f;
	public float holdJumpIncrement = .5f;
	public float jumpMilliseconds = 100f;
	public bool holdToJump = false;
	public float jumpStrength = 0f;
	public float airModifier = 5f;
	[HideInInspector]
	public bool grounded = true;
	private Vector3 moveDirection;
	private float jumpMillisecondsLeft = 0;
	private float _holdJumpMaxStrength = 0;
	private float _holdJumpIncrement = 0;

	void SetUp(){
		grounded = true;
		jumpMillisecondsLeft = jumpMilliseconds;
		if(holdToJump){
			jumpStrength = 0;
		}else{
			jumpStrength = jumpMaxStrength;
		}
		_holdJumpIncrement = holdJumpIncrement * 200;
		_holdJumpMaxStrength = jumpMaxStrength * 1000;
	}

	void Start(){
		SetUp();
	}

	void Jump(){
		rigidbody.AddForce(new Vector3(0,jumpStrength,0), ForceMode.VelocityChange);
		grounded = false;
	}

	void Update() {
		moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical")).normalized;
		if(Input.GetButton("Jump")){
			float timeStep = Time.deltaTime;
			float milliSecondsElapsed = timeStep*1000;
			jumpMillisecondsLeft-=milliSecondsElapsed;
			if(!holdToJump && (jumpMillisecondsLeft>0)){
				Jump();
			}
			else if(jumpMillisecondsLeft>0){
				float jumpInc = _holdJumpIncrement * timeStep; 
				jumpStrength+= jumpInc;
				Mathf.Clamp(jumpStrength,0,_holdJumpMaxStrength);
			}
		}else if(holdToJump && Input.GetButtonUp("Jump")){
			Jump();
			jumpStrength = 0;
		}
	}
	
	void OnCollisionEnter(Collision collision){
		SetUp();
	}

	void FixedUpdate() {
		rigidbody.MovePosition(rigidbody.position + transform.TransformDirection(moveDirection) * (grounded? moveSpeed : (moveSpeed/airModifier)) * Time.deltaTime);
	}
}
