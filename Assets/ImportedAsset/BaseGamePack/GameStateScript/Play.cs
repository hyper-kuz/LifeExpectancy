using UnityEngine;
using System.Collections;

public class Play : State
{
	private LifeTimer lifeTimer;
	private SiasakiChanController Siasaki;
	private QuestFactory questFactory;
	private Yakubin yakubin;

	public GameObject DamageEffect;
	private GameObject cloneDamageEffect;

	float NextTime = 0.0f;
	public float RateQuestTime = 0.5f;
	bool QuestLoadFlag = false;

	public override void StateStart ()
	{
		lifeTimer = GameObject.FindGameObjectWithTag("LifeTimer").GetComponent<LifeTimer>();
		Siasaki = GameObject.FindGameObjectWithTag("Player").GetComponent<SiasakiChanController>();
		questFactory = GameObject.FindGameObjectWithTag("QuestFactory").GetComponent<QuestFactory>();
		yakubin = this.Siasaki.yakubin.GetComponent<Yakubin>();

		questFactory.InstantQuest();
		lifeTimer.isStop = false;

		//コールバック登録
		Siasaki.NomikomuCallback += NomikomiEvent;
		Siasaki.Nomikomu_to_Idle_Callback += Nomikomu_to_Idle_Event;
		Siasaki.CreateYakuCallback += CreateYakuEvent;
		Siasaki.Damage_to_Idle_Callback += Damage_to_Idle_Event;
		questFactory.DestroyQuestCallBack += QuestTimeUpEvent;

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
		if(Siasaki != null){
			Siasaki.FreezePlayer();
		}
		
	}

	//Questの寿命が切れた時に呼ばれるイベント
	void QuestTimeUpEvent(){
		Debug.Log("In Play");
		Destroy(this.questFactory.CurrentQuest);
		this.Siasaki.GetComponent<Animator>().SetTrigger("Damage");
		this.yakubin.DeleteYaku();

		cloneDamageEffect = Instantiate(DamageEffect)as GameObject;
		this.lifeTimer.isStop = true;
	}

	void Damage_to_Idle_Event(){
		this.questFactory.InstantQuest();
		if(cloneDamageEffect != null){
			Destroy(cloneDamageEffect);
		}
		this.lifeTimer.isStop = false;
	}

	//ヤクコさんが薬を飲み込んだ時の処理
	void NomikomiEvent(){
		int YakuNum = this.yakubin.DeleteYaku ();
		int QuestNum = this.questFactory.CurrentNumber;
		int ans = CalculateAddLifeTime (Mathf.Abs (YakuNum - QuestNum));
		
		if(YakuNum == 0){
			this.Siasaki.PlayerState = YakukoState.Idle;
			return;
		}

		this.lifeTimer.GetComponent<LifeTimer> ().AddTime (ans);
		this.questFactory.DestroyCurrentQuest();
		
		NextTime = RateQuestTime + Time.time;
	}

	//ヤクコさんが薬を飲み込んだ後アイドル状態になった時の処理
	void Nomikomu_to_Idle_Event(){
		questFactory.InstantQuest();
	}

	//ヤクコさんが薬ビンを振った時の処理
	void CreateYakuEvent(float AgeSageTime){

		if (AgeSageTime >= 1.0f) {
			yakubin.CreateYaku (1);
		} else if (0.6f < AgeSageTime && AgeSageTime <= 0.9f) {
			yakubin.CreateYaku (2);
		} else if (0.3f < AgeSageTime && AgeSageTime <= 0.6f) {
			yakubin.CreateYaku (3);
		}
		if (0.3f > AgeSageTime) {
			yakubin.CreateYaku (4);
		}

	}

	//飲んだ個数によって寿命を増やすか減らすか決めるメソッド
	int CalculateAddLifeTime (int ans)
	{
		
		int num = 0;
		
		switch (ans) {
		case 0:
			num = 5;
			break;
			
		case 1:
			num = 3;
			break;
			
		case 2:
			num = 2;
			break;
			
		case 3:
			num = 1;
			break;
			
		default:
			num = -1;
			break;
		}
		
		return num;
	}

}
