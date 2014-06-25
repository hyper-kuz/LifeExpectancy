using UnityEngine;
using System.Collections;

public class Quest : MonoBehaviour {

	private TextMesh myTextMesh;
	private int _CurrentNumber;

	public int CurrentNumber{get{return _CurrentNumber;}}

	// Use this for initialization
	void Awake () {
		this.myTextMesh = this.GetComponent<TextMesh>();
		this.myTextMesh.richText = true;
		this.HideQuest();
	}

	public void HideQuest(){
		this.gameObject.SetActive(false);
	}
	
	public void SetQuest(int num){
		this.gameObject.SetActive(true);
		this._CurrentNumber = num;
		this.myTextMesh.text =  this.CurrentNumber + " 粒処方セヨ！";
	}

}
