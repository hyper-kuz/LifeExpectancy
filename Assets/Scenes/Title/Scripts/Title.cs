using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour {

	private int pointer = 0;
	private Animator[] anims;

	public GameObject[] Objs;


	// Use this for initialization
	void Start () {
		anims = new Animator[2];
		anims[0] = Objs[0].GetComponent<Animator>();
		anims[1] = Objs[1].GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetAxisRaw("Vertical") > 0 && pointer == 1){
			anims[pointer].SetBool("isFocus",false);
			pointer = (pointer + 1) % 2;
		}
		if(Input.GetAxisRaw("Vertical") < 0 && pointer == 0){
			anims[pointer].SetBool("isFocus",false);
			pointer = (pointer + (2 - 1) ) % 2;
		}

		anims[pointer].SetBool("isFocus",true);

		if(Input.GetKeyDown(KeyCode.Z)){
			Application.LoadLevel("GamePlay");
		}

	}
}
