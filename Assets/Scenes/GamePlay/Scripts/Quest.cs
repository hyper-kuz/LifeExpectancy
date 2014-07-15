using UnityEngine;
using System.Collections;

[RequireComponent(typeof (TextMesh))]
public class Quest : MonoBehaviour {

	public int YakuNumber = 0;

	private float LifeTime = 5;
	private bool isEnd = false;
	private const string str = "粒処方セヨ！";
	private TextMesh myTextMesh;

	public delegate void LifeEndEvent();
	public event LifeEndEvent LifeEndCallBack;

	public void SetQuest(float LifeTime,int YakuNumber){
		this.LifeTime = LifeTime;
		this.YakuNumber = YakuNumber;
		this.myTextMesh = this.GetComponent<TextMesh>();
		this.myTextMesh.text = this.YakuNumber + str;
	}

	// Update is called once per frame
	void Update () {
	
		LifeTime -= Time.deltaTime;
		if(LifeTime <= 0.0f){
			LifeEndCallBack();
		}

	}

}
