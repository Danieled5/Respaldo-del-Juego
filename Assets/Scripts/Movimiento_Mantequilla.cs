using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movimiento_Mantequilla : MonoBehaviour
{
    Animator animator;

    //Run
    public int runSpeed = 1;
    float horizontal;
    float vertical;
    bool facingRight;

    //Crouch
    bool isCrouching;
    bool isSliding;

    //Slide speed
    public float slideSpeed = 2f;

    //Jump
    public float jumpForce = 300;
    Rigidbody2D rigidbody1;
    float axisY;
    public bool isJumping;

    //Limitador
    private bool limitadorActivo = true; // Bandera para controlar el limitador

    //Scaling
    public float minY = -4.40f; // Límite mínimo en Y
    public float maxY = 0f;     // Límite máximo en Y
    public Vector3 minScale = new Vector3(1.5f, 1.5f, 1.5f); // Escala mínima
    public Vector3 maxScale = new Vector3(1f, 1f, 1f); // Escala máxima


    void Awake()
    {
        animator = GetComponent<Animator>();
        rigidbody1 = GetComponent<Rigidbody2D>();
        rigidbody1.Sleep();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        animator.SetFloat("Speed", Mathf.Abs(horizontal != 0 ? horizontal : vertical));

        if (Input.GetButton("Crouch"))
        {
            if (horizontal != 0) // Si el personaje está moviéndose horizontalmente
            {
                isSliding = true;
                isCrouching = false;
                animator.SetBool("IsSliding", true);
                animator.SetBool("IsCrouching", false);
            }
            else // Si el personaje está quieto
            {
                isSliding = false;
                isCrouching = true;
                animator.SetBool("IsSliding", false);
                animator.SetBool("IsCrouching", true);
            }
        }
        else
        {
            isSliding = false;
            isCrouching = false;
            animator.SetBool("IsSliding", false);
            animator.SetBool("IsCrouching", false);
        }

        if (transform.position.y < axisY)
            OnLanding();

        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            axisY = transform.position.y;
            isJumping = true;
            limitadorActivo = false; //Desactivar el limitador durante el salto
            rigidbody1.gravityScale = 1.5f;
            rigidbody1.WakeUp();
            rigidbody1.AddForce(new Vector2(0, jumpForce));
            animator.SetBool("IsJumping", isJumping);
            Debug.Log("salto");
        }

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("isAtacking", true);
        }
    }

    public void endAttack()
    {
        animator.SetBool("isAtacking", false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "leche")
        {
            isJumping = false;
            limitadorActivo = true; //Reactivar el limitador al aterrizar
            rigidbody1.gravityScale = 0f;
            rigidbody1.Sleep();
            animator.SetBool("IsJumping", false);
            Debug.Log("aterrizo");
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "leche")
        {
            rigidbody1.gravityScale = 1.5f;
        }
    }

    void FixedUpdate()
    {
        Vector3 newPosition = transform.position;

        if ((horizontal != 0 || vertical != 0) && !isCrouching && !isSliding)
        {
            Vector3 movement = new Vector3(horizontal * runSpeed, vertical * runSpeed, 0.0f);
            newPosition += movement * Time.deltaTime;
        }

        if (isSliding)
        {
            Vector3 slideMovement = new Vector3(horizontal * slideSpeed, 0.0f, 0.0f);
            newPosition += slideMovement * Time.deltaTime;
        }

        if (limitadorActivo)
        {
            newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY); // Establece los límites minY y maxY
        }

        transform.position = newPosition;
        Flip(horizontal);
        AdjustScaleBasedOnY(); //Ajustar la scala basada en la posicion Y
    }

    private void Flip(float horizontal)
    {
        if (horizontal < 0 && !facingRight || horizontal > 0 && facingRight)
        {
            facingRight = !facingRight;

            //Solo invertir la escala en el eje X
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;

        }
    }

    void OnLanding()
    {
        isJumping = false;
        limitadorActivo = true; //Reactivar el limitador al aterrizar
        rigidbody1.gravityScale = 0f;
        rigidbody1.Sleep();
        axisY = transform.position.y;
        animator.SetBool("IsJumping", false);
        Debug.Log("aterrizo");

    }

    void AdjustScaleBasedOnY()
    {
        //Calcular la proporcion de la posicion Y entre minY y maxY
        float t = Mathf.InverseLerp(minY, maxY, transform.position.y);

        // Interpolar entre minScale y maxScale basado en t
        Vector3 targetScale = Vector3.Lerp(minScale, maxScale, t);

        // Mantener el signo de la escala en X para el volteo horizontal
        targetScale.x *= Mathf.Sign(transform.localScale.x);

        transform.localScale = targetScale;
    }
}