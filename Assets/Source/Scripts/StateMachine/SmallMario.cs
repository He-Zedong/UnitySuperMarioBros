using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mario;
using System;

public class SmallMario : State<AI> 
{
	private static SmallMario _instance;

	private SmallMario()
	{
		if(_instance != null)
		{
			return;
		}

		_instance = this;
	}

	public static SmallMario Instance
	{
		get
		{
			if (_instance == null) 
			{
				new SmallMario ();
			}

			return _instance;
		}
	}

	public override void EnterState(AI _owner)
	{
		Debug.Log ("Enter Small Mario");

	}

	public override void ExitState(AI _owner)
	{
		Debug.Log ("Exit Small Mario");
		//Record Ani State
	}

	public override void UpdateState(AI _owner)
	{
		Debug.Log ("Update Small Mario");

		if (Input.GetKeyDown(KeyCode.B)) 
		{
			_owner.stateMachine.ChangeState (BigMario.Instance);
			_owner.anim.SetTrigger ("TransferToBig");
		}

	}

	public override void PlayWalkAni(AI _owner)
	{
		_owner.anim.SetBool ("SmallMarioWalk",true);
	}
}
