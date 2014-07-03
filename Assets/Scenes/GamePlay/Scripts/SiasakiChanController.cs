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
	AgeruIdle,
	AgeruNomu,
	Sageru,
	Nomikomu,
};

public class SiasakiChanController : MonoBehaviour
{ 

	private Animator animator;
	private Script_SpriteStudio_PartsRoot YakukoController;
	private YakukoTimer timer;//上げてから下げての時間を計測するタイマー
	private Yakubin yakubinScript;
	private QuestFactory YakukoQuestFactory;
	private float AgeSageTime = 0.0f;//上げてから下げての時間
	private bool Freeze = false;

	public YakukoState PlayerState;
	public GameObject Yakuko;
	public GameObject Yakubin;
	public GameObject BigHand;
	public GameObject questFactory;
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
		this.YakukoQuestFactory = this.questFactory.GetComponent<QuestFactory>();

	}
	
	// Update is called once per frame
	void Update ()
	{
		bool InputZ = false, InputX = false, InputC = false,InputV = false;

		if (Freeze == false) {

			InputZ = Input.GetKeyDown (KeyCode.Z);
			InputX = Input.GetKeyDown (KeyCode.X);
			InputC = Input.GetKeyDown (KeyCode.C);
			InputV = Input.GetKeyDown (KeyCode.V);

			animator.SetBool ("InputZ",InputZ);
			animator.SetBool ("InputX",InputX);
			animator.SetBool ("InputC",InputC);
			animator.SetBool ("InputV",InputV);

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
		if (PlayerState == YakukoState.Nomikomu || PlayerState == YakukoState.AgeruNomu) {
			this.YakukoQuestFactory.InstantQuest();
		}

		this.YakukoController.AnimationPlay (5, 0, 1, 1.0f);
		this.PlayerState = YakukoState.Idle;
		BigHand.SetActive (true);
	}

	void AgeruNomu(){
		this.YakukoController.AnimationPlay(2,0,1,1.0f);
		this.PlayerState = YakukoState.AgeruNomu;
		BigHand.SetActive (false);
		
		int YakuNum = this.yakubinScript.DeleteYaku ();
		int QuestNum = this.YakukoQuestFactory.CurrentNumber;
		int ans = CalculateAddLifeTime (Mathf.Abs (YakuNum - QuestNum));
		
		if(YakuNum == 0){
			this.PlayerState = YakukoState.Idle;
			return;
		}
		
		this.LifeTimer.GetComponent<LifeTimer> ().AddTime (ans);
		this.YakukoQuestFactory.DestroyCurrentQuest();
	}

	void AgeruIdle ()
	{
		this.YakukoController.AnimationPlay(1,0,1,1.0f);
		this.PlayerState = YakukoState.AgeruIdle;
		timer.Start ();
	}

	void Ageru ()
	{
		this.YakukoController.AnimationPlay (0, 1, 1, 1.0f);
		this.PlayerState = YakukoState.Ageru;
		timer.Start ();
	}

	void Sageru ()
	{
		this.YakukoController.AnimationPlay (7, 1, 1, 1.0f);
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
		this.YakukoController.AnimationPlay (6, 1, 1, 1.0f);
		this.PlayerState = YakukoState.Nomikomu;
		BigHand.SetActive (false);

		int YakuNum = this.yakubinScript.DeleteYaku ();
		int QuestNum = this.YakukoQuestFactory.CurrentNumber;
		int ans = CalculateAddLifeTime (Mathf.Abs (YakuNum - QuestNum));

		if(YakuNum == 0){
			this.PlayerState = YakukoState.Idle;
			return;
		}

		this.LifeTimer.GetComponent<LifeTimer> ().AddTime (ans);
		this.YakukoQuestFactory.DestroyCurrentQuest();

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
