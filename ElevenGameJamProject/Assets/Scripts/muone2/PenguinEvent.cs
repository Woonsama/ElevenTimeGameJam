using eleven.game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinEvent : MonoBehaviour
{
    public int score;
    public Animator PenguinAnimator;

    private void Start()
    {
        PenguinAnimator = GetComponent<Animator>();
        InGameManager.Instance.uiHeaderView.SetScore(0);
    }
    void OnEnable()
    {
        Muone2SoundManager.instance.soundBGMOffOn();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            Item item = collision.GetComponent<Item>();

            score = score + item?.Score ?? 0;
            InGameManager.Instance.uiHeaderView.SetScore(score);

            collision.gameObject.SetActive(false);

            Muone2SoundManager.instance.soundGetItemOffOn();
        }

        if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("DeadZone"))
        {
            PenguinAnimator.SetBool("isDie", true );  //실제론 메니저에서 죽음을 온하는 함수를 받아와야 함. 게임의 다른 것들도 멈춰야함.
            Muone2SoundManager.instance.soundDieOffOn();
        }

        if (collision.gameObject.CompareTag("Home"))
        {
            PenguinAnimator.SetBool("isGoal", true);   //실제론 메니저에서 엔딩을 온하는 함수를 받아와야 함. 게임의 다른 것들도 멈춰야함.
            Debug.Log("Good Ending on");
            Debug.Log("your score is" + score);
            Muone2SoundManager.instance.soundGoalHomeOffOn();
        }
    }

}
