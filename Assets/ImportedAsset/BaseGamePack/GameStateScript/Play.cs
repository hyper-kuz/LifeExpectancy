using UnityEngine;
using System.Collections;

public class Play : State
{
	private LifeTimer lifeTimer;
	private SiasakiChanController Siasaki;

	public override void StateStart ()
	{
		lifeTimer = GameObject.FindGameObjectWithTag("LifeTimer").GetComponent<LifeTimer>();
		Siasaki = GameObject.FindGameObjectWithTag("Player").GetComponent<SiasakiChanController>();
	}

	public override void StateUpdate ()
	{

		//終了条件
		if(lifeTimer.isEnd == true){
			this.isEnd = true;
		}

	}

	public override void StateDestroy ()
	{
		Siasaki.FreezePlayer();
	}

}
