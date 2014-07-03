using UnityEngine;
using System.Collections;

public class Initailizer : State
{

	public GameObject Text;

	private SiasakiChanController Player;
	private LifeTimer lifeTimer;

	public override void StateStart ()
	{
		Text = Instantiate(Text)as GameObject;

		GameObject temp = GameObject.FindGameObjectWithTag("Player")as GameObject;

		Player = temp.GetComponent<SiasakiChanController>();
		if(Player == null){
			Debug.LogError("Player don't exist");
		}

		Player.FreezePlayer();

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
		Player.ResetPlayer();
		Destroy (Text);
	}
}
