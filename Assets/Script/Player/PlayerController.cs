using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public LayerMask solidObjetcLayer;
    public LayerMask longGrass;

    public event Action OnEncounters;

    private bool isMovining;
    private Vector2 input;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    public void HandleUpdate()
    {
        if (!isMovining)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            //Saca el movimiento diagonal
            if (input.x != 0) input.y = 0;
            //
            

            if (input != Vector2.zero)
            {
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);


                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                if (IsWalkable(targetPos))
                {
                    StartCoroutine(Move(targetPos));
                }
            }
        }

        animator.SetBool("isMoving", isMovining);
    }


    IEnumerator Move(Vector3 targetPos)
    {
        isMovining = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        isMovining = false;
        CheckForEncounters();

    }

    private bool IsWalkable(Vector3 targetPos)//Checkea si el lugar a donde queremos dirigir nuestro player es o no es transitable
    {
       if(Physics2D.OverlapCircle(targetPos,0.2f,solidObjetcLayer) != null)
       {
            return false;

       }
        return true;
    }

    private void CheckForEncounters() //Crea encuentros random cuando caminamos por el pasto
    {
        if (Physics2D.OverlapCircle(transform.position, 0.2f, longGrass) != null)
        {
            if (UnityEngine.Random.Range(1, 101) <= 10)
            {
                animator.SetBool("isMoving", false);
                OnEncounters();
            }

        }
    }

}
