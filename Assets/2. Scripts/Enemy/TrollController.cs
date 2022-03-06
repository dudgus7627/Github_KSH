using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

#if UNITY_EDITOR
using UnityEditor;
#endif
public class TrollController : Enermy
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
    public GameObject[] items = new GameObject[4];
    public GameObject DeadParticle;
    Transform enemytrans;
    private float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;

    Transform target;

    public GameObject homePortal;

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
        target = GameObject.FindGameObjectWithTag("Player").transform;
        enemytrans = GameObject.FindGameObjectWithTag("Enemy").transform;
        Idle();
    }



    public void Update()
    {
        /*Debug.Log(dead.ToString() + state.ToString() + ", dist : " + Vector3.Distance(target.transform.position,
               transform.position));*/
        //Debug.Log("state : " + state.ToString());
        //Debug.Log("Maxcurrenthp : " + enermyState.MaxcurrentHp.ToString());
        //Debug.Log("currenthp : " + enermyState.currenthp.ToString());

        if (!dead && enermyState.enermyCurrenthp <= 0)
        {
            StartCoroutine(OnDead());
        }

        if (state == State.Idle)
        {
            if (Vector3.Distance(target.transform.position,
               transform.position) <= 8.5f && Vector3.Distance(target.transform.position,
               transform.position) > 3.5f && !enemy.pathPending)
            {
                if (enemy.remainingDistance <= 8.5f)
                {

                    Tracking();
                }
            }

        }

        else if (state == State.Traking && Vector3.Distance(target.transform.position,
        transform.position) <= 3.5f)
        {
            AttackStart();
        }

        else if (state == State.AttackStart)
        {
            if (Vector3.Distance(target.transform.position, transform.position) >= 5f)
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
              transform.position) > 4.5f && !enemy.pathPending && state != State.AttackStart)
            {
                //Debug.Log("추격 : " + Vector3.Distance(target.transform.position,
                //transform.position).ToString());

                Tracking();

            }
            else if (state == State.Traking && Vector3.Distance(target.transform.position,
        transform.position) <= 3.5f)
            {
                AttackStart();
                if (state == State.AttackStart && Vector3.Distance(target.transform.position, transform.position) > 5.5f)
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
        //애너미 체력 50퍼이하면 스턴
    }

    public void AttackStop()
    {
        state = State.Idle;
        var lookRotation = Quaternion.LookRotation(target.transform.position - transform.position, Vector3.up);
        var targetAngleY = lookRotation.eulerAngles.y;
        transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngleY, ref turnSmoothVelocity, turnSmoothTime);
        enemy.isStopped = false;
        animator.SetBool("attack", false);
    }



    IEnumerator OnDead()
    {

        state = State.Dead;
        dead = true;
        enemy.isStopped = true;
        if (deadClip != null) audioPlayer.PlayOneShot(deadClip);
        yield return new WaitForSeconds(0.1f);
        HPSlider.gameObject.SetActive(false);
        GetComponent<Collider>().enabled = false;
        animator.applyRootMotion = true;
        animator.SetBool("Dead", true);
        yield return new WaitForSeconds(4.5f);
        DeadEffect();
        gameObject.SetActive(false);
        dropTheItems();
        GameManager.Instance.gameState = GameState.Final;
        homePortal.gameObject.SetActive(true);
    }
    public void DeadEffect()
    {
        Instantiate(DeadParticle, enemy.transform.position, enemy.transform.rotation);
        Debug.Log("파티클 소환");
    }
    public void dropTheItems()
    {
        Debug.LogWarning("아이템 드랍");
        int maxItems = 4;
        Vector3 Dropenemytrans = new Vector3(enemy.transform.position.x, 0.5f, enemy.transform.position.z);

        for (int i = 0; i < maxItems; i++)
        {
            Instantiate(items[i], Dropenemytrans, Quaternion.identity);
        }
    }
}
