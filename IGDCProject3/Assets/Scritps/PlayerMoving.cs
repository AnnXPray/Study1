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
        // 1. ���� (�����Ŀ�)
        if (Input.GetButtonDown("Jump")) // �⺻����
        {
            if (JumpScore < 1)
            {
                rigid.velocity = Vector2.up * JumpPower;
                ++JumpScore;
            }
        }
    }

    // 2. ���� (���� �浹 �̺�Ʈ)
    void OnCollisionStay2D(Collision2D collision) // �浹 �߻��� ȣ��Ǵ� �Լ�
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

    // 3. ��ֹ� ��ġ (Ʈ���� �浹 �̺�Ʈ)
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

    // 4. �ִϸ��̼�
    void ChangeAnim(State state)
    {
        anim.SetInteger("State", (int)state); 
    }
}
