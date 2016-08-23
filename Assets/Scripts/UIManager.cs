using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class UIManager : MonoBehaviour {
	
	GameObject[] pauseObjects;
	AudioMixer mixer;
	GameObject pauseObject;
	bool paused;
	float SliderMusic;
	public float musicVolume {get; set;}
	public bool IsPaused {
		get { return paused; }
		set {
			paused = value;
			foreach (var i in pauseObjects) {
				i.SetActive (paused);
			}

		}
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
			IsPaused = !IsPaused;
		if (IsPaused == true)
			Time.timeScale = 0;
		else
			Time.timeScale = 1;
	}
	void Start()
	{
		pauseObjects = GameObject.FindGameObjectsWithTag ("ShowOnPause");
		Time.timeScale=1;
		IsPaused = false;

	}
	public void LevelSelect()
	{
		SceneManager.LoadScene("Level Select");
	}
	public void Resume()
	{
		IsPaused=false;
	}
	public void Restart()
	{
		if (SceneManager.GetActiveScene () != null)
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		else
			Debug.Log ("No Scene set");
	}
	public void MainMenu()
	{
		SceneManager.LoadScene("Main Menu");
	}
	public void Tobias()
	{
		SceneManager.LoadScene("Tobias");
	}
	public void Tutorial()
	{
		SceneManager.LoadScene("Tutorial3");
	}
	public void Speedrun()
	{
		SceneManager.LoadScene("RunnerLevel");
	}
	public void Fabrice()
	{
		SceneManager.LoadScene("Fabrice");
	}
	public void Quit()
	{
		Application.Quit();
	}

}
