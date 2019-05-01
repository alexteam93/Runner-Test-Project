using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCharacterController : MonoBehaviour
{
    private Animator characterAnimator;
    // public bool isRunning = true;
    public float jumpForce = 50f, changeLineSpeed = 5f;
    public float firstLinePos, lineDistance;
    private int lineNumber = 1, linesCount = 3;
    public GameManager GM;

    private Rigidbody rb; 

    void Start()
    {
        characterAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        SwipeController.SwipeEvent += CheckInput;
    }

    void Update()
    {
        Debug.DrawRay(transform.position, Vector3.down * 0.05f, Color.red);
        Vector3 newPos = transform.position;
        newPos.x = Mathf.Lerp(newPos.x, firstLinePos + (lineNumber * lineDistance), Time.deltaTime * changeLineSpeed);
        transform.position = newPos;
    }

    bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 0.05f);
    }
    void CheckInput(SwipeController.SwipeType type)
    {
        if(type == SwipeController.SwipeType.UP && isGrounded())        
        {
            
            Jump();
        }

        if  (type == SwipeController.SwipeType.RIGHT)
            ChangeLine(1);
        else if(type == SwipeController.SwipeType.LEFT)
            ChangeLine(-1);
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce);
        characterAnimator.SetTrigger("jumping");
       
    }

    void ChangeLine(int direction)
    {
        lineNumber += direction;
        lineNumber = Mathf.Clamp(lineNumber, 0, linesCount - 1);
        
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "coin")
        {
            GM.AddCoin(1);
            other.gameObject.SetActive(false);
        } 
        else if(other.tag == "boost")
        {
            GM.Boost(1.5f);
            other.gameObject.SetActive(false);
        }

        if(other.tag == "death")
        {
            characterAnimator.SetTrigger("die");
            GM.GameOver();
        }
    }
}
