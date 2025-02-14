using UnityEngine;

public class TankController : MonoBehaviour
{
    [Header("Tank Movement")]
    public float moveSpeed = 5f;
    public float rotateSpeed = 60f;

    [Header("Turret Control")]
    public Transform turret;
    public float turretRotateSpeed = 100f;

    [Header("Firing")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireForce = 20f;
    public float fireRate = 1f;
    private float nextFireTime = 0f;

    private void Update()
    {
        MoveTank();
        RotateTurret();
        FireCannon();
    }

    void MoveTank()
    {
        float move = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        float rotate = Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime;

        transform.Translate(Vector3.forward * move);
        transform.Rotate(Vector3.up * rotate);
    }

    void RotateTurret()
    {
        float turretRotate = Input.GetAxis("Mouse X") * turretRotateSpeed * Time.deltaTime;
        turret.Rotate(Vector3.up * turretRotate);
    }

    void FireCannon()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + 1f / fireRate;

            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.linearVelocity = firePoint.forward * fireForce;

            Destroy(projectile, 3f); // Destroy projectile after 3 seconds
        }
    }

}
