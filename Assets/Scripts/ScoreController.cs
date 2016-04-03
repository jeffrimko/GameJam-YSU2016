using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreController : MonoBehaviour {

    public Text scoreText;
    public GameObject finalScore;
    private uint pScore;
    private bool pRestart = false;
    private bool pShown = false;

    // Use this for initialization
    void Start () {
        pRestart = false;
        pShown = false;
        pScore = 0;
        scoreText.text = "Score: " + pScore;
        finalScore.SetActive(false);
    }

    public void Increment () {
        pScore++;
        scoreText.text = "Score: " + pScore;
    }

    public void Restart() {
        pRestart = true;
    }

    // Update is called once per frame
    void Update () {
        if(Input.GetKey("escape")) {
            Application.Quit();
        }
        if(!pRestart) {
            return;
        }
        if(!pShown) {
            finalScore.SetActive(true);
            Text final = finalScore.GetComponentInChildren<Text>();
            final.text = "Final Score: " + pScore;
            final.text += "\nPress ENTER to retry.";
            final.text += "\nPress ESC to exit.";
            pShown = true;
        }
        if(Input.GetKeyDown(KeyCode.Return)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
