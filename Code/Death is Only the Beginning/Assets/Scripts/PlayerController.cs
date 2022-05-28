using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool isUber = false;

    PlayerUberMode playerUberMode;

    //singleton instance
    public static PlayerController instance;


    void Awake()
    {
        if (instance != null && instance != this) {
            gameObject.SetActive(false);
        } else {
            instance = this;
        }
    }

    private void Start()
    {
        playerUberMode = GetComponent<PlayerUberMode>();
    }

    public void Die()
    {
        if (! isUber) {
            playerUberMode.ActivateUberMode();
            isUber = true;
        } else {
            GameManager.instance.GameOver();
            Destroy(gameObject);
        }
    }
}
