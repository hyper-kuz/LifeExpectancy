using UnityEngine;
using System.Collections;

/// <summary>
/// Volume設定用の構造体
/// </summary>
[System.Serializable]
public class SoundVolume
{
	public float BGM = 1.0f;
	public float Voice = 1.0f;
	public float SE = 1.0f;
	public bool BGM_Mute = false;
	public bool SE_Mute = false;
	
	public void Init ()
	{
		BGM = 1.0f;
		Voice = 1.0f;
		SE = 1.0f;
		BGM_Mute = false;
		SE_Mute = false;
	}
}

[System.Serializable]
public class SoundSource
{
	public AudioClip clip;
	public string Name;
}

/// <summary>
/// 音源を扱うシングルトン.
/// どこからでも参照出来るのでプロパティを通してインスタンスを得られる.
/// </summary>
public class SoundManager : Singleton<SoundManager>
{

	//BGM
	private AudioSource BGMSource;
	// SE
	private AudioSource[] SEsources = new AudioSource[16];

	//音量
	public SoundVolume volume = new SoundVolume ();

	//BGM
	public SoundSource[] BGMs;
	// SE
	public SoundSource[] SEs;

	void Start ()
	{

		BGMSource = gameObject.AddComponent<AudioSource> ();
		BGMSource.loop = true;

		for(int i = 0; i < SEsources.Length; i++){
			SEsources[i] = gameObject.AddComponent<AudioSource>();
		}


	}

	void Update ()
	{
	
	}

	public void PlayBGM (string name)
	{

		foreach (SoundSource ss in BGMs) {
			if (ss.Name.Equals (name) == true) {
				BGMSource.clip = ss.clip;
				BGMSource.Play ();
			}
		}

	}

	public void PlaySE (string name)
	{

		foreach (SoundSource ss in SEs) {
			if (ss.Name.Equals (name) == true) {

				foreach (AudioSource audiosource in SEsources) {
					if (audiosource.isPlaying == false) {
						Debug.Log ("uhun");
						audiosource.clip = ss.clip;
						audiosource.Play ();
						return;
					}
				}
			}
		}
	}
	

}