using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;

public class StateManager : Singleton<StateManager>
{

	private GameObject currentTask;
	private State currentScript;

	//複数の遷移専用のGameObject
	public GameObject State_Initalizer;
	public GameObject State_Play;
	public GameObject State_End;

	public bool start = true;

	// Use this for initialization
	void Start ()
	{

		//初期化状態を生成.
		currentTask = myInstantiate (State_Initalizer);
		
	}

	GameObject myInstantiate (GameObject obj)
	{
		if(obj == null){
			Debug.LogError(currentScript.TransitionState.ToString() + " is None!");
			Application.Quit();
			return null;
		}
		
		GameObject clone = Instantiate (obj)as GameObject;
		currentScript = clone.GetComponent<State> ();
		clone.name = obj.name;
		return clone;
	}
	
	// Update is called once per frame
	void Update ()
	{

		//状態を監視する
		if (currentScript.isEnd == true) {

			//前の状態を終了処理させる.
			if (currentTask != null && currentScript.TransitionState != StateEnum.e_NONE){
				Destroy (currentTask);
			}
			else {
	
				//Switch文で状態を変更.
				switch (currentScript.TransitionState) {
				
				//初期化状態.
				case StateEnum.e_INIT:
					start = true;
					currentTask = myInstantiate (State_Initalizer);
					break;

				//ゲームプレイ中の状態.
				case StateEnum.e_PLAY:
					start = false;
					currentTask = myInstantiate (State_Play);
					break;

				case StateEnum.e_END:
					currentTask = myInstantiate(State_End);
					break;
				}


			}
		}

	}
	
}
