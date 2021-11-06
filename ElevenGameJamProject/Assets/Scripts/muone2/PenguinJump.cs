using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinJump : MonoBehaviour
{
    public GameObject penguin;
    public int JumpCount;
    Rigidbody2D rigid;
    public float jumpPower;


    // Start is called before the first frame update
    void Start()
    {
        JumpCount = 0;
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && JumpCount < 2)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, Vector2.up.y * jumpPower);
            JumpCount++;
            Debug.Log(JumpCount);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            JumpCount = 0;
    }

}
