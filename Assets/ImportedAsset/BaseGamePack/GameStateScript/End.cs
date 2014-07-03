using UnityEngine;
using System.Collections;

public class End : State
{

	public GameObject EndText;

	public override void StateStart ()
	{
		Instantiate(EndText);
	}

	public override void StateUpdate ()
	{

		if(Input.GetKeyDown(KeyCode.Z)){
			Application.LoadLevel("Title");
		}
	
	}

	public override void StateDestroy ()
	{
			
	}

}
