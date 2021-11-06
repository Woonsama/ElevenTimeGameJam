using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muone2SoundManager : MonoBehaviour
{
    public static Muone2SoundManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject soundBGM;
    public GameObject soundDie;
    public GameObject soundGetItem;
    public GameObject soundJump;
    public GameObject soundGoalHome;
    public GameObject soundMouseClick;

    public void soundBGMOffOn()
    {
        soundBGM.SetActive(false);
        soundBGM.SetActive(true);
    }
    public void soundDieOffOn()
    {
        soundDie.SetActive(false);
        soundDie.SetActive(true);
    }
    public void soundGetItemOffOn()
    {
        soundGetItem.SetActive(false);
        soundGetItem.SetActive(true);
    }
    public void soundJumpOffOn()
    {
        soundJump.SetActive(false);
        soundJump.SetActive(true);
    }
    public void soundGoalHomeOffOn()
    {
        soundGoalHome.SetActive(false);
        soundGoalHome.SetActive(true);
    }
    public void soundMouseClickOffOn()
    {
        soundMouseClick.SetActive(false);
        soundMouseClick.SetActive(true);
    }


}
