using UnityEngine;
using System.Collections;

public class LifeTimer : MonoBehaviour
{

	private TextMesh myTextMesh;
	private int _NowLifeTime;
	private float NextTime;
	private float RateTime = 1.0f;
	private bool _isEnd = false;

	public GameObject LifeUpEffect;
	public GameObject LifeDownEffect;
	public int InitLifeTime = 30;
	public int SumLifeTime = 0;
	public bool isStop = true;

	public int NowLifeTime{ get { return _NowLifeTime; } }
	public bool isEnd{ get { return _isEnd; } }

	// Use this for initialization
	void Awake ()
	{
		this.myTextMesh = this.GetComponent<TextMesh> ();
		if (this.myTextMesh) {
			this.InitTextMesh ();
		}
	}

	void Update ()
	{

		if (isStop == false && _isEnd == false) {
			if (NextTime <= Time.time) {
				NextTime = Time.time + RateTime;
				this.Clock ();
			}
		}

	}

	void EndTimer ()
	{
		this._isEnd = true;
		this.isStop = true;
	}

	void Clock ()
	{
		this._NowLifeTime -= 1;

		if (this._NowLifeTime > -1) {
			ReWriteText();
		} else
			this.EndTimer ();

		SumLifeTime++;

	}

	void ReWriteText(){
		if(this._NowLifeTime < 10)
			this.myTextMesh.text = "余命:<color=red>" + _NowLifeTime + "</color>";
		else
			this.myTextMesh.text = "余命:" + _NowLifeTime;
	}

	//寿命（タイマー）を増やすメソッド,マイナスの値も来るよ
	public void AddTime(int num){

		this._NowLifeTime += num;
		GameObject clone;
		if(num > 0)
			clone = Instantiate(LifeUpEffect)as GameObject;
		else
			clone = Instantiate(LifeDownEffect)as GameObject;

		clone.GetComponent<FadeOut>().SetUp(num);

		ReWriteText();
		NextTime = Time.time + RateTime;
	}

	public void InitTextMesh ()
	{
		this._NowLifeTime = this.InitLifeTime;
		this.myTextMesh.richText = true;
		this.myTextMesh.text = "余命:<color=red>" + _NowLifeTime + "</color>";
		this.isStop = true;
		this.NextTime = 0.0f;
	}

	public void StartLifeTimer ()
	{

	}
	

}
