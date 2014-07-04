using UnityEngine;
using System.Collections;

public class Initailizer : State
{

	public GameObject Text;

	private GameObject Player;
	private LifeTimer lifeTimer;

	public override void StateStart ()
	{
		Text = Instantiate(Text)as GameObject;

		Player = GameObject.FindGameObjectWithTag("Player");

		/*
		Player = temp.GetComponent<SiasakiChanController>();
		if(Player == null){
			Debug.LogError("Player don't exist");
		}*/

		Player.SendMessage("Freeze",SendMessageOptions.DontRequireReceiver);

		lifeTimer = GameObject.FindGameObjectWithTag("LifeTimer").GetComponent<LifeTimer>();
	}

	public override void StateUpdate ()
	{

		if(Input.GetKeyDown(KeyCode.Z)){
			this.isEnd = true;
		}

	}

	public override void StateDestroy ()
	{
		lifeTimer.isStop = false;
		Player.SendMessage("ResetPlayer",SendMessageOptions.DontRequireReceiver);
		Destroy (Text);
	}
}
