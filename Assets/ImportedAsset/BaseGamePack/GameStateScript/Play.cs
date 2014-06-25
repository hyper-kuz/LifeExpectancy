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
		Siasaki.Quest.GetComponent<Quest>().SetQuest(Random.Range(1,5));
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
