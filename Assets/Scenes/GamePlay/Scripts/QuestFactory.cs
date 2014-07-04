using UnityEngine;
using System.Collections;

public class QuestFactory : MonoBehaviour {
	
	public GameObject PrefabQuest;

	private int _CurrentNumber;
	
	private int TrialNumber = 0;
	public AnimationCurve YakuNumberCurve;
	public GameObject CurrentQuest;
	private bool isQuest = false;
	private float ExistTime = 0;
	public bool TimeOverFlag = false;

	public int CurrentNumber{get{return _CurrentNumber;}}

	public void InstantQuest(){
		TrialNumber++;
		float evalnum = TrialNumber / 100.0f;
		this._CurrentNumber = (int)(YakuNumberCurve.Evaluate(evalnum) * 100.0f + 5.0f + Random.Range(-2.0f,2.0f));
		CurrentQuest = Instantiate(PrefabQuest)as GameObject;

		TextMesh tm = CurrentQuest.GetComponent<TextMesh>();
		tm.text = this._CurrentNumber + "粒処方セヨ!";

		this.isQuest = true;
		this.ExistTime = 5;
		this.TimeOverFlag = false;
	}

	public void DestroyCurrentQuest(){
		Destroy(CurrentQuest);
		this.isQuest = false;
		this.TimeOverFlag = false;
	}

	void Update(){

		if(isQuest == true){
			this.ExistTime -= Time.deltaTime;
			if(this.ExistTime <= 0){
				this.TimeOverFlag = true;
			}
		}

	}

	void GameStart(){

	}

	void GameEnd(){

	}
	
}
