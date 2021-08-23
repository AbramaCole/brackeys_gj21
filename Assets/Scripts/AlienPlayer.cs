using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienPlayer : MonoBehaviour
{
    [SerializeField] private float speed = 2;
    [SerializeField] private float projectileSpeed = .5f;
    [SerializeField] private float shotsPerSecond = 3;
    [SerializeField] private GameObject projectilePrefab;
    
    
    private Rigidbody2D rb;
    private Transform projectileSpawnTransform;
    private List<GameObject> projectiles = new List<GameObject>();
    private float secondsPerShot = 1;
    private float lastShotTime = 0;


    private void OnValidate()
    {
        secondsPerShot = 1 / shotsPerSecond;
    }

    void Start()
    {
#if !UNITY_EDITOR
        OnValidate();
#endif
        rb = GetComponent<Rigidbody2D>();
        projectileSpawnTransform = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveDir = Vector3.zero;

        if(Input.GetKey(KeyCode.W))
        {
            //moveDir.y += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            //moveDir.y -= 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDir.x -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDir.x += 1;
        }

        if(Input.GetKey(KeyCode.Space) && (Time.time - secondsPerShot) > lastShotTime)
        {
            FireProjectile();
        }

        rb.position += Vector2.right * moveDir * speed * Time.deltaTime;
        rb.position = new Vector2(Mathf.Clamp(rb.position.x, -8, 8), rb.position.y);
    }

    private void FixedUpdate()
    {
        for(int i = 0; i < projectiles.Count; ++i)
        {
            if(projectiles[i].transform.position.y > -9)
            {
                Destroy(projectiles[i]);
                projectiles.RemoveAt(i);
            }
            else
            {
                projectiles[i].transform.position += Vector3.up * projectileSpeed;
            }
        }
    }

    private void FireProjectile()
    {
        lastShotTime = Time.time;
        GameObject p = Instantiate(projectilePrefab);
        p.transform.position = projectileSpawnTransform.position;
        projectiles.Add(p);
    }
}
