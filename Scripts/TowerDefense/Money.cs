using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Money : MonoBehaviour {

    public float money;
    public Text text;

    public void Update() {
        text.text = "Money: " + money;
    }

    public void AddMoney(int moneyToAdd) {
        money += moneyToAdd;
    }

    public void RemoveMoney(int moneyToLose) {
        money -= moneyToLose;
    }
}
