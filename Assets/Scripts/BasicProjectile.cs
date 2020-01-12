using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    CharacterData enemy;
    CharacterData projectileOwner;

    Rigidbody rb;


    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    public void Instantiate(CharacterData owner, CharacterData target)
    {
        // called when the prefab is created to set the owner and target of the projectile
        enemy = target;
        projectileOwner = owner;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector3 point = enemy.transform.position;
        Vector3 dir = point - transform.position;
        dir.Normalize();

        rb.AddForce(dir * projectileOwner.Stats.baseStats.projectileSpeed, ForceMode.Force);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemy.GetDamaged(projectileOwner, enemy, projectileOwner.Stats.baseStats.attackDamage);
            Destroy(this.gameObject);
        }
        if (other.tag == "Projectile")
        {
            Physics.IgnoreCollision(this.GetComponent<Collider>(), other, true);
        }
    }

}
