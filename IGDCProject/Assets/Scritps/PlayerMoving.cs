using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    public enum State { Stand, Run, Jump, Hit }
    public float startJumpPower;
    public float JumpPower;
    public bool isGround;

    Rigidbody2D rigid;
    Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (isGround)
            {
                rigid.AddForce(Vector2.up * startJumpPower, ForceMode2D.Impulse);
            }
        }
    }

    void OnCollisionStay2D(Collision2D collision) // 충돌 발생시 호출되는 함수
    {
        if (!isGround)
        {
            ChangeAnim(State.Run);
        }
        isGround = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        ChangeAnim(State.Jump);
        isGround = false;
    }

    void ChangeAnim(State state)
    {
        anim.SetInteger("State", (int)state); 
    }
}
