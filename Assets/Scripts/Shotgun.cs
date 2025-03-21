using UnityEngine;

public class Shotgun : MonoBehaviour
{
    public Transform cameraPos;
    public Transform muzzlePoint;
    public LayerMask enemyLayer;
    public RaycastHit rayHit;

    public GameObject hitParticle;

    public float spread;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
    }

    void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            for (int i = 0; i < 5; i++)
            {
                Shoot();
            }

            AudioManager.instance.PlaySFX(0);
        }
    }

    void Shoot()
    {
        // raycast
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        Vector3 direction = cameraPos.forward + new Vector3(x, y, 0);

        if (Physics.Raycast(cameraPos.transform.position, direction, out rayHit, 50f))
        {
            Debug.Log(rayHit.collider.name);

            Instantiate(hitParticle, rayHit.point, Quaternion.LookRotation(rayHit.normal));
        }
    }
}
