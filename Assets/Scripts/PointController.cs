using UnityEngine;
using System.Collections;

public class PointController : MonoBehaviour {

    public float lifeTime = 5.0f;
    private float blinkFor = 1.0f;
    private float pSpawnTime;
    private float pBlinkTime;
    private ScoreController pScoreCtlr;
    private FieldController pFieldCtlr;
    private Renderer pRend;

    // Use this for initialization
    void Start () {
        pRend = GetComponent<Renderer>();
        pSpawnTime = Time.time;
        pBlinkTime = pSpawnTime + lifeTime - blinkFor;
        pScoreCtlr = (ScoreController)GameObject.Find("Score Keeper").GetComponent(typeof(ScoreController));
        pFieldCtlr = (FieldController)GameObject.Find("Field").GetComponent(typeof(FieldController));
    }

    // Update is called once per frame
    void Update () {
        if(Time.time > pBlinkTime) {
            pRend.enabled ^= true;
            pBlinkTime = Time.time + 0.1f;
        }
        if(Time.time > pSpawnTime + lifeTime) {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other) {
        if("Player" == other.gameObject.tag) {
            Destroy(this.gameObject);
            pScoreCtlr.Increment();
            pFieldCtlr.CreatePoint();
        }
    }
}
