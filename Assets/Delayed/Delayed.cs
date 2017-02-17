using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delayed : MonoBehaviour {
    static Delayed _inst;
    public static Delayed inst {
        get {
            if (null == _inst) {
                _inst = new GameObject().AddComponent<Delayed>();
            }
            return _inst;
        }
    }

    public static Coroutine Action (GameObject host, System.Action action, float delay) {
        return inst.StartCoroutine(ActionCoroutine(host, action, delay));
    }

    static IEnumerator ActionCoroutine(GameObject host, System.Action action, float delay) {
        if (delay > 0) {
            yield return new WaitForSeconds(delay);
        } else {
            yield return null;
        }
        while (true) {
            if (null == host) {
                yield break;
            }
            if (host.activeInHierarchy) {
                break;
            }
            yield return null;
        }
        action();
    }
}
