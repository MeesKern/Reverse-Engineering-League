using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpell : MonoBehaviour
{
    CharacterData projectileOwner;

    Rigidbody rb;

    Vector3 projectileDir;
    Vector3 m_targetLoc;

    float speed = 50;
    float maxRange = 10;
    int damage = 60;
    public bool m_canUseCrystal = true;

    // TODO add all those these variables to the character data script


    private Vector3 lastPosition;
    private float distanceTraveled;





    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lastPosition = transform.position;
    }

    public void Instantiate(CharacterData owner, Vector3 targetLoc, bool canUseCrystal)
    {
        // called when the prefab is spawned to set the owner
        projectileOwner = owner;
        projectileDir = targetLoc - transform.position;
        projectileDir.Normalize();
        m_canUseCrystal = canUseCrystal;
        m_targetLoc = targetLoc;
    }

    // Update is called once per frame
    void Update()
    {
        if (distanceTraveled >= maxRange)
        {
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        Move();
        distanceTraveled += Vector3.Distance(transform.position, lastPosition);
        lastPosition = transform.position;
    }

    private void Move()
    {
        // TODO swap force for target.position += a la the dash
        if (!m_canUseCrystal)
        {
            rb.AddForce(m_targetLoc * speed, ForceMode.Force);
        }
        else
        {
            rb.AddForce(projectileDir * speed, ForceMode.Force);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Physics.IgnoreCollision(this.GetComponent<Collider>(), other, true);
        }
        if (other.tag == "Enemy")
        {
            CharacterData enemy = other.gameObject.GetComponent<CharacterData>();
            enemy.GetDamaged(projectileOwner, enemy, damage);
            Destroy(this.gameObject);
        }
    }
}
