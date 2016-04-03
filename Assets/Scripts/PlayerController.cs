using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float moveMult = 1.25f;
    private ScoreController pScoreCtlr;

    private Rigidbody pRb;

    // Use this for initialization
    void Start () {
        pRb = GetComponent<Rigidbody>();
        pScoreCtlr = (ScoreController)GameObject.Find("Score Keeper").GetComponent(typeof(ScoreController));
    }

    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "Obstacle") {
            pScoreCtlr.Restart();
        }
    }

    // FixedUpdate is called at a fixed interval.
    void FixedUpdate() {
        float move_h = Input.GetAxis("Horizontal") * moveMult;
        float move_v = Input.GetAxis("Vertical") * moveMult;
        Vector3 movement = new Vector3(move_h, 0.0f, move_v);
        pRb.AddForce(movement, ForceMode.Impulse);
    }
}
