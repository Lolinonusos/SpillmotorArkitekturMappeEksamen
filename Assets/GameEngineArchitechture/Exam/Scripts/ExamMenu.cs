using UnityEngine;
using UnityEngine.SceneManagement;

public class ExamMenu : MonoBehaviour {
	public void StartFlat() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void StartTerrain() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
	}

	public void CloseApp() {
		Application.Quit();
	}

}