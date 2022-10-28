using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public string isPlayer1;
    public float speed;
    public Rigidbody2D rb;
    public Vector3 startPosition;
    private float movement;

    private bool firstUpInput = false;
    private bool firstDownInput = false;
    [SerializeField] private string DashUpInput;
    [SerializeField] private string DashDownInput;
    private float timer = 0;
    private float timer2 = 0;
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 7f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 0.35f;

    [SerializeField] private TrailRenderer tr;

    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {       


        if (isDashing)
        {
            return;
        } 
        
        movement = Input.GetAxisRaw(isPlayer1);

        rb.velocity = new Vector2(rb.velocity.x, movement * speed);

        if (Input.GetKeyDown(DashDownInput) && canDash && firstDownInput)
        {
            StartCoroutine(Dash(-1)); firstDownInput = false;
        }
        else if(Input.GetKeyDown(DashDownInput))
        {
            firstDownInput = true;
        }
        ResetDownInput();

        if (Input.GetKeyDown(DashUpInput) && canDash && firstUpInput)
        {
            StartCoroutine(Dash(1)); firstUpInput = false;
        }
        else if (Input.GetKeyDown(DashUpInput))
        {
            firstUpInput = true;
        }
        ResetUpInput();
    }

    public void Reset()
    {
        rb.velocity = Vector2.zero;
        transform.position = startPosition;
    }

    private IEnumerator Dash(int dashdirection)
    {
        firstUpInput = false;
        timer = 0;
        timer2 = 0;
        canDash = false;
        isDashing = true;
        rb.velocity = new Vector2(0f,transform.localScale.y * dashingPower * dashdirection);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
    private void ResetUpInput()
    {
        if (!firstUpInput) return;
        timer += Time.deltaTime;
        if(timer > 0.2f)
        {
            firstUpInput = false;
            timer = 0;
        }
    }
    private void ResetDownInput()
    {
        if (!firstDownInput) return;
        timer2 += Time.deltaTime;
        if (timer2 > 0.2f)
        {
            firstDownInput = false;
            timer2 = 0;
        }
    }
}
