using UnityEngine;
using System.Collections;

public class ObstacleController : MonoBehaviour {

    public bool scoreable = false;
    private ScoreController pScoreCtlr;

    // Use this for initialization
    void Start () {
        pScoreCtlr = (ScoreController)GameObject.Find("Score Keeper").GetComponent(typeof(ScoreController));
    }

    // Update is called once per frame
    void Update () {
    }

    void OnCollisionEnter(Collision collision) {
        if("Unspawn" == collision.gameObject.tag) {
            Destroy(this.gameObject);
            if(scoreable) {
                pScoreCtlr.Increment();
            }
        }
    }
}
