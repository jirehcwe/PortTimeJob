using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour {

	private GameObject levelManager;
	private LevelManager levelManagerScript;
	private GameObject main;
	private PlayerInput mainScript;
	private GameObject hook;
	//private HookScript hookScript;

	public Texture shipBackground;
	public Texture skyBackground;

	public float speed;
	public float maxSpeed;	//max speed of background scrolling
	public float yPos;		//yPos of the background. used as parameter for offset
	public bool sceneChanged;

	private float height;	//to scale offset to same speed as translate

	private float initTime;
	public float depth;
	public float depthReached;
	public Text depthText;
	public Text instructions;
	public Text winText;
	public Canvas gameCanvas;

	private float changingY;
	private float endingY;	//used to sync the 2nd background position and camera view.

	private bool scrollStopped;
	private bool canStartMovingUp = false;
	private bool startingEndingAnimation = false;
	private bool windSoundPlayed = false;

	private AudioSource BGMSoundSource;

	void Start () {
		//referencing main and hook script
		levelManager = GameObject.Find("LevelManager(Clone)");
		levelManagerScript = levelManager.GetComponent<LevelManager> ();
		main = GameObject.Find ("PlayerInput");
		mainScript = main.GetComponent<PlayerInput> ();
		hook = GameObject.Find ("Hook");
		//hookScript = hook.GetComponent<HookScript> ();

		yPos = 0;
		maxSpeed = 1.5f;
		initTime = 0;
		height = GetComponent<MeshCollider> ().bounds.size.y;
		sceneChanged = false;
		depth = 0;


		changingY = CalculateChangingOffsetValue ();
		endingY = CalculateFinalOffsetValue ();

		scrollStopped = false;

		BGMSoundSource = AudioManager.instance.GetSource ("Crate Fishing BGM");
	}

	// Update is called once per frame
	void Update () {
		if (speed == 0 && (!scrollStopped)) {
			return;
		}

		if (initTime == 0 && speed != 0) {
			initTime = Time.time;
		}

		if (!scrollStopped) {
			yPos -= Time.deltaTime * speed * (1 / height);
			yPos = (yPos < -1.0f) ? yPos + 1.0f : yPos;

			depth = (Time.time - initTime)*maxSpeed*5;
			depthText.text = "Depth: " + depth.ToString("n0")+"m";
		}else{
			yPos += Time.deltaTime * speed * (1 / height);
			yPos = (yPos > 1.0f) ? yPos - 1.0f : yPos;

			depthText.text = "Depth Reached: " + depthReached.ToString("n0")+"m";
			depth -= Time.deltaTime * speed * 5;
			if (LevelManager.gameCount <= 1) {
				if (depth <= depthReached - 40f) {
					StopScroll ();
					GameWon ();
					return;
				}
			} else {
				maxSpeed = 30f;
				if(GetComponent<Renderer> ().material.mainTextureOffset.y >= changingY && GetComponent<Renderer> ().material.mainTexture == shipBackground) {
					GetComponent<Renderer> ().material.mainTexture = skyBackground;
					GameWon ();
					gameCanvas.transform.GetChild (0).gameObject.SetActive (false);
					gameCanvas.transform.GetChild (2).gameObject.SetActive (false);
					main.SendMessage ("DeactivateHearts");
				}
				if (startingEndingAnimation) {
					if (Input.touchCount != 0) {
						AudioManager.instance.PlayCommonSound ("click");
						//deactivate box
						gameCanvas.transform.GetChild (3).gameObject.SetActive (false);
						//hook goes up and out of scene, quickly
						EndingAnimation();
						//scroller stops
						//boom! sprite appears, indicating that it came from above.
						//parcel appears, pause game, fade to white.
						//levelManager.SendMessage ("GameEnded");
					}
				}
				BGMSoundSource.volume -= 0.005f;
			}
			if (speed < maxSpeed && (canStartMovingUp)) {
				speed += 0.5f;
			}

		}

		GetComponent<Renderer> ().material.mainTextureOffset= new Vector2 (0, yPos);

	}

	void StartScroll(){
		speed = maxSpeed;
	}

	void StopScroll(){
		speed = 0;
	}

	void BottomReached(){
		if (!sceneChanged) {
			speed = maxSpeed * 3f;
			if (GetComponent<Renderer> ().material.mainTextureOffset.y >= changingY) {	//offset a bit earlier to let hook reset position and so ship doesnt appear suddenly.
				GetComponent<Renderer> ().material.mainTexture = shipBackground;
				hook.SendMessage ("ControlHook");
				AudioManager.instance.PlaySound ("Control Hook");
				sceneChanged = true;
			} else {
				return;
			}
		} 

		if (GetComponent<Renderer> ().material.mainTextureOffset.y <= (endingY)){
			if (!scrollStopped) {
				GetComponent<Renderer> ().material.mainTextureOffset = new Vector2(GetComponent<Renderer> ().material.mainTextureOffset.x,endingY);
				depthReached = depth;
				speed = 0;
				hook.SendMessage ("ChangeSprite");
				scrollStopped = true;
				StartCoroutine (Wait ());
			}
		}
	}

	float CalculateFinalOffsetValue(){
		float cameraLength = mainScript.limitY - mainScript.originY;
		float ratio = cameraLength / height;
		return ratio - 1;		//by right this should be (1 - ratio). But offset needs to be multiplied by -1, so this is ending Y value.
	}

	float CalculateChangingOffsetValue(){
		float cameraLength = mainScript.limitY - mainScript.originY;
		float ratio = cameraLength / height;
		return ratio - 1 + ratio*2/3;		
	}

    public void WinHack()
    {
		gameCanvas.transform.GetChild(3).GetChild(0).gameObject.GetComponent<Text>().text =
				"Oh no!! The crane has too much momentum. Its going to fly off!!!";
		gameCanvas.transform.GetChild(3).gameObject.SetActive(true);
		startingEndingAnimation = true;
    }

	public void GameWon(){
		if (LevelManager.gameCount >= 2) {
			if (!windSoundPlayed) {
				AudioManager.instance.PlaySound ("Flying");
				windSoundPlayed = true;
			}
			gameCanvas.transform.GetChild (3).GetChild (0).gameObject.GetComponent<Text> ().text = 
				"Oh no!! The crane has too much momentum. Its going to fly off!!!";
			gameCanvas.transform.GetChild (3).gameObject.SetActive (true);
			startingEndingAnimation = true;
		} else {
			gameCanvas.transform.GetChild (3).GetChild (0).gameObject.GetComponent<Text> ().text = 
				"Container Successfully Loaded! Only " + (levelManagerScript.maxGames - LevelManager.gameCount - 1) + " more to go!";
			gameCanvas.transform.GetChild (3).gameObject.SetActive (true);
			if (Input.touchCount != 0) {
				AudioManager.instance.PlayCommonSound ("click");
				levelManager.SendMessage ("GameEnded");
			}
		}
	}

	IEnumerator Wait(){
		yield return new WaitForSeconds (2f);
		canStartMovingUp = true;
		speed = 0.001f;
		maxSpeed *= 3;
	}

	void EndingAnimation(){
		hook.SendMessage ("MoveHookUp");
	}
}