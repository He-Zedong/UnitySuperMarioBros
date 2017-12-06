using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mario;

public class AI : MonoBehaviour {

	public bool switchState { get; set;}

	[SerializeField]
	Transform checkPoint;

	//[SerializeField]
	private Rigidbody2D rig2d;

	//[SerializeField]
	public Animator anim { get; set;}


	private float curSpeed = 3f;
	private float maxSpeed = 5f;
	private float jumpHeight = 350f;
	private bool isFacingRight = true;
	public bool isGrounded { get; set;}

	float checkDistance = 0.05f;

	int hitCount = 0;
	public bool isDead = false;

	LayerMask groundLayer;
	LayerMask enemyLayer;

	//Animator playerAnim;
	AnimatorStateInfo stateInfo;

	public StateMachine<AI> stateMachine { get; set;}

	private void Start()
	{
		rig2d = this.GetComponent<Rigidbody2D>();
		anim = this.GetComponent<Animator>();
		groundLayer = 1 << LayerMask.NameToLayer("Ground");
		enemyLayer = 1 << LayerMask.NameToLayer("Enemy");

		stateMachine = new StateMachine<AI> (this);
		stateMachine.ChangeState (SmallMario.Instance);

		//checkPoint = transform.Find("GroundCheckPoint");
		//playerAnim = GetComponent<Animator>();


		//gameTimer = Time.time;
	}

	private void Update()	
	{
		stateMachine.Update ();

		isGrounded = CheckIsGrounded();

		if (isGrounded) {
			anim.SetBool ("Grounded", true);
			var h = Input.GetAxis ("Horizontal");
			Move (h);
			// Turn Right
			if (h > 0 && !isFacingRight) {
				Reverse ();
			}

			//Turn Left
			if (h < 0 && isFacingRight) {
				Reverse ();
			}

			// Jump
			if (Input.GetKeyDown (KeyCode.Space) && isGrounded) {
				Debug.Log ("True");
				Jump ();
			}
		} else 
		{
			anim.SetBool ("Grounded", false);
		}
	}
	void Reverse()
	{
		isFacingRight = !isFacingRight;
		var scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

	public void Move(float dic)
	{
		rig2d.velocity = new Vector2(dic * curSpeed, rig2d.velocity.y);
		anim.SetFloat("Speed", Mathf.Abs(dic * curSpeed));
		Debug.Log (Mathf.Abs(dic * curSpeed));
		//stateMachine.PlayWalkAni ();
		//anim.SetFloat("Speed", Mathf.Abs(dic * curSpeed));
		//anim.SetFloat("MoveSpeed", curSpeed);
	}

	void Jump()
	{
		rig2d.AddForce(new Vector2(0, jumpHeight));
		anim.SetBool ("Grounded",false);
	}

	private bool CheckIsGrounded()
	{
		Vector2 check = checkPoint.position;
		RaycastHit2D hit = Physics2D.Raycast(check, Vector2.down, checkDistance, groundLayer.value);

		if (hit.collider != null)
		{
			//            anim.SetBool("IsGrounded", true);
			//            isGrounded = true;
			return true;
		}
		else
		{
			//            anim.SetBool("IsGrounded", false);
			return false;
		}
	}
}
