using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissile : MonoBehaviour {
    public float speed;
    public float destroyRadius;
    public GameObject explosionPrefab;

    public void Shoot(Vector3 end) {
        transform.LookAt(end);
        float time = (end - transform.position).magnitude / speed;
        LeanTween.move(gameObject, end, time);
        Delayed.Action(gameObject, () => {
            EnemyMissile.DestroyMissilesWithin(end, destroyRadius);
            Instantiate<GameObject>(explosionPrefab, end, Quaternion.identity);
            Destroy(gameObject, 0.01f);
        }, time);
    }
}
