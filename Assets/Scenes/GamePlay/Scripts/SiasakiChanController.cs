using UnityEngine;
using System.Collections;

public sealed class YakukoTimer
{

	private float _NowTime = 0.0f;
	private bool _isWork = false;

	public bool isWork{ get { return _isWork; } }

	public void Stop ()
	{
		_isWork = false;
	}

	public void Start ()
	{
		_isWork = true;
	}

	public void Reset ()
	{
		_isWork = false;
		_NowTime = 0.0f;
	}

	public float GetAgeSageTime ()
	{
		return _NowTime;
	}

	public void Update (float currentTime)
	{

		if (this._isWork == true) {
			_NowTime += currentTime;
		}

	}

}

public enum YakukoState
{
	Idle,
	Ageru,
	Sageru,
	Modosu,
	Nomikomu,
};

public class SiasakiChanController : MonoBehaviour
{ 

	private Animator animator;
	private Script_SpriteStudio_PartsRoot YakukoController;
	private YakukoTimer timer;//上げてから下げての時間を計測するタイマー
	private Yakubin yakubinScript;
	private Quest YakukoQuest;
	private float AgeSageTime = 0.0f;//上げてから下げての時間
	private bool Freeze = false;

	public YakukoState PlayerState;
	public GameObject Yakuko;
	public GameObject Yakubin;
	public GameObject BigHand;
	public GameObject Quest;
	public GameObject LifeTimer;

	// Use this for initialization
	void Awake ()
	{
		this.animator = this.GetComponent<Animator> ();
		this.YakukoController = Yakuko.GetComponent<Script_SpriteStudio_PartsRoot> ();

		this.YakukoController.AnimationPlay (3, 0, 1, 1.0f);

		PlayerState = YakukoState.Idle;

		this.timer = new YakukoTimer ();

		this.yakubinScript = Yakubin.GetComponent<Yakubin> ();
		this.YakukoQuest = this.Quest.GetComponent<Quest> ();

	}
	
	// Update is called once per frame
	void Update ()
	{
		bool doAgeruFlag = false, doSageruFlag = false, doNomikomuFlag = false, doModosuFlag = false;

		if (Freeze == false) {

			if (Input.GetKeyDown (KeyCode.Z)) {
				doAgeruFlag = true;
			}

			if (Input.GetKeyDown (KeyCode.X)) {
				doSageruFlag = true;
			}

			if (Input.GetKeyDown (KeyCode.C)) {
				doNomikomuFlag = true;
			}

			if (Input.GetKeyDown (KeyCode.V)) {
				doModosuFlag = true;
			}

			animator.SetBool ("doAgeru", doAgeruFlag);
			animator.SetBool ("doSageru", doSageruFlag);
			animator.SetBool ("doNomikomu", doNomikomuFlag);
			animator.SetBool ("doModosu", doModosuFlag);

			timer.Update (Time.deltaTime);
		}

	}

	//キャラクターの動きを止める
	public void FreezePlayer ()
	{
		Freeze = true;
	}

	//フリーズされたキャラクターを動かす
	public void ResetPlayer ()
	{
		Freeze = false;
	}

	//ここから下はAnimation Event関数に登録された関数群
	void Idle ()
	{

		//Nomikomuアニメーションの後だったらクエスト更新
		if (PlayerState == YakukoState.Nomikomu) {
			this.YakukoQuest.SetQuest (Random.Range (1, 5));
		}

		this.YakukoController.AnimationPlay (3, 0, 1, 1.0f);
		this.PlayerState = YakukoState.Idle;
		BigHand.SetActive (true);
	}

	void Modosu ()
	{
		this.PlayerState = YakukoState.Modosu;
	}

	void Ageru ()
	{
		this.YakukoController.AnimationPlay (0, 1, 1, 1.0f);
		this.PlayerState = YakukoState.Ageru;
		timer.Start ();
	}

	void Sageru ()
	{
		this.YakukoController.AnimationPlay (2, 1, 1, 1.0f);
		this.PlayerState = YakukoState.Sageru;
		AgeSageTime = timer.GetAgeSageTime ();
		timer.Reset ();

		if (AgeSageTime >= 1.0f) {
			yakubinScript.CreateYaku (1);
		} else if (0.6f < AgeSageTime && AgeSageTime <= 0.9f) {
			yakubinScript.CreateYaku (2);
		} else if (0.3f < AgeSageTime && AgeSageTime <= 0.6f) {
			yakubinScript.CreateYaku (3);
		}
		if (0.3f > AgeSageTime) {
			yakubinScript.CreateYaku (4);
		}
	}

	void Nomikomu ()
	{
		this.YakukoController.AnimationPlay (1, 1, 1, 1.0f);
		this.PlayerState = YakukoState.Nomikomu;
		BigHand.SetActive (false);

		int YakuNum = this.yakubinScript.DeleteYaku ();
		int QuestNum = this.YakukoQuest.CurrentNumber;
		int ans = CalculateAddLifeTime (Mathf.Abs (YakuNum - QuestNum));

		if(YakuNum == 0){
			this.PlayerState = YakukoState.Idle;
			return;
		}

		this.LifeTimer.GetComponent<LifeTimer> ().AddTime (ans);

		this.YakukoQuest.HideQuest ();
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
