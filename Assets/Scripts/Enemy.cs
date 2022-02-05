using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Abilities")]
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private int damage=0;

    [Header("Effects")]
    [SerializeField] private GameObject cactusBiteEffect;
    [SerializeField] private GameObject gnomeBiteEffect;
    [SerializeField] private GameObject starTrophyBiteEffect;
    [SerializeField] private GameObject stoneBiteEffect;

    [Header("Audio")]
    [SerializeField] private AudioClip walkAudioClip;
    [SerializeField] private AudioClip attackAudioClip;
    [SerializeField] private AudioClip jumpAudioClip;
    [SerializeField] private AudioSource audioSource;



    public EnemyType Type => enemyType;

    private float speed=0;
    private bool isAttacking = false;
    private int defenderLayer;
    private int loseLayer;
    private GameObject target = null;
    private Animator animator;
    private static readonly int Jump = Animator.StringToHash("jump");
    private static readonly int Attack = Animator.StringToHash("isAttacking");

    // Start is called before the first frame update
    void Start()
    {
        // set collision layers
        loseLayer = LayerMask.NameToLayer("LoseCollider");
        defenderLayer = LayerMask.NameToLayer("Defender");

        animator = GetComponent<Animator>();


    }

    // Update is called once per frame
    void Update()
    {
        // Move object while not attacking
        if (!isAttacking)
        {
            transform.Translate(Vector2.left *Time.deltaTime*speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check collision layers
        if (other.gameObject.layer == loseLayer)
        {
            EventHandler.CallEndGameEvent();
        }

        if (other.gameObject.layer != defenderLayer) return;


        if (other.gameObject.GetComponent<Stone>() && enemyType == EnemyType.Fox)
        {
            animator.SetTrigger(Jump);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer != defenderLayer) return;

        // if fox then ignore it
        if(other.gameObject.GetComponent<Stone>() && enemyType == EnemyType.Fox) return;

        // Start attacking
        isAttacking = true;
        target = other.gameObject;
        animator.SetBool(Attack,isAttacking);

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer != defenderLayer) return;

        // Stop attacking
        isAttacking = false;
        target = null;
        animator.SetBool(Attack,isAttacking);
    }

    public void StrikeCurrentTarget()
    {
        if (target!=null)
        {
            target.GetComponent<Health>().TakeDamage(damage);
        }
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void PlayWalkSound()
    {

        if(audioSource.clip==walkAudioClip)    return;

        audioSource.clip = walkAudioClip;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlayJumpSound()
    {
        audioSource.clip = jumpAudioClip;
        audioSource.loop = false;
        audioSource.Play();
    }

    public void PlayAttackSound()
    {
        if(audioSource.clip==attackAudioClip)    return;

        audioSource.clip = attackAudioClip;
        audioSource.loop = true;
        audioSource.Play();
    }


    public void FireProjectile()
    {
        if(target==null)    return;


        if (target.CompareTag("Cactus"))
        {
            if(cactusBiteEffect.activeSelf)    return;
            cactusBiteEffect.SetActive(true);
        }
        else if (target.CompareTag("Gnome"))
        {
            if(gnomeBiteEffect.activeSelf)    return;
            gnomeBiteEffect.SetActive(true);
        }
        else if (target.CompareTag("Stone"))
        {
            if(stoneBiteEffect.activeSelf)    return;
            stoneBiteEffect.SetActive(true);
        }
        else if (target.CompareTag("StarTrophy"))
        {
            if(starTrophyBiteEffect.activeSelf)    return;
            starTrophyBiteEffect.SetActive(true);
        }

    }

    public void StopParticle()
    {
        starTrophyBiteEffect.SetActive(false);
        stoneBiteEffect.SetActive(false);
        gnomeBiteEffect.SetActive(false);
        cactusBiteEffect.SetActive(false);
    }

    public enum EnemyType
    {
        Fox,Lizard,None
    }
}
