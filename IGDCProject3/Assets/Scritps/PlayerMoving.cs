using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    public enum State { Stand, Run, Jump, Hit }
    public float JumpPower;
    public bool isGround;
    int JumpScore = 0;
    public int Life = 5;

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
        // 1. 점프 (점프파워)
        if (Input.GetButtonDown("Jump")) // 기본점프
        {
            if (JumpScore < 1)
            {
                rigid.velocity = Vector2.up * JumpPower;
                ++JumpScore;
            }
        }
    }

    // 2. 착지 (물리 충돌 이벤트)
    void OnCollisionStay2D(Collision2D collision) // 충돌 발생시 호출되는 함수
    {
        if (!isGround)
        {
            ChangeAnim(State.Run);
        }

        JumpScore = 0;
        isGround = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        ChangeAnim(State.Jump);
        isGround = false;
    }

    // 3. 장애물 터치 (트리거 충돌 이벤트)
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "obstacle")
        {
            --Life;
            if (Life == 0)
            {
                ChangeAnim(State.Hit);
                rigid.simulated = false;
            }
        }
    }

    // 4. 애니메이션
    void ChangeAnim(State state)
    {
        anim.SetInteger("State", (int)state); 
    }
}
