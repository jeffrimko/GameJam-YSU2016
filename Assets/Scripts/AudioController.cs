using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour {

    static private bool pIsPlaying = false;

    void Awake() {
        DontDestroyOnLoad(this.gameObject);
        if(!pIsPlaying) {
            GetComponent<AudioSource>().Play();
            pIsPlaying = true;
        }
    }


    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }
}
