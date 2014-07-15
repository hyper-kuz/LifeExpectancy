using UnityEngine;
using System.Collections;

public class State : MonoBehaviour {
	
	public StateEnum StateType;
	public StateEnum TransitionState;
	[HideInInspector]
	public bool isEnd;
		
	// Use this for initialization
	void Start () {
		StateStart();
	}
	
	// Update is called once per frame
	void Update () {
		StateUpdate();
	}
	
	void OnDestroy(){
		StateDestroy();
	}

	void OnGUI(){
		StateOnGUI();
	}

	protected void EndState(){
		this.isEnd = true;
	}

	public virtual void StateOnGUI(){

	}
	
	public virtual void StateStart(){
		
	}
	
	public virtual void StateUpdate(){
		
	}
	
	public virtual void StateDestroy(){
		
	}

	public GameObject myInstantiate(GameObject obj){
		GameObject clone = Instantiate(obj)as GameObject;
		clone.name = obj.name;

		return clone;

	}
}
