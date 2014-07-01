using UnityEngine;
using System.Collections;

[RequireComponent(typeof (TextMesh))]
public class Quest : MonoBehaviour {

	public int YakuNumber = 0;

	private int LifeTime = 5;
	private bool isEnd = false;
	private const string str = "粒処方セヨ！";
	private TextMesh myTextMesh;

	void SetQuest(int LifeTime,int YakuNumber){
		this.LifeTime = LifeTime;
		this.YakuNumber = YakuNumber;
		this.myTextMesh = this.GetComponent<TextMesh>();
		this.myTextMesh.text = this.YakuNumber + str;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
