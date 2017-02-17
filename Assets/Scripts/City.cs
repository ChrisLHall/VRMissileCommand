using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour {
    public static List<City> cities = new List<City>();
    public static City ClosestArmedCity(Vector3 pos) {
        float minSqrDist = float.PositiveInfinity;
        City result = null;
        if (cities.Count == 0) {
            return null;
        }
        for (int i = 0; i < cities.Count; i++) {
            City c = cities[i];
            float sqrDist = (pos - c.transform.position).sqrMagnitude;
            if (sqrDist < minSqrDist && c.armed) {
                minSqrDist = sqrDist;
                result = c;
            }
        }
        return result;
    }
    public static City RandomCity() {
        if (cities.Count == 0) {
            return null;
        }
        return cities[Random.Range(0, cities.Count)];
    }

    public Transform missileShootPos;
    public GameObject playerMissilePrefab;
    public GameObject giantExplosionPrefab;
    public Material armedMat;
    public Material unarmedMat;
    public float reloadTime;
    bool armed = true;

    private void Awake() {
        cities.Add(this);
    }
    
    public void Shoot(Vector3 pos) {
        PlayerMissile pm = Instantiate(playerMissilePrefab, missileShootPos.position, Quaternion.identity).GetComponent<PlayerMissile>();
        pm.Shoot(pos);
        SetArmed(false);
        Delayed.Action(gameObject, () => {
            SetArmed(true);
        }, reloadTime);
    }

    void SetArmed(bool which) {
        armed = which;
        foreach (Renderer r in GetComponentsInChildren<Renderer>()) {
            r.material = which ? armedMat : unarmedMat;
        }
    }

    public void BlowUp() {
        Instantiate<GameObject>(giantExplosionPrefab, transform.position, Quaternion.identity);
        cities.Remove(this);
        Destroy(gameObject);
    }

    public static void ResetState() {
        cities.Clear();
    }
}
