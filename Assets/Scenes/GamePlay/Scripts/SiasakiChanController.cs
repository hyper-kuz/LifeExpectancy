﻿using UnityEngine;
using System.Collections;

public sealed class myTimer
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
	Damage,
};

public class SiasakiChanController : MonoBehaviour
{ 

	private Animator animator;
	private Script_SpriteStudio_PartsRoot YakukoController;
	private myTimer timer;//上げてから下げての時間を計測するタイマー

	private float AgeSageTime = 0.0f;//上げてから下げての時間
	private bool Freeze = false;

	public YakukoState PlayerState;
	public GameObject Yakuko;
	public GameObject yakubin;
	public GameObject BigHand;
	public GameObject LifeTimer;

	public delegate void NomikomuCallBack();
	public event NomikomuCallBack NomikomuCallback;

	public delegate void Nomikomu_to_Idle_CallBack();
	public event Nomikomu_to_Idle_CallBack Nomikomu_to_Idle_Callback;

	public delegate void CreateYakuCallBack(float AgeSageTime);
	public event CreateYakuCallBack CreateYakuCallback;

	public delegate void DamageCallBack();
	public event DamageCallBack Damage_to_Idle_Callback;

	// Use this for initialization
	void Awake ()
	{
		this.animator = this.GetComponent<Animator> ();
		this.YakukoController = Yakuko.GetComponent<Script_SpriteStudio_PartsRoot> ();

		this.YakukoController.AnimationPlay (3, 0, 1, 1.0f);

		this.PlayerState = YakukoState.Idle;

		this.timer = new myTimer ();
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

		switch(PlayerState){
		case YakukoState.AgeruNomu:
		case YakukoState.Nomikomu:
			Nomikomu_to_Idle_Callback();
			break;

		case YakukoState.Damage:
			Damage_to_Idle_Callback();
			break;
		}

		this.YakukoController.AnimationPlay (6, 0, 1, 1.0f);
		this.PlayerState = YakukoState.Idle;
		BigHand.SetActive (true);
	}

	void Damage(){
		this.YakukoController.AnimationPlay(5,0,1,1.0f);
		this.PlayerState = YakukoState.Damage;

		BigHand.SetActive(false);
	}

	void AgeruNomu(){
		this.YakukoController.AnimationPlay(2,0,1,1.0f);
		this.PlayerState = YakukoState.AgeruNomu;
		BigHand.SetActive (false);

		NomikomuCallback();
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
		this.YakukoController.AnimationPlay (8, 1, 1, 1.0f);
		this.PlayerState = YakukoState.Sageru;
		AgeSageTime = timer.GetAgeSageTime ();
		timer.Reset ();

		CreateYakuCallback(AgeSageTime);
	}
	
	void Nomikomu ()
	{
		this.YakukoController.AnimationPlay (7, 1, 1, 1.0f);
		this.PlayerState = YakukoState.Nomikomu;
		BigHand.SetActive (false);

		NomikomuCallback();
	}

}
