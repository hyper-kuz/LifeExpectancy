using UnityEngine;
using System.Collections;

public class QuestFactory : MonoBehaviour {
	
	public GameObject PrefabQuest;

	private int _CurrentNumber;
	
	private int TrialNumber = 0;
	public AnimationCurve YakuNumberCurve;
	private GameObject CurrentQuest;
	
	public int CurrentNumber{get{return _CurrentNumber;}}

	public void InstantQuest(){
		TrialNumber++;
		float evalnum = TrialNumber / 100.0f;
		this._CurrentNumber = (int)(YakuNumberCurve.Evaluate(evalnum) * 100.0f + 5.0f + Random.Range(-2.0f,2.0f));
		CurrentQuest = Instantiate(PrefabQuest)as GameObject;
		TextMesh tm = CurrentQuest.GetComponent<TextMesh>();
		tm.text = this._CurrentNumber + "粒処方セヨ!";
	}

	public void DestroyCurrentQuest(){
		Destroy(CurrentQuest);
	}

	void GameStart(){

	}

	void GameEnd(){

	}
	
}
