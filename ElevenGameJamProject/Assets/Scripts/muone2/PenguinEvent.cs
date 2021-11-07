using eleven.game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinEvent : MonoBehaviour
{
    public Animator PenguinAnimator;
    public bool isDead = false;
    bool isComplete = false;

    private void Start()
    {
        PenguinAnimator = GetComponent<Animator>();
    }

    private async void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead || isComplete)
            return;

        if (collision.gameObject.CompareTag("Item"))
        {
            Item item = collision.GetComponent<Item>();

            int score = item?.Score ?? 0;
            InGameManager.Instance.AddScore(score);

            collision.gameObject.SetActive(false);

            Muone2SoundManager.instance.soundGetItemOffOn();
        }

        else if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("DeadZone"))
        {
            PenguinAnimator.SetBool("isDie", true );  //������ �޴������� ������ ���ϴ� �Լ��� �޾ƿ;� ��. ������ �ٸ� �͵鵵 �������.
            Muone2SoundManager.instance.soundDieOffOn();
            isDead = true;

            await InGameManager.Instance.OpenGameFailView();
        }

        else if (collision.gameObject.CompareTag("Home"))
        {
            PenguinAnimator.SetBool("isGoal", true);   //������ �޴������� ������ ���ϴ� �Լ��� �޾ƿ;� ��. ������ �ٸ� �͵鵵 �������.
            Debug.Log("Good Ending on");
            Muone2SoundManager.instance.soundGoalHomeOffOn();
            isComplete = true;

            await InGameManager.Instance.OpenGameClearView();
        }

        
    }

}
