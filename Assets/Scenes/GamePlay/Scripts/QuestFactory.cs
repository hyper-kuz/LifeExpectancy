using UnityEngine;
using System.Collections;

public class QuestFactory : MonoBehaviour {

	//Questのprefab
	public GameObject PrefabQuest;

	//現在のクリアに必要なクエストのヤク数
	private int _CurrentNumber;

	//Questを更新した数
	private int TrialNumber = 0;

	//ヤク数の放物線
	public AnimationCurve YakuNumberCurve;

	[HideInInspector]
	public GameObject CurrentQuest;
	public Quest CurrentQuestScript;

	private bool isQuest = false;

	public int CurrentNumber{get{return _CurrentNumber;}}

	//Questに必要な値を決めてクエストを生成する
	public void InstantQuest(){
		TrialNumber++;
		float evalnum = TrialNumber / 100.0f;
		this._CurrentNumber = (int)(YakuNumberCurve.Evaluate(evalnum) * 100.0f + 5.0f + Random.Range(-2.0f,2.0f));
		CurrentQuest = Instantiate(PrefabQuest)as GameObject;
		CurrentQuestScript = CurrentQuest.GetComponent<Quest>();

		CurrentQuestScript.SetQuest((float)5,this._CurrentNumber);
		CurrentQuestScript.LifeEndCallBack += DestroyCurrentQuest;

		this.isQuest = true;
	}

	public void DestroyCurrentQuest(){
		Destroy(CurrentQuest);
	}

}
