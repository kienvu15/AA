using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D Rigidbody2D;
    Animator anim;

    [SerializeField] public float moveSpeed = 2f;
    private float OriginmoveSpeed;
    public Transform Target1;
    public Transform Target2;
    private Transform currentTarget;
    private bool isFacingRight = true;
    

    
    

    
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        OriginmoveSpeed = moveSpeed;
        currentTarget = Target1;

    }

    // Update is called once per frame
    void Update()
    {
        

        
            EnemyMove();

        Flip2();


    }

    public void EnemyMove()
    {

        Rigidbody2D.position = Vector2.MoveTowards(Rigidbody2D.position, currentTarget.position, moveSpeed * Time.deltaTime);
        if (Vector2.Distance(Rigidbody2D.position, currentTarget.position) < 0.1f)
        {
            if (currentTarget == Target1)
            {
                currentTarget = Target2;

            }
            else if (currentTarget == Target2)
            {
                currentTarget = Target1;

            }
        }
    }

    public void Flip2()
    {
        
        if ((currentTarget.position.x < transform.position.x && isFacingRight) || (currentTarget.position.x > transform.position.x && !isFacingRight))
        {
            Flip();
        }
        
    }
    public void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 Sacle = transform.localScale;
        Sacle.x *= -1;
        transform.localScale = Sacle;

    }



}
