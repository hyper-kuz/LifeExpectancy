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
		lifeTimer.isStop = false;
		GameObject.FindGameObjectWithTag("QuestFactory").SendMessage("InstantQuest");
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
