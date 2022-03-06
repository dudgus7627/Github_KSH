using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public GameObject knifeAttackPoint;
    public GameObject Attack_Point;
    public GameObject[] Prefabs;
    private GameObject hitbox;
    public WeaponManager weaponManager = new WeaponManager();
    public WeaponManager.WeponName weponName;
    public AudioSource source;
    public AudioClip hitsound;
    public bool knifeInventory = false;

    private void Awake()
    {
        DontDestroyOnLoad(GameObject.Find("Knife"));
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            int damage = weaponManager.GetDamage(weponName);
            other.gameObject.GetComponent<Enermy>().SetDamage(damage);
            if (hitbox != null) Destroy(hitbox);
            StartCoroutine(Hit());
        }

        if(other.CompareTag("PlayerHand") && GameManager.Instance.gameState == GameState.Final)
        {
            Debug.Log("ÀÚ»ì");
            StartCoroutine(HitHand(other.gameObject));

            //FadeInOutController.Instance.FadeOut();
            GameManager.Instance.gameState = GameState.EndingCredit;
            GameManager.Instance.ChangeScene();
        }
    }

    IEnumerator Hit()
    {
        hitbox = Instantiate(Prefabs[Random.Range(0, Prefabs.Length)], transform.position, Quaternion.identity);
        source.PlayOneShot(hitsound);
        yield return new WaitForSeconds(2f);
        Destroy(hitbox);
    }

    IEnumerator HitHand(GameObject hand)
    {
        for(int i = 0; i < Prefabs.Length; i++)
        {
            Instantiate(Prefabs[i], hand.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(2f);
        }
        
        source.PlayOneShot(hitsound);
        yield return new WaitForSeconds(2f);
    }

    public void knifeOninventory()
    {
        knifeInventory = true;
    }

    public void knifeOffinventory()
    {
        knifeInventory = false;
    }
}
