using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    public GameObject axeAttackPoint;
    public GameObject Attack_Point;
    public GameObject[] Prefabs;
    private GameObject hitbox;
    public WeaponManager weaponManager = new WeaponManager();
    public WeaponManager.WeponName weponName;

    public bool axeInventory = false;
    public AudioSource source;
    public AudioClip hitsound;

    private void Awake()
    {
        DontDestroyOnLoad(GameObject.Find("Axe"));
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("Enemy Hit : " + other.name + other.transform.position.ToString());

            int damage = weaponManager.GetDamage(weponName);
            Debug.Log("GetDamage : " + damage);
            //other.gameObject.GetComponent<Enermy1>().SetDamage(damage);
            other.gameObject.GetComponent<Enermy>().SetDamage(damage);
            if (hitbox != null) Destroy(hitbox);
            StartCoroutine(Hit());
        }
    }

    IEnumerator Hit()
    {
        Debug.Log(Prefabs.Length + "ÀÇ °¹¼ö");
        hitbox = Instantiate(Prefabs[Random.Range(0, Prefabs.Length)], transform.position, Quaternion.identity);

        source.PlayOneShot(hitsound);
        yield return new WaitForSeconds(2f);
        Destroy(hitbox);
    }

    public void axeOninventory()
    {
        axeInventory = true;
    }

    public void axeOffinventory()
    {
        axeInventory = false;
    }
}
