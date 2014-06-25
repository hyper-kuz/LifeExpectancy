using UnityEngine;
using System.Collections;

public class FadeOut : MonoBehaviour {

	void DestroyMe(){
		Destroy(this.gameObject);
	}

	public void SetUp(int num){
		TextMesh tm = this.GetComponent<TextMesh>();
		if(num > 0)
			tm.text = "寿命 " + "+" + num + "秒";
		else
			tm.text = "寿命 " + num + "秒";

		this.GetComponent<Animator>().SetTrigger("Set");
	}
}
