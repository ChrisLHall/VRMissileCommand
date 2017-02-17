using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : MonoBehaviour {
    static List<EnemyMissile> missileInstances = new List<EnemyMissile>();
    static int missilesBlownUp = 0;
    public float speed;
    public GameObject explosionPrefab;

    public void Shoot(City end) {
        missileInstances.Add(this);
        transform.LookAt(end.transform.position);
        float time = (end.transform.position - transform.position).magnitude / speed;
        LeanTween.move(gameObject, end.transform.position, time);
        Delayed.Action(gameObject, () => {
            if (null != end) {
                end.BlowUp();
            }
            Instantiate<GameObject>(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject, 0.01f);
        }, time);
    }

    public static void DestroyMissilesWithin(Vector3 point, float radius) {
        float sqrRad = radius * radius;
        for (int i = 0; i < missileInstances.Count; i++) {
            EnemyMissile em = missileInstances[i];
            if (null == em) {
                missileInstances.RemoveAt(i);
                i--;
                continue;
            }
            if ((em.transform.position - point).sqrMagnitude < sqrRad) {
                missilesBlownUp++;
                Instantiate<GameObject>(em.explosionPrefab, em.transform.position, Quaternion.identity);
                Destroy(em.gameObject);
                missileInstances.RemoveAt(i);
                i--;
                continue;
            }
        }
    }

    public static void ResetState() {
        Debug.Log("FINAL SCORE: " + missilesBlownUp);
        missilesBlownUp++;
        missileInstances.Clear();
    }
}
