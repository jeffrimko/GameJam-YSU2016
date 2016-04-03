using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject player;
    private Vector3 pOffset;
    private float pMinY = -37.0f;
    private float pMaxX = 5.0f;
    private float pMinX = -5.0f;
    private float pMaxZ = 30.0f;

    // The speed held for camera zoom.
    private float pSpeedHold = 0.0f;
    // The cool down rate.
    private float pHoldRate = 0.003f;
    // Maintains the cool down value.
    private float pHoldCool = 0.0f;
    // Max percentage of original offset used for zoom.
    private float pZoomMax = 0.55f;
    // Speed cap for max zoom.
    private float pSpeedMax = 25.0f;

    // Use this for initialization
    void Start () {
        pOffset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate () {
        // Handle the zoom hold speed and associated cool down.
        float speed = player.GetComponent<Rigidbody>().velocity.magnitude;
        if(speed > pSpeedHold) {
            pHoldCool = 0.0f;
            pSpeedHold = speed;
            if(pSpeedHold > pSpeedMax) {
                pSpeedHold = pSpeedMax;
            }
        } else {
            pHoldCool += pHoldRate;
            pSpeedHold -= pHoldCool;
        }
        if(pSpeedHold < 0.0f) {
            pSpeedHold = 0.0f;
        }

        // Calculate zoom based on held speed.
        float speed_pct_max = (pSpeedHold / pSpeedMax);
        float zoom_sub =  speed_pct_max * (1.0f - pZoomMax);
        float zoom = 1.0f - zoom_sub;

        // Set camera position and check limits.
        transform.position = player.transform.position + (pOffset * zoom);
        if(transform.position.z < pMinY) {
            transform.position = new Vector3(transform.position.x, transform.position.y, pMinY);
        }
        if(transform.position.x < pMinX) {
            transform.position = new Vector3(pMinX, transform.position.y, transform.position.z);
        }
        if(transform.position.x > pMaxX) {
            transform.position = new Vector3(pMaxX, transform.position.y, transform.position.z);
        }
        if(transform.position.z > pMaxZ) {
            transform.position = new Vector3(transform.position.x, transform.position.y, pMaxZ);
        }
    }
}
