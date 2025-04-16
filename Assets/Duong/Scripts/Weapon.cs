using Photon.Pun;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage;

    public Camera camera;

    public float fireRate;

    private float nextFire;

    // Update is called once per frame
    void Update()
    {
        if (nextFire > 0)
            nextFire -= Time.deltaTime;

        if (Input.GetButton("Fire1") && nextFire <= 0)
        {
            nextFire = 1 / fireRate;
            Fire();
        }
    }

    // usage
    void Fire()
    {
        Ray ray = new Ray(origin: camera.transform.position, direction: camera.transform.forward);

        RaycastHit hit;

        if (Physics.Raycast(ray.origin, ray.direction, out hit, maxDistance: 100f))
        {
            if (hit.transform.gameObject.GetComponent<Health>())
                hit.transform.gameObject.GetComponent<PhotonView>().RPC(methodName: "TakeDamage", RpcTarget.All, damage);
        }
    }
}