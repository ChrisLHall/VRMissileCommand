using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using UnityEngine.SceneManagement;

public class PlayerMissileCtl : MonoBehaviour {
    public VRTK_ControllerEvents ctrl1;
    public VRTK_ControllerEvents ctrl2;

    void Start() {
        ctrl1.TriggerClicked += Ctrl1Trigger;
        ctrl2.TriggerClicked += Ctrl2Trigger;
        ctrl1.TouchpadReleased += (_, __) => ResetEntireGame();
        ctrl2.TouchpadReleased += (_, __) => ResetEntireGame();
    }

    void Ctrl1Trigger(object sender, ControllerInteractionEventArgs e) {
        Debug.Log(1);
        TriggerClicked(0);
    }

    void Ctrl2Trigger(object sender, ControllerInteractionEventArgs e) {
        Debug.Log(2);
        TriggerClicked(1);
    }

    void TriggerClicked(int which) {
        Vector3 pos = which == 1 ? ctrl2.transform.position : ctrl1.transform.position;
        City closest = City.ClosestArmedCity(pos);
        if (null == closest) {
            return;
        }
        closest.Shoot(pos);
    }

    void ResetEntireGame() {
        EnemyMissile.ResetState();
        City.ResetState();
        SceneManager.LoadScene(0);
    }
}
