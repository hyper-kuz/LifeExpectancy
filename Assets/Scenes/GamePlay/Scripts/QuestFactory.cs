using UnityEngine;
using System.Collections;

public class QuestFactory : MonoBehaviour {
	
	public GameObject PrefabQuest;

	private int _CurrentNumber;
	
	private int TrialNumber = 0;
	public AnimationCurve YakuNumberCurve;
	public GameObject CurrentQuest;
	
	public int CurrentNumber{get{return _CurrentNumber;}}

	public void InstantQuest(){
		TrialNumber++;
		float evalnum = TrialNumber / 100.0f;
		this._CurrentNumber = (int)(YakuNumberCurve.Evaluate(evalnum) * 100.0f + 5.0f + Random.Range(-2.0f,2.0f));
	}

	void GameStart(){

	}

	void GameEnd(){

	}
	
}
