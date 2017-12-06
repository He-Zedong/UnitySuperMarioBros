using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mario;
using System;

public class BigMario : State<AI> 
{
	private static BigMario _instance;

	private BigMario()
	{
		if(_instance != null)
		{
			return;
		}

		_instance = this;
	}

	public static BigMario Instance
	{
		get
		{
			if (_instance == null) 
			{
				new BigMario ();
			}

			return _instance;
		}
	}

	public override void EnterState(AI _owner)
	{
		Debug.Log ("Enter Big Mario");
		//_owner.anim.SetTrigger ("TransferToBig");
	}

	public override void ExitState(AI _owner)
	{
		Debug.Log ("Exit Big Mario");
	}

	public override void UpdateState(AI _owner)
	{
		Debug.Log ("Update Big Mario");
		if (Input.GetKeyDown(KeyCode.S)) 
		{
			_owner.stateMachine.ChangeState (SmallMario.Instance);
			_owner.anim.SetTrigger ("TransferToSmall");
		}
	}

	public override void PlayWalkAni(AI _owner)
	{
		_owner.anim.SetBool ("BigMarioWalk",true);
	}
}
