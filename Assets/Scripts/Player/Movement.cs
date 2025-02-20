using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    public float speed;
    public bool isGrounded;
    public float dashCooldown;
    public float dashRecharge = 2;
    public float jumpForce;
    private bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        isGrounded = CheckFloor();
        Jump();
        Dash();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Moving();
    }
    void Moving()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        if (canMove)
        {
            Vector3 move = transform.right * x + transform.forward * z;
            rb.velocity = new Vector3(move.x * speed, rb.velocity.y, move.z * speed);
        }
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldown <= 0 && !gameObject.GetComponent<Player>().isBlocking)
        {
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                speed *= 5;
                dashCooldown = dashRecharge;
                StartCoroutine(DeactivateDash());
            }
        }
        else if (dashCooldown > 0)
        {
            dashCooldown -= Time.deltaTime;
        }
    }
    IEnumerator DeactivateDash()
    {
        yield return new WaitForSeconds(0.1f);
        speed /= 5;
    }
    bool CheckFloor()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.1f))
        {
            return true;
        }
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Food" || other.gameObject.tag == "Wall")
        {
            canMove = false;
            rb.velocity = Vector3.zero;
            Vector3 direction = (new Vector3(transform.position.x, 0, transform.position.z) - new Vector3(other.transform.position.x, 0, other.transform.position.z)).normalized;
            rb.AddForce(direction * 5, ForceMode.Impulse);
            StartCoroutine(EnableMovement());
        }
    }
    IEnumerator EnableMovement()
    {
        yield return new WaitForSeconds(0.5f);
        canMove = true;
    }
}