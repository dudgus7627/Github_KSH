using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Gun : MonoBehaviour
{

    [Header("Prefab Refrences")]
    public GameObject bulletPrefab;
    public GameObject casingPrefab;
    public GameObject muzzleFlashPrefab;

    [Header("Location Refrences")]
    [SerializeField] private Transform uziBarrel;
    [SerializeField] private Transform casingExitLocation;

    [Header("Settings")]
    [SerializeField] public LayerMask layer;
    [SerializeField] private float bulletSpeed = 100f;
    [SerializeField] private float range = 50f;

    [Tooltip("Casing Ejection Speed")] [SerializeField] private float ejectPower = 150f;

    public AudioSource source;
    public AudioClip firesound;
    public AudioClip norerolad;
    public AudioClip reload;

    [SerializeField] private XRNode inputs;

    public bool isGrab;
    public bool isinvantory = false;
    public float fireRate = 0.5f;
    private float nextFire = 0.0f;
    private float reloadRate = 1f;
    public GameObject[] prefabs;
    private GameObject hitbox;
    RaycastHit hit;

    public class BloodEffect
    {
        public GameObject obj;
        public float timer;
    }

    WeaponManager weaponManager = new WeaponManager();
    public WeaponManager.WeponName weponName;
    public enum gunstate
    {
        SHOOT,
        NO_BULLET,
        RELOAD
    };

    void Start()
    {
        DontDestroyOnLoad(GameObject.Find("Uzi"));

        if (uziBarrel == null)
            uziBarrel = transform;
        weaponManager.uziCurrentBuellt += weaponManager.uziMaxBuellt;
        if (mParticleList == null)
            mParticleList = new List<BloodEffect>();
        StartCoroutine(@BloodEffectCoroutine());
    }

    IEnumerator @BloodEffectCoroutine()
    {
        List<BloodEffect> list = new List<BloodEffect>();
        while (true)
        {
            foreach (BloodEffect blood in mParticleList)
            {
                if (blood.timer > 2)
                {
                    GameObject.Destroy(blood.obj);
                    list.Add(blood);
                }
                else
                {
                    blood.timer += 1;
                }
            }
            foreach (BloodEffect blood in list)
            {
                mParticleList.Remove(blood);
            }
            list.Clear();
            yield return new WaitForSeconds(1);
        }
    }
    public List<BloodEffect> mParticleList;

    void Update()
    {
        Debug.DrawRay(uziBarrel.position, uziBarrel.TransformDirection(Vector3.forward) * 100f, Color.green);

        InputDevice device = InputDevices.GetDeviceAtXRNode(inputs);
        bool is_trigger = false;
        bool is_reLoad = false;

        device.TryGetFeatureValue(CommonUsages.triggerButton, out is_trigger);
        if (isinvantory == false)
        {
            if (isGrab && is_trigger && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                Shoot();
            }
        }
        device.TryGetFeatureValue(CommonUsages.secondaryButton, out is_reLoad);
        if (isGrab && is_reLoad && Time.time > nextFire && weaponManager.uziCurrentBuellt != weaponManager.uziMaxBuellt)
        {
            is_trigger = false;
            nextFire = Time.time + reloadRate;
            StartCoroutine(Reload());
        }
    }

    void Shoot()
    {
        if (weaponManager.uziCurrentBuellt <= 0)
            source.PlayOneShot(norerolad);
        else if (weaponManager.uziCurrentBuellt > 0)
        {
            weaponManager.uziCurrentBuellt--;
            source.PlayOneShot(firesound);

            if (Physics.Raycast(uziBarrel.transform.position, uziBarrel.transform.forward, out hit, range, layer))
            {
                Debug.Log(hit.transform.name);
                int damage = weaponManager.GetDamage(weponName);
                hit.transform.gameObject.GetComponent<Enermy>().SetDamage(damage);
                Hit(hit.point);
            }
            if (muzzleFlashPrefab)
            {
                GameObject TempFlash = Instantiate(muzzleFlashPrefab, uziBarrel.position, uziBarrel.rotation);
                Destroy(TempFlash, 0.5f);
            }
            if (bulletPrefab)
            {
                GameObject tempBulletHead = Instantiate(bulletPrefab, uziBarrel.position, uziBarrel.rotation);
                tempBulletHead.GetComponent<Rigidbody>().AddForce(uziBarrel.forward * bulletSpeed);
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
    IEnumerator Reload()
    {
        source.PlayOneShot(reload);
        yield return new WaitForSeconds(3f);
        weaponManager.uziCurrentBuellt = weaponManager.uziMaxBuellt;
    }
    void Hit(Vector3 hit)
    {
        BloodEffect bloodEffect = new BloodEffect();

        hitbox = Instantiate(prefabs[Random.Range(0, prefabs.Length)], hit, Quaternion.identity);
        bloodEffect.obj = hitbox;
        bloodEffect.timer = 0;
        mParticleList.Add(bloodEffect);
    }
    public void SelectedEnter()
    {
        isGrab = true;
    }
    public void SelectedExit()
    {
        isGrab = false;
    }
    public void OnInisinvantory()
    {
        isinvantory = true;
    }
    public void OnOutisinvantory()
    {
        isinvantory = false;
    }
}