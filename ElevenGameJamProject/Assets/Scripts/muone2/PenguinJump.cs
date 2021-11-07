using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinJump : MonoBehaviour
{
    public GameObject penguin;
    public int JumpCount;
    Rigidbody2D rigid;
    public float jumpPower;

    PenguinEvent penguinEvent;

    // Start is called before the first frame update
    void Start()
    {
        JumpCount = 0;
        rigid = GetComponent<Rigidbody2D>();
        penguinEvent = GetComponentInChildren<PenguinEvent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!penguinEvent.isDead)
        {
            if(JumpCount < 2)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    rigid.velocity = new Vector2(rigid.velocity.x, Vector2.up.y * jumpPower);
                    JumpCount++;
                    Debug.Log(JumpCount);
                    Muone2SoundManager.instance.soundJumpOffOn();
                }
                else if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    if (touch.phase == TouchPhase.Began)
                    {
                        rigid.velocity = new Vector2(rigid.velocity.x, Vector2.up.y * jumpPower);
                        JumpCount++;
                        Debug.Log(JumpCount);
                        Muone2SoundManager.instance.soundJumpOffOn();
                    }
                }
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            JumpCount = 0;
    }

}
