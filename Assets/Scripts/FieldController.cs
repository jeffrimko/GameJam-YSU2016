using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class FieldController : MonoBehaviour {

    private class OpenWallObs {
        public GameObject left;
        public GameObject right;
        public float speed;
        public uint id;

        private float pScaleY = 2.5f;
        private float pScaleZ = 1.0f;

        static private uint pId = 0;

        public OpenWallObs(GameObject base_obs, uint size, uint cent, float spd) {
            // TODO: Check that opening params are valid.
            // TODO: Make 50.0 (width of field) automatic.

            id = pId++;

            float osize = (float)size;
            float ocent = (float)cent;

            // Calculate the left obstacle values.
            float lsize = ocent - (osize / 2.0f);
            float lcent = lsize / 2.0f;
            lcent -= (50.0f / 2.0f);

            // Calculate the right obstacle values.
            float rsize = 50.0f - (lsize + osize);
            float rcent = ocent + (osize / 2.0f);
            rcent = ((50.0f - rcent) / 2.0f) + rcent;
            rcent -= (50.0f / 2.0f);

            // Create obstacle.
            Vector3 lpos = new Vector3(lcent, 1.25f, 75.0f);
            left = (GameObject)Instantiate(base_obs, lpos, Quaternion.identity);
            left.transform.localScale = new Vector3(lsize, pScaleY, pScaleZ);
            ObstacleController ctrl = left.GetComponent<ObstacleController>();
            // Only the left obstacle is scoreable, prevents double scoring during unspawn.
            ctrl.scoreable = true;
            Vector3 rpos = new Vector3(rcent, 1.25f, 75.0f);
            right = (GameObject)Instantiate(base_obs, rpos, Quaternion.identity);
            right.transform.localScale = new Vector3(rsize, pScaleY, pScaleZ);

            // Set obstacle in motion.
            speed = spd;
            Vector3 mov = new Vector3(0.0f, 0.0f, spd * -1);
            left.GetComponent<Rigidbody>().velocity = mov;
            right.GetComponent<Rigidbody>().velocity = mov;
        }
    }

    public GameObject baseObstacle;
    public GameObject basePoint;
    private float pTimeWall;
    private float pTimePoint;
    private uint pNumWall;

    OpenWallObs CreateWall(uint size, uint cent, uint spd) {
        OpenWallObs obs = new OpenWallObs(baseObstacle, size, cent, (float)spd);
        pNumWall++;
        return(obs);
    }

    public GameObject CreatePoint() {
        float x = Random.Range(-20.0f, 20.0f);
        float z = Random.Range(0.0f, 40.0f);
        Vector3 pos = new Vector3(x, 0.5f, z);
        GameObject point = (GameObject)Instantiate(basePoint, pos, Quaternion.identity);
        pTimePoint = Time.time + 5.0f;
        return(point);
    }

    // Use this for initialization
    void Start() {
        pNumWall = 0;
        pTimeWall = Time.time;
        pTimePoint = Time.time;
    }

    // Set how long in seconds before spawning a new wall obstacle.
    void SpawnInSecs(float sec) {
        pTimeWall = Time.time + sec;
    }

    // Handles the point pickup logic.
    void HandlePoints() {
        if(Time.time > pTimePoint) {
            CreatePoint();
        }
    }

    // Handles the wall obstacle logic.
    void HandleWalls() {
        if(Time.time > pTimeWall) {
            uint size = 0;
            uint center = 0;
            uint speed = 0;

            // Create new wall obstacle.
            if(pNumWall < 4) {
                // Slow easy wave.
                size = (uint)Random.Range(8, 10);
                center = (uint)Random.Range(15, 35);
                speed = 15;
                SpawnInSecs(Random.Range(2.75f, 3.0f));
            } else if(pNumWall == 4) {
                // Single closing in wall.
                size = (uint)Random.Range(8, 10);
                center = (uint)Random.Range(15, 35);
                speed = 25;
                SpawnInSecs(5.0f);
            } else if(pNumWall < 9) {
                // Quick single walls.
                size = (uint)Random.Range(8, 10);
                center = (uint)Random.Range(15, 35);
                speed = 50;
                SpawnInSecs(Random.Range(2.0f, 3.0f));
            } else if(pNumWall < 15) {
                // Moderate wave.
                size = (uint)Random.Range(5, 7);
                center = (uint)Random.Range(10, 40);
                speed = (uint)Random.Range(18, 20);
                SpawnInSecs(Random.Range(2.0f, 2.5f));
            } else if(pNumWall == 15) {
                // Single closing in wall.
                size = (uint)Random.Range(5, 7);
                center = (uint)Random.Range(10, 40);
                speed = 28;
                SpawnInSecs(5.0f);
            } else if(pNumWall < 20) {
                // Quick single walls.
                size = (uint)Random.Range(5, 7);
                center = (uint)Random.Range(10, 40);
                speed = 60;
                SpawnInSecs(Random.Range(2.0f, 3.0f));
            } else {
                // Difficult wave.
                size = (uint)Random.Range(3, 5);
                center = (uint)Random.Range(7, 43);
                speed = (uint)Random.Range(20, 25);
                SpawnInSecs(Random.Range(2.0f, 2.5f));
            }

            if(speed > 0) {
                CreateWall(size, center, speed);
            }
        }
    }

    // Update is called once per frame.
    void Update () {
        HandleWalls();
        HandlePoints();
    }
}
