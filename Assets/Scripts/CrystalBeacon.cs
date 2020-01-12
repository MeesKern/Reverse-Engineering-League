using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalBeacon : MonoBehaviour
{
    // TODO add all these variables to character data
    float rotationSpeed = 50.0f;
    float aliveTimer = 8.0f;
    float internalTimerAlive;
    public GameObject spell_Q;
    CharacterData m_owner;

    Vector3[] dirList = new Vector3[8];

    // Start is called before the first frame update
    void Start()
    {
        internalTimerAlive = 0.0f;
        dirList[0] = new Vector3(0, 0, 1);
        dirList[1] = new Vector3(1, 0, 1);
        dirList[2] = new Vector3(1, 0, 0);
        dirList[3] = new Vector3(1, 0, -1);
        dirList[4] = new Vector3(0, 0, -1);
        dirList[5] = new Vector3(-1, 0, -1);
        dirList[6] = new Vector3(-1, 0, 0);
        dirList[7] = new Vector3(-1, 0, 1);
    }

    public void Instantiate(CharacterData owner)
    {
        m_owner = owner;
    }

    // Update is called once per frame
    void Update()
    {
        if (internalTimerAlive >= aliveTimer)
            Remove();

        internalTimerAlive += Time.deltaTime;
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.Self);
    }

    void Remove()
    {
        Destroy(this.gameObject);
        m_owner.GetComponent<BaseChampion>().abilityW = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ProjectileSpell>() != null)
        {
            if (other.GetComponent<ProjectileSpell>().m_canUseCrystal)
            {
                Destroy(other.gameObject);
                for (int i = 0; i < 8; i++)
                {
                    GameObject abilityQ = Instantiate(spell_Q, transform.position, transform.rotation);
                    abilityQ.GetComponent<ProjectileSpell>().Instantiate(m_owner, dirList[i], false);
                }
            }
            else
            {
                Physics.IgnoreCollision(this.GetComponent<Collider>(), other, true);
            }
        }
    }
}
