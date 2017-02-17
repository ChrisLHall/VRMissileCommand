using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawner : MonoBehaviour {
    public GameObject missilePrefab;
    public float preDelay;
    public float appxInterval;
    float shootTimer = 0f;
	
    // Update is called once per frame
    void Update () {
        if (preDelay > 0f) {
            preDelay -= Time.deltaTime;
            return;
        }
        shootTimer -= Time.deltaTime;
        if (shootTimer < 0f) {
            City c = City.RandomCity();
            if (null != c) {
                EnemyMissile newMissile = Instantiate(missilePrefab).GetComponent<EnemyMissile>();
                newMissile.transform.position = transform.position;
                newMissile.transform.position += new Vector3(-0.5f + Random.value, 0f, -0.5f + Random.value) * 3f;
                newMissile.Shoot(c);
            }
            shootTimer = appxInterval * Random.Range(0.9f, 1.1f);
            appxInterval *= 0.95f;
        }
    }
}
