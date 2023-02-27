using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool IsPlayerDead { get => isPlayerDead; } 

    [SerializeField] private int jumpForce = 10;
    [SerializeField] private float gravityModifier;
    [SerializeField] private ParticleSystem explosionParticle;
    [SerializeField] private ParticleSystem dirtParticle;
    [SerializeField] private AudioClip jumpSound; 
    [SerializeField] private AudioClip crashSound;

    private bool isPlayerDead = false;
    private bool isOnTheGround = true;
    private bool isOnTheSecondJump = false;
    private AudioSource playerAudio;
    private Rigidbody playerRb;

    private Animator playerAnim;
    private Vector3 preStartPos;
    private Vector3 startPos = new Vector3(0, 0, 0);

    
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        playerAnim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;

        preStartPos = transform.position;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && (isOnTheGround || !isOnTheSecondJump) && !isPlayerDead)
        {
            Jump();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isOnTheGround = true;
            isOnTheSecondJump = false;
            dirtParticle.Play();
        }
        if(collision.gameObject.CompareTag("Obstacle"))
        {
            Die();
        } 
    }

    public void MoveToStartPosition(float interpolationRatio)
    {
        transform.position = Vector3.Lerp(preStartPos, startPos, interpolationRatio);
    }

    public void SetAnimationSpeed(float speedMultiplier)
    {
        playerAnim.SetFloat("Speed_f", speedMultiplier);
    }

    void Jump()
    {
        if(!isOnTheGround)
        {
            isOnTheSecondJump = true;
        }
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isOnTheGround = false;
        dirtParticle.Stop();
        playerAnim.SetTrigger("Jump_trig");
        playerAudio.PlayOneShot(jumpSound, 1.0f);
    }

    void Die()
    {
        isPlayerDead = true;
        Debug.Log("Game over!");
        playerAnim.SetBool("Death_b", true);
        playerAnim.SetInteger("DeathType_int", 1);
        dirtParticle.Stop();
        explosionParticle.Play();
        playerAudio.PlayOneShot(crashSound, 1.0f);
    }
}