using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

#if UNITY_EDITOR
using UnityEditor;
#endif
public class NomalEController : Enermy
{
    [Header("Controller")]
    [SerializeField]
    private bool dead = false;
    private State state;
    private NavMeshAgent enemy;
    private Animator animator;
    private AudioSource audioPlayer;
    public AudioClip IdleClip;  //idle sound
    public AudioClip AttackClip;
    public AudioClip hitClip;
    public AudioClip deadClip;
    [SerializeField]
    public GameObject[] items = new GameObject[3];
    public GameObject DeadParticle;
    Transform enemytrans;
    private float moveSpeed = 1.0f;
    private float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;
    Transform target;

    private enum State
    {
        Idle,
        Traking,
        AttackStart,
        AttackStop,
        Dead
    }

    public void Awake()
    {
        base.Awake();
        enemy = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioPlayer = GetComponent<AudioSource>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemytrans = GameObject.FindGameObjectWithTag("Enemy").transform;
        Debug.Log("Target : " + target.ToString());

        Idle();

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other == GameObject.FindGameObjectWithTag("Bullet"))
        {
            if (hitClip != null) audioPlayer.PlayOneShot(hitClip);
            animator.SetTrigger("Damage");
        }

        if (other == GameObject.FindGameObjectWithTag("Axe"))
        {
            if (hitClip != null) audioPlayer.PlayOneShot(hitClip);
            animator.SetTrigger("Damage");
        }

        if (other == GameObject.FindGameObjectWithTag("Knife"))
        {
            if (hitClip != null) audioPlayer.PlayOneShot(hitClip);
            animator.SetTrigger("Damage");
        }

    }

    public void Update()
    {
        //Debug.Log(", dist : " + Vector3.Distance(target.transform.position,
        //       transform.position));

        if (!dead && enermyState.enermyCurrenthp <= 0)
        {
            StartCoroutine(OnDead());
        }

        if (state == State.Idle)
        {
            if (Vector3.Distance(target.transform.position,
               transform.position) <= 10.5f && Vector3.Distance(target.transform.position,
               transform.position) > 3f && !enemy.pathPending)
            {
                if (enemy.remainingDistance <= 10.5f)
                {
                    //Debug.Log("추격 : " + Vector3.Distance(target.transform.position,
                    //transform.position).ToString());
                    /*Debug.Log("pathPending : " + enemy.pathPending.ToString());
                    Debug.Log("remainingDistance : " + enemy.remainingDistance.ToString());*/
                    Tracking();
                }
            }

        }

        else if (state == State.Traking && Vector3.Distance(target.position,
        transform.position) <= 3f)
        {
            AttackStart();
        }

        else if (state == State.AttackStart)
        {
            if (Vector3.Distance(target.transform.position, transform.position) >= 4f)
            {
                AttackStop();
            }
            var lookRotation = Quaternion.LookRotation(target.transform.position - transform.position, Vector3.up);
            var targetAngleY = lookRotation.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngleY, ref turnSmoothVelocity, turnSmoothTime);
        }

        if (dam != 0 && state != State.Dead)
        {

            if (Vector3.Distance(target.transform.position,
              transform.position) > 3.5f && !enemy.pathPending && state != State.AttackStart)
            {
                //Debug.Log("추격 : " + Vector3.Distance(target.transform.position,
                //transform.position).ToString());

                Tracking();

            }
            else if (state == State.Traking && Vector3.Distance(target.transform.position,
        transform.position) <= 3f)
            {
                AttackStart();
                if (state == State.AttackStart && Vector3.Distance(target.transform.position, transform.position) > 4f)
                {
                    AttackStop();

                }
            }

        }
    }

    public void Idle()
    {
        state = State.Idle;
        if (IdleClip != null) audioPlayer.PlayOneShot(IdleClip);
    }


    public void Tracking()
    {
        //Debug.Log("타겟 : " + target.ToString());
        state = State.Traking;
        var lookRotation = Quaternion.LookRotation(target.transform.position - transform.position, Vector3.up);
        var targetAngleY = lookRotation.eulerAngles.y;
        transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngleY, ref turnSmoothVelocity, turnSmoothTime);
        enemy.SetDestination(target.transform.position);
        animator.SetInteger("moving", 1); //Run
    }

    public void AttackStart()
    {

        state = State.AttackStart;
        enemy.isStopped = true;
        animator.SetBool("attack", true);
        if (AttackClip != null) audioPlayer.PlayOneShot(AttackClip);
    }

    public void AttackStop()
    {
        state = State.Idle;
        var lookRotation = Quaternion.LookRotation(target.transform.position - transform.position, Vector3.up);
        var targetAngleY = lookRotation.eulerAngles.y;
        transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngleY, ref turnSmoothVelocity, turnSmoothTime);
        animator.SetBool("attack", false);
        enemy.isStopped = false;
    }


    /*    public void OnDamage()
        {

            //데미지 입었을때
            if (!dead && enermyState1.enermyCurrenthp > 0 && enermyState1.enermyCurrenthp < 500)
            {
                //state = State.Damage;
                if (hitClip != null) audioPlayer.PlayOneShot(hitClip);
                animator.SetTrigger("Damage");

            }
        }
    */

    IEnumerator OnDead()
    {
        state = State.Dead;
        dead = true;
        enemy.isStopped = true;
        HPSlider.gameObject.SetActive(false);
        GetComponent<Collider>().enabled = false;
        animator.applyRootMotion = true;
        animator.SetBool("Dead", true);
        if (deadClip != null) audioPlayer.PlayOneShot(deadClip);
        yield return new WaitForSeconds(4.7f);
        DeadEffect();
        gameObject.SetActive(false);
        dropTheItems();

    }
    public void DeadEffect()
    {
        Instantiate(DeadParticle, enemy.transform.position, enemy.transform.rotation);
        Debug.Log("파티클 소환");
    }
    public void dropTheItems()
    {
        Debug.LogWarning("아이템 드랍");
        int maxItems = 3;
        //yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < maxItems; i++)
        {
            int rand = Random.Range(0, 3);
            //yield return new WaitForSeconds(0.3f);
            Instantiate(items[rand], enemytrans.position, Quaternion.identity);
        }
    }
}
