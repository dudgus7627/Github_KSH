using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class SimpleShoot : MonoBehaviour
{
    [Header("Prefab Refrences")]
    public GameObject bulletPrefab;
    public GameObject casingPrefab;
    public GameObject muzzleFlashPrefab;

    [Header("Location Refrences")]
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private Transform barrelLocation;
    [SerializeField] private Transform casingExitLocation;
    [SerializeField] private float bulletSpeed = 100f;
    [SerializeField] private float range = 50f;

    [Header("Settings")]
    [Tooltip("Bullet Speed")] [SerializeField] private float shotPower = 500f;
    [Tooltip("Casing Ejection Speed")] [SerializeField] private float ejectPower = 150f;

    public AudioSource source;
    public AudioClip firesound;
    public AudioClip norerolad;
    public AudioClip reload;

    private bool isGrab2;
    public bool inventoryCheck = false;
    private float nextFire = 0.0f;
    private float reloadRate = 1f;
    private float waitingtime = 3.0f;

    public GameObject[] prefabs;
    private GameObject hitbox;
    public LayerMask layer;

    RaycastHit hit;
    public XRNode inputs;
    WeaponManager weaponManager = new WeaponManager();
    Gun gun = new Gun();
    public WeaponManager.WeponName weponName;
    void Start()
    {
        if (barrelLocation == null)
            barrelLocation = transform;
        if (gunAnimator == null)
            gunAnimator = GetComponentInChildren<Animator>();
        weaponManager.pistolCurrentBuellt += weaponManager.pistolMaxBuellt;
    }

    public void PullTheTrigger()
    {
        gunAnimator.SetTrigger("Fire");
    }

    private void Update()
    {
        InputDevice device1 = InputDevices.GetDeviceAtXRNode(inputs);
        bool is_reLoad = false;
        device1.TryGetFeatureValue(CommonUsages.secondaryButton, out is_reLoad);
        if (isGrab2 && is_reLoad && weaponManager.pistolCurrentBuellt != weaponManager.pistolMaxBuellt && Time.time > nextFire)
        {
            nextFire = Time.time + reloadRate;
            StartCoroutine(ReLoad());
        }

    }
    //This function creates the bullet behavior
    public void Shoot()
    {
        if (inventoryCheck == false)
        {
            gun.isGrab = false;
            Debug.Log(gun.isGrab);

            if (weaponManager.pistolCurrentBuellt <= 0)
            {
                source.PlayOneShot(norerolad);
            }
            else if (weaponManager.pistolCurrentBuellt > 0)
            {
                weaponManager.pistolCurrentBuellt--;
                source.PlayOneShot(firesound);

                if (Physics.Raycast(barrelLocation.transform.position, barrelLocation.transform.forward, out hit, range, layer))
                {
                    Debug.Log(hit.transform.name);
                    int damage = weaponManager.GetDamage(weponName);
                    hit.transform.gameObject.GetComponent<Enermy>().SetDamage(damage);
                    StartCoroutine(Hit(hit.point));
                }

                if (muzzleFlashPrefab)
                {
                    GameObject TempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);
                    Destroy(TempFlash, 0.5f);
                }
                if (bulletPrefab)
                {
                    GameObject tempBulletHead = Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation);
                    tempBulletHead.GetComponent<Rigidbody>().AddForce(barrelLocation.forward * bulletSpeed);
                }
                if (casingPrefab)
                {
                    GameObject tempCasing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation) as GameObject;
                    //Add force on casing to push it out
                    tempCasing.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(ejectPower * 0.7f, ejectPower), (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);
                    //Add torque to make casing spin in random direction
                    tempCasing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(100f, 1000f)), ForceMode.Impulse);
                }
            }
        }
    }

    IEnumerator Hit(Vector3 hit)
    {
        hitbox = Instantiate(prefabs[Random.Range(0, prefabs.Length)], hit, Quaternion.identity);
        yield return new WaitForSeconds(2f);
        Destroy(hitbox);
    }

    IEnumerator ReLoad()
    {
        source.PlayOneShot(reload);
        yield return new WaitForSeconds(3f);
        weaponManager.pistolCurrentBuellt = weaponManager.pistolMaxBuellt;
    }

    public void SelectedEnter()
    {
        isGrab2 = true;
    }
    public void SelectedExit()
    {
        isGrab2 = false;
    }

    public void onInventory()
    {
        inventoryCheck = true;
    }
    public void offInventory()
    {
        inventoryCheck = false;
    }
}