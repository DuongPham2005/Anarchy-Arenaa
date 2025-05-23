using Photon.Pun;
using Photon.Pun.UtilityScripts;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage;

    public Camera camera;

    public float fireRate;

    [Header("VFX")]
    public GameObject hitVFX;

    private float nextFire;

    [Header("Ammo")]
    public int mag = 5;

    public int ammo = 30;
    public int magAmmo = 30;

    [Header("SFX")] public int shootSFXIndex = 0;
    public PlayerPhotonSoundManager playerPhotonSoundManager;

    [Header("UI")]
    public TextMeshProUGUI magText;
    public TextMeshProUGUI ammoText;

    [Header("Animation")]
    public Animation animation;
    public AnimationClip reload;

    [Header("Recoil Settings")]
//    [Range(0, 1)]
 //   public float recoilPerent = 0.3f;

    [Range(0, 2)]
    public float recoverPerent = 0.7f;
    [Space]
    public float recoilUp = 1f;
    public float recoilBack = 0f;

    private Vector3 originalPosition;
    private Vector3 recoilVelocity = Vector3.zero;

    private float recoilLength;
    private float recoverLength;

    private bool recoiling;
    public bool recovering;

    void Start()
    {
        magText.text = mag.ToString();
        ammoText.text = ammo + "/" + magAmmo;

        originalPosition = transform.localPosition;

        recoilLength = 0;
        recoverLength = 1 / fireRate * recoverPerent;

    }
    // Update is called once per frame
    void Update()
    {
        if (nextFire > 0)
        {
            nextFire -= Time.deltaTime;
        }

        if (Input.GetButton("Fire1") && nextFire <= 0 && ammo > 0 && animation.isPlaying == false)
        {
            nextFire = 1 / fireRate;

            ammo--;

            magText.text = mag.ToString();
            ammoText.text = ammo + "/" + magAmmo;

            Fire();
        }

        if (Input.GetKeyDown(KeyCode.R) && mag > 0)
        {
            Reload();
        }

        if(recoiling)
        {
            Recoil();
        }
        if(recovering)
        {
            Recovering();
        }
    }


    void Reload()
    {
        animation.Play(reload.name);

        if(mag > 0)
        {
            mag--;

            ammo = magAmmo;
        }

        magText.text = mag.ToString();
        ammoText.text = ammo + "/" + magAmmo;
    }
    void Fire()
    {
        recoiling = true;
        recovering = false;

        playerPhotonSoundManager.PlayShootSFX(shootSFXIndex);

        Ray ray = new Ray(origin: camera.transform.position, direction: camera.transform.forward);

        RaycastHit hit;

        //PhotonNetwork.LocalPlayer.AddScore(1);

        if (Physics.Raycast(ray.origin, ray.direction, out hit, maxDistance: 100f))
        {
            PhotonNetwork.Instantiate(hitVFX.name, hit.point, Quaternion.identity);

            if (hit.transform.gameObject.GetComponent<Health>())
            {
                PhotonNetwork.LocalPlayer.AddScore(damage);

                if(damage >= hit.transform.gameObject.GetComponent<Health>().health)
                {
                    // kill

                    RoomManager.instance.kills++;
                    RoomManager.instance.SetHashes();

                    PhotonNetwork.LocalPlayer.AddScore(100);
                }

                hit.transform.gameObject.GetComponent<PhotonView>().RPC(methodName: "TakeDamage", RpcTarget.All, damage);
            }
                
        }
    }

    void Recoil()
    {
        Vector3 finalPosition = new Vector3(originalPosition.x, originalPosition.y + recoilUp, originalPosition.z - recoilBack);

        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, finalPosition, ref recoilVelocity, recoilLength);

        if(transform.localPosition == finalPosition)
        {
            recoiling = false;
            recovering = true;
        }
    } 
    void Recovering()
    {
        Vector3 finalPosition = originalPosition;

        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, finalPosition, ref recoilVelocity, recoverLength);

        if(transform.localPosition == finalPosition)
        {
            recoiling = false;
            recovering = false;
        }
    }
}