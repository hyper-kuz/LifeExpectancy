using UnityEngine;
using System.Collections;

public class Yakubin : MonoBehaviour {

	public GameObject Yaku;
	public GameObject Marker;
	public GameObject YakuBox;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void CreateYaku(int num){

		for(int i = 0; i < num; i++){
			GameObject cloneYaku = Instantiate(Yaku)as GameObject;
			cloneYaku.AddComponent<Rigidbody2D>();
			Vector2 vec = new Vector2(Random.Range(-1.0f,0.0f),Random.Range(-1.0f,0.0f));
			cloneYaku.rigidbody2D.AddForce(vec * 2.0f,ForceMode2D.Impulse);
			cloneYaku.transform.Rotate(new Vector3(0.0f,0.0f,Random.Range(0.0f,180.0f)));
			cloneYaku.transform.position = new Vector3(Marker.transform.position.x,Marker.transform.position.y,Marker.transform.position.z);
			cloneYaku.transform.parent = YakuBox.transform;
		}

	}

	public int DeleteYaku(){
	
		int temp = YakuBox.transform.childCount;

		foreach(Transform child in YakuBox.transform){
			Destroy (child.gameObject);
		}

		return temp;
	}

}
