using UnityEngine;
using System.Collections;

public class End : State
{
	public GameObject EndText;
	public GameObject SumLifeNumberText;
	
	public override void StateStart ()
	{
		GameObject LifeTimer = GameObject.FindGameObjectWithTag("LifeTimer");

		Instantiate(EndText);
		GameObject clone = Instantiate(SumLifeNumberText)as GameObject;
		clone.GetComponent<TextMesh>().text = "Score:" + LifeTimer.GetComponent<LifeTimer>().SumLifeTime;

	}

	public override void StateUpdate ()
	{

		if(Input.GetKeyDown(KeyCode.Z)){
			Application.LoadLevel("Title");
		}
	
	}

	public override void StateOnGUI ()
	{
	}

	public override void StateDestroy ()
	{
			
	}

}
