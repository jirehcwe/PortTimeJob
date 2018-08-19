using UnityEngine.Audio;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {

	public static AudioManager instance;

	public Sound[] commonSounds;

	public Sound[] iconSounds;
	public Sound[] loadingScreenSounds;
	public Sound[] mainMainMenuSounds;
	public Sound[] menuSounds;
	public Sound[] serverRoomSounds;
	public Sound[] citosOnlySounds;
	public Sound[] catchingItemsSounds;
	public Sound[] crateFishingSounds;
	public Sound[] sortifySounds;
	public Sound[] symmetrySounds;

	private Sound[] currentArray;

	private string previousScene;


	// Use this for initialization
	void Awake () {

		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
			return;
		}

		DontDestroyOnLoad (gameObject);

		SceneManager.activeSceneChanged += SceneChanged; //whenever active scene is changed, the SceneChanged command is called

		previousScene = SceneManager.GetActiveScene ().name;
		LoadSounds (previousScene);

		foreach (Sound s in commonSounds) {
			s.source = gameObject.AddComponent<AudioSource> ();
			s.source.clip = s.clip;
			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
		}

	}

	public void PlayCommonSound(string name){
		Sound s = Array.Find (commonSounds, sound => sound.name == name);
		if (s == null) {
			Debug.LogWarning ("AudioManager.PlayCommonSound: Sound " + name +  " not found");
			return;
		}
		s.source.Play ();
	}

	public void StopCommonSound(string name){
		Sound s =Array.Find (commonSounds, sound => sound.name == name);
		if (s == null) {
			Debug.LogWarning ("AudioManager.StopCommonSound: Sound " + name +  " not found");
			return;
		}
		s.source.Stop ();
	}

	public void PlaySound(string name){
		Sound s = Array.Find (currentArray, sound => sound.name == name);
		if (s == null) {
			Debug.LogWarning ("AudioManager.PlaySound: Sound " + name +  " not found. Current Scene: " + SceneManager.GetActiveScene().name);
			return;
		}
		s.source.Play ();
	}

	public void StopSound(string name){
		Sound s =Array.Find (currentArray, sound => sound.name == name);
		if (s == null) {
			Debug.LogWarning ("AudioManager.StopSound: Sound " + name +  " not found. Current Scene: " + SceneManager.GetActiveScene().name);
			return;
		}
		s.source.Stop ();
	}
		
	public AudioSource GetSource(string name){
		Sound s = Array.Find (currentArray, sound => sound.name == name);
		if (s == null) {
			Debug.LogWarning ("AudioManager.GetSource: Sound " + name +  " not found. Current Scene: " + SceneManager.GetActiveScene().name);
			return null;
		}
		return s.source;
	}

	public AudioSource GetCommonSource(string name){
		Sound s = Array.Find (commonSounds, sound => sound.name == name);
		if (s == null) {
			Debug.LogWarning ("AudioManager.GetCommonSource: Sound " + name +  " not found");
			return null;
		}
		return s.source;
	}

	//this is called upon scene change
	private void SceneChanged(Scene uselessScene, Scene newScene){
		
		if(previousScene != newScene.name){
			UnLoadSounds (previousScene);
			LoadSounds (newScene.name);
			previousScene = newScene.name;
		}
	}

	void LoadSounds(string name){
		switch (name) {
		case "Icon":
			currentArray = iconSounds;
			foreach (Sound s in iconSounds) {
				s.source = gameObject.AddComponent<AudioSource> ();
				s.source.clip = s.clip;
				s.source.volume = s.volume;
				s.source.pitch = s.pitch;
				s.source.loop = s.loop;
				s.source.playOnAwake = false;
			}
			break;

		case "LoadingScreen":
			currentArray = loadingScreenSounds;
			foreach (Sound s in loadingScreenSounds) {
				s.source = gameObject.AddComponent<AudioSource> ();
				s.source.clip = s.clip;
				s.source.volume = s.volume;
				s.source.pitch = s.pitch;
				s.source.loop = s.loop;
				s.source.playOnAwake = false;
			}
			break;

		case "MainMainMenu":
			currentArray = mainMainMenuSounds;
			foreach (Sound s in mainMainMenuSounds) {
				s.source = gameObject.AddComponent<AudioSource> ();
				s.source.clip = s.clip;
				s.source.volume = s.volume;
				s.source.pitch = s.pitch;
				s.source.loop = s.loop;
				s.source.playOnAwake = false;
			}
			break;

		case "Menu":
			currentArray = menuSounds;
			foreach (Sound s in menuSounds) {
				s.source = gameObject.AddComponent<AudioSource> ();
				s.source.clip = s.clip;
				s.source.volume = s.volume;
				s.source.pitch = s.pitch;
				s.source.loop = s.loop;
				s.source.playOnAwake = false;
			}
			break;

		case "ServerRoom":
			currentArray = serverRoomSounds;
			foreach (Sound s in serverRoomSounds) {
				s.source = gameObject.AddComponent<AudioSource> ();
				s.source.clip = s.clip;
				s.source.volume = s.volume;
				s.source.pitch = s.pitch;
				s.source.loop = s.loop;
				s.source.playOnAwake = false;
			}
			break;

		case "Citos only":
			currentArray = citosOnlySounds;
			foreach (Sound s in citosOnlySounds) {
				s.source = gameObject.AddComponent<AudioSource> ();
				s.source.clip = s.clip;
				s.source.volume = s.volume;
				s.source.pitch = s.pitch;
				s.source.loop = s.loop;
				s.source.playOnAwake = false;
			}
			break;

		case "CatchingItems":
			currentArray = catchingItemsSounds;
			foreach (Sound s in catchingItemsSounds) {
				s.source = gameObject.AddComponent<AudioSource> ();
				s.source.clip = s.clip;
				s.source.volume = s.volume;
				s.source.pitch = s.pitch;
				s.source.loop = s.loop;
				s.source.playOnAwake = false;
			}
			break;

		case "CrateFishing":
			currentArray = crateFishingSounds;
			foreach (Sound s in crateFishingSounds) {
				s.source = gameObject.AddComponent<AudioSource> ();
				s.source.clip = s.clip;
				s.source.volume = s.volume;
				s.source.pitch = s.pitch;
				s.source.loop = s.loop;
				s.source.playOnAwake = false;
			}
			break;

		case "Sortify":
			currentArray = sortifySounds;
			foreach (Sound s in sortifySounds) {
				s.source = gameObject.AddComponent<AudioSource> ();
				s.source.clip = s.clip;
				s.source.volume = s.volume;
				s.source.pitch = s.pitch;
				s.source.loop = s.loop;
				s.source.playOnAwake = false;
			}
			break;

		case "Symmetry":
		case "SymmetryInstruction":
			currentArray = symmetrySounds;
			foreach (Sound s in symmetrySounds) {
				s.source = gameObject.AddComponent<AudioSource> ();
				s.source.clip = s.clip;
				s.source.volume = s.volume;
				s.source.pitch = s.pitch;
				s.source.loop = s.loop;
				s.source.playOnAwake = false;
			}
			break;

		default:
			Debug.LogWarning ("AudioManager.LoadSounds: scene " + name + " not found");
			break;

		}
	}

	void UnLoadSounds(string name){
		switch (name) {
		case "Icon":
			foreach (Sound s in iconSounds) {
				Destroy (s.source);
			}
			break;

		case "LoadingScreen":
			currentArray = loadingScreenSounds;
			foreach (Sound s in loadingScreenSounds) {
				Destroy (s.source);
			}
			break;

		case "MainMainMenu":
			currentArray = mainMainMenuSounds;
			foreach (Sound s in mainMainMenuSounds) {
				Destroy (s.source);
			}
			break;

		case "Menu":
			currentArray = menuSounds;
			foreach (Sound s in menuSounds) {
				Destroy (s.source);
			}
			break;

		case "ServerRoom":
			currentArray = serverRoomSounds;
			foreach (Sound s in serverRoomSounds) {
				Destroy (s.source);
			}
			break;

		case "Citos only":
			currentArray = citosOnlySounds;
			foreach (Sound s in citosOnlySounds) {
				Destroy (s.source);
			}
			break;

		case "CatchingItems":
			currentArray = catchingItemsSounds;
			foreach (Sound s in catchingItemsSounds) {
				Destroy (s.source);
			}
			break;

		case "CrateFishing":
			foreach (Sound s in crateFishingSounds) {
				Destroy (s.source);
			}
			break;

		case "Sortify":
			currentArray = sortifySounds;
			foreach (Sound s in sortifySounds) {
				Destroy (s.source);
			}
			break;

		case "Symmetry":
		case "SymmetryInstruction":
			currentArray = symmetrySounds;
			foreach (Sound s in symmetrySounds) {
				Destroy (s.source);
			}
			break;

		default:
			Debug.LogWarning ("AudioManager.SceneChanged: previous scene " + name + " not found");
			break;

		}
	}
}

[System.Serializable]
public class Sound{

	public string name;

	public AudioClip clip;

	[Range(0f,1f)]
	public float volume;

	[Range(0.1f,5f)]
	public float pitch;

	public bool loop;

	[HideInInspector]
	public AudioSource source;
}