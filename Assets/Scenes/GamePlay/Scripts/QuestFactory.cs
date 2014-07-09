using UnityEngine;
using System.Collections;

public class QuestFactory : MonoBehaviour {
	
	public GameObject PrefabQuest;			//Questのprefab
	private int _CurrentNumber;				//現在のクリアに必要なクエストのヤク数
	private int TrialNumber = 0;			//Questを更新した数
	public AnimationCurve YakuNumberCurve;	//ヤク数の放物線

	[HideInInspector]
	public GameObject CurrentQuest;			//現在のクエスト
	[HideInInspector]
	public Quest CurrentQuestScript;		//現在のクエストのスクリプト

	public delegate void DestroyQuest();
	public event DestroyQuest DestroyQuestCallBack;

	public int CurrentNumber{get{return _CurrentNumber;}}

	//Questに必要な値を決めてクエストを生成する
	public void InstantQuest(){
		TrialNumber++;
		float evalnum = TrialNumber / 100.0f;
		this._CurrentNumber = (int)(YakuNumberCurve.Evaluate(evalnum) * 100.0f + 5.0f + Random.Range(-2.0f,2.0f));
		CurrentQuest = Instantiate(PrefabQuest)as GameObject;
		CurrentQuestScript = CurrentQuest.GetComponent<Quest>();

		CurrentQuestScript.SetQuest((float)5,this._CurrentNumber);
		CurrentQuestScript.LifeEndCallBack += EndQuestLifeTime;
	}

	void EndQuestLifeTime(){
		DestroyQuestCallBack();
	}

	public void DestroyCurrentQuest(){
		Destroy(CurrentQuest);
	}

}
