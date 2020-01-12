using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseChampion : SimpleStateMachine
{
    // Reference to the character controller used for the movement of the champion
    //CharacterController controller;

    // Reference to the input script to receive input of the player
    PlayerInputController input;

    // Reference to the navmeshagent component
    NavMeshAgent navAgent;

    CharacterData characterData;

    // Reference to the path calculated by the navmeshagent
    NavMeshPath calculatedPath;

    // Reference to the transform of the camera
    Transform cameraT;

    // Enum for all the States of the champion
    // TODO Add more states to the champion states when new states are being implemented
    enum ChampionStates { Idle, Running, BasicAttacking, Spell_1Animation, Spell_2Animation, Spell_3Animation, Spell_4Animation, Dash }

    [Space]
    [Header("Champion Movement")]
    // Character movement speed

    RaycastHit[] rayCastHitCache = new RaycastHit[16];

    Vector3 lastRayCastResult;

    int gameplayLayer;

    int rayToUse = 0;
    int spellRay = 0;

    CharacterData enemy;

    // reference to the bullet prefab for the basic attack
    public GameObject basicAttackProjectile;

    // reference to the spell prefabs
    public GameObject spell_Q;
    public GameObject spell_W;
    public GameObject spell_E;
    public GameObject spell_R;

    GameObject weapon;

    // temporary variables
    float spell_1Duration = 0.4f;
    float spell_1timer;
    float spell_2Duration = 0.4f;
    float spell_2timer;
    float spell_3Duration = 0.4f;
    float spell_3timer;
    float spell_4Duration = 0.4f;
    float spell_4timer;


    float currentDashDistance;
    public float dashLength;
    Vector3 prevDashLoc;
    Vector3 dashDir;
    public float dashSpeed;

    [HideInInspector]
    public GameObject abilityW;






    // Start is called before the first frame update
    void Start()
    {
        calculatedPath = new NavMeshPath();

        navAgent = GetComponent<NavMeshAgent>();

        characterData = GetComponent<CharacterData>();
        enemy = characterData;

        navAgent.speed = characterData.Stats.baseStats.movementSpeed;
        navAgent.angularSpeed = 360.0f;
        navAgent.updateRotation = false;

        lastRayCastResult = transform.position;

        // Grab a reference of the character controller attached to the owner
        //controller = GetComponent<CharacterController>();

        // Grab a reference to the player input script attached to the owner
        input = GetComponent<PlayerInputController>();

        // Get the transform of the camera
        cameraT = Camera.main.transform;

        // TODO fix the object finding to find only the weapon attached to the owner
        weapon = GameObject.Find("Weapon");

        // Set the current state to Idle
        currentState = ChampionStates.Idle;

        gameplayLayer = 1 << LayerMask.NameToLayer("Gameplay");

        abilityW = new GameObject();
    }

    // Update is called once per frame
    void Update()
    {
        SendMessage("SuperUpdate", SendMessageOptions.DontRequireReceiver);

        Ray screenRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (input.Current.RightMouseButtonInput)
        {
            OnRightClick(screenRay);
        }
        if (input.Current.Spell_1Input)
        {
            Spell_1(screenRay);
        }
        if (input.Current.Spell_2Input)
        {
            Spell_2(screenRay);
        }
        if (input.Current.Spell_3Input)
        {
            Spell_3(screenRay);
        }
        if (input.Current.Spell_4Input)
        {
            Spell_4();
        }
        if (input.Current.StopMovingInput)
        {
            StopMoving();
        }


       

    }

    void Idle_EnterState()
    {
        // TODO add functionality for entering Idle State
        Debug.Log("entered idle state");
    }

    void Idle_SuperUpdate()
    {
        // TODO Add functionality for Idle in update
        // Debug.Log("running idle state on update");

    }

    void Idle_ExitState()
    {
        // TODO Add functionality for leaving the idle state
    }

    void Running_EnterState()
    {
        // TODO add functionality for entering Running State
        Debug.Log("entered running state");

    }

    void Running_SuperUpdate()
    {
        // TODO Add functionality for Running in update
        Debug.Log("running running state on update");
        if (navAgent.velocity.magnitude > Mathf.Epsilon)
        {
            RotateChampion();
        }
        else if (navAgent.velocity.magnitude < Mathf.Epsilon && Vector3.Distance(navAgent.pathEndPosition, transform.position) <= 1.2f)
        {
            currentState = ChampionStates.Idle;
        }

    }

    void Running_ExitState()
    {
        // TODO Add functionality for leaving the Running state
    }

    void BasicAttacking_EnterState()
    {

        // TODO add functionality for entering the basic attack state
    }

    void BasicAttacking_SuperUpdate()
    {
        // TODO add functionality for the basic attack update state
        CheckBasicAttack();

    }

    void BasicAttacking_ExitState()
    {
        //enemy = characterData;
        // doet meerdere keren yoinken terwijl die dat maar 1x hoort te doen
        // TODO add functionality for the basic attack leave state
    }

    void Spell_1Animation_EnterState()
    {
        spell_1timer = 0.0f;
        if (navAgent.hasPath)
        {
            navAgent.isStopped = true;
        }

    }

    void Spell_1Animation_SuperUpdate()
    {
        if (spell_1timer >= spell_1Duration)
        {
            currentState = lastState;
        }
        spell_1timer += Time.deltaTime;

    }

    void Spell_1Animation_ExitState()
    {
        if (!currentState.Equals(ChampionStates.Running))
        {
            navAgent.ResetPath();
        }
        navAgent.isStopped = false;
    }

    void Spell_2Animation_EnterState()
    {
        spell_2timer = 0.0f;
        if (navAgent.hasPath)
        {
            navAgent.isStopped = true;
        }

    }

    void Spell_2Animation_SuperUpdate()
    {
        if (spell_2timer >= spell_2Duration)
        {
            currentState = lastState;
        }
        spell_2timer += Time.deltaTime;

    }

    void Spell_2Animation_ExitState()
    {
        if (!currentState.Equals(ChampionStates.Running))
        {
            navAgent.ResetPath();
        }
        navAgent.isStopped = false;
    }

    void Spell_3Animation_EnterState()
    {
        spell_3timer = 0.0f;
        if (navAgent.hasPath)
        {
            navAgent.isStopped = true;
        }

    }

    void Spell_3Animation_SuperUpdate()
    {
        if (spell_3timer >= spell_3Duration)
        {
            currentState = lastState;
        }
        spell_3timer += Time.deltaTime;

    }

    void Spell_3Animation_ExitState()
    {
        if (!currentState.Equals(ChampionStates.Running))
        {
            navAgent.ResetPath();
        }
        navAgent.isStopped = false;
    }

    void Spell_4Animation_EnterState()
    {
        spell_4timer = 0.0f;
        if (navAgent.hasPath)
        {
            navAgent.isStopped = true;
        }

    }

    void Spell_4Animation_SuperUpdate()
    {
        if (spell_4timer >= spell_4Duration)
        {
            currentState = lastState;
        }
        spell_4timer += Time.deltaTime;

    }

    void Spell_4Animation_ExitState()
    {
        if (!currentState.Equals(ChampionStates.Running))
        {
            navAgent.ResetPath();
        }
        navAgent.isStopped = false;
    }

    void Dash_EnterState()
    {
        characterData.spell_3CooldownInternal = characterData.Stats.baseStats.spell3_Cooldown;
        currentDashDistance = 0.0f;
        prevDashLoc = transform.position;
        navAgent.ResetPath();
        input.inputEnabled = false;

    }

    void Dash_SuperUpdate()
    {
        if (currentDashDistance >= dashLength)
        {
            currentState = ChampionStates.Idle;
        }

        currentDashDistance += Vector3.Distance(transform.position, prevDashLoc);
        prevDashLoc = transform.position;
        Dash();

    }

    void Dash_ExitState()
    {
        input.inputEnabled = true;
    }

    private void OnRightClick(Ray screenRay)
    {
        rayCastHitCache = new RaycastHit[16];
        if (Physics.RaycastNonAlloc(screenRay, rayCastHitCache, 9999.0f, gameplayLayer) > 0)
        {
            
            for (int i = 0; i < rayCastHitCache.Length; i++)
            {

                if (rayCastHitCache[i].collider != null)
                {
                    if (rayCastHitCache[i].collider.tag == "Enemy")
                    {
                        rayToUse = i;
                        break;
                    }
                    else if (rayCastHitCache[i].collider.tag == "Floor")
                    {
                        rayToUse = i;
                    }
                }

                
            }
            
            switch (rayCastHitCache[rayToUse].collider.tag)
            {
                case "Enemy":
                    enemy = rayCastHitCache[rayToUse].collider.GetComponent<CharacterData>();
                    if (!currentState.Equals(ChampionStates.BasicAttacking))
                    {
                        StopMoving();
                        currentState = ChampionStates.BasicAttacking;
                    }
                    break;
                case "Floor":
                    MoveToMouseClick(rayToUse);
                    break;
                default:
                    break;
            }
            

        }
    }

    private void CheckBasicAttack()
    {
        if (enemy != characterData)
        {
            if (characterData.CanAttackReach(enemy, characterData.Stats.baseStats.attackRange))
            {
                navAgent.isStopped = true;
                Vector3 targetDir = enemy.transform.position - transform.position;
                transform.rotation = Quaternion.LookRotation(new Vector3(targetDir.normalized.x, 0.0f, targetDir.normalized.z));
                // TODO change rotatechampion function to work for multiple occasions instead of just walking.
                BasicAttack();
            }
            else
            {
                Debug.Log("Enemy out of range");
                RotateChampion();
                navAgent.isStopped = false;
                navAgent.SetDestination(enemy.transform.position);
            }
        }

        

    }

    private void BasicAttack()
    {
        if (characterData.CanAttack)
        {
            GameObject autoAttack = Instantiate(basicAttackProjectile, weapon.transform.position, weapon.transform.rotation);
            autoAttack.GetComponent<BasicProjectile>().Instantiate(characterData, enemy);
            characterData.basicAttackCooldownInternal = 1 / characterData.Stats.baseStats.attackSpeed;
                // TODO research hoe attack speed werkt met items erbij
        }

    }

    private void MoveToMouseClick(int rayToUse)
    {
        navAgent.isStopped = false;
        Vector3 point = rayCastHitCache[rayToUse].point;
        // Avoid the same path if close enough to previous click
        // TODO replace 1.0f with the radius of the champion's width
        if (Vector3.SqrMagnitude(point - transform.position) > 1.5f)
        {
            if (calculatedPath.status == NavMeshPathStatus.PathComplete)
            {
                navAgent.SetPath(calculatedPath);
                calculatedPath.ClearCorners();
                if (!currentState.Equals(ChampionStates.Running))
                {
                    currentState = ChampionStates.Running;
                }

            }
            NavMeshHit hit;
            if (NavMesh.SamplePosition(point, out hit, 0.5f, NavMesh.AllAreas))
            {
                lastRayCastResult = point;


                navAgent.CalculatePath(hit.position, calculatedPath);
            }
        }
        
    }

    void StopMoving()
    {
        if (!currentState.Equals(ChampionStates.Idle))
        {
            if (navAgent.hasPath)
            {
                navAgent.isStopped = true;

                currentState = ChampionStates.Idle;
            }
        }

    }

    private void RotateChampion()
    {
        //Vector3 lookDirection = (point - transform.position).normalized;
        //Quaternion lookRotation = Quaternion.LookRotation(point);
        //transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        //transform.rotation.y = Mathf.Lerp(transform.rotation.y, lookRotation.y, Time.deltaTime * rotationSpeed);
        if (navAgent.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            transform.rotation = Quaternion.LookRotation(navAgent.velocity.normalized);
        }


    }

    void Spell_1(Ray ray)
    {
        if (characterData.CanSpell_1)
        {
            rayCastHitCache = new RaycastHit[16];
            if (Physics.RaycastNonAlloc(ray, rayCastHitCache, 9999.0f, gameplayLayer) > 0)
            {
                for (int i = 0; i < rayCastHitCache.Length; i++)
                {
                    if (rayCastHitCache[i].collider != null)
                    {
                        if (rayCastHitCache[i].collider.tag == "Floor")
                        {
                            spellRay = i;
                        }
                    }
                }
                currentState = ChampionStates.Spell_1Animation;
                Vector3 spellDir = rayCastHitCache[spellRay].point - weapon.transform.position;
                Vector3 lookRot = new Vector3(spellDir.x, 0.0f, spellDir.z).normalized;
                transform.rotation = Quaternion.LookRotation(lookRot);
                GameObject abilityQ = Instantiate(spell_Q, weapon.transform.position, weapon.transform.rotation);
                abilityQ.GetComponent<ProjectileSpell>().Instantiate(characterData, rayCastHitCache[spellRay].point, true);
                characterData.spell_1CooldownInternal = characterData.Stats.baseStats.spell1_Cooldown;
            }
        }
        else
        {
            Debug.Log("spell 1 is on cooldown");
        }

    }

    void Spell_2(Ray ray)
    {
        if (characterData.CanSpell_2)
        {
            rayCastHitCache = new RaycastHit[16];
            if (Physics.RaycastNonAlloc(ray, rayCastHitCache, 9999.0f, gameplayLayer) > 0)
            {
                for (int i = 0; i < rayCastHitCache.Length; i++)
                {
                    if (rayCastHitCache[i].collider != null)
                    {
                        if (rayCastHitCache[i].collider.tag == "Floor")
                        {
                            spellRay = i;
                        }
                    }
                }
            }
            currentState = ChampionStates.Spell_2Animation;
            Vector3 spellDir = rayCastHitCache[spellRay].point - weapon.transform.position;
            Vector3 lookRot = new Vector3(spellDir.x, 0.0f, spellDir.z).normalized;
            transform.rotation = Quaternion.LookRotation(lookRot);
            Vector3 spawnLoc = new Vector3(rayCastHitCache[spellRay].point.x, weapon.transform.position.y, rayCastHitCache[spellRay].point.z);
            abilityW = Instantiate(spell_W, spawnLoc, weapon.transform.rotation);
            abilityW.GetComponent<CrystalBeacon>().Instantiate(characterData);
            characterData.spell_2CooldownInternal = characterData.Stats.baseStats.spell2_Cooldown;  
        }
        else
        {
            Debug.Log("spell 2 is on cooldown");
        }
    }

    void Spell_3(Ray ray)
    {
        if (characterData.CanSpell_3)
        {
            dashDir = new Vector3();
            rayCastHitCache = new RaycastHit[16];
            if (Physics.RaycastNonAlloc(ray, rayCastHitCache, 9999.0f, gameplayLayer) > 0)
            {
                for (int i = 0; i < rayCastHitCache.Length; i++)
                {
                    if (rayCastHitCache[i].collider != null)
                    {
                        if (rayCastHitCache[i].collider.tag == "Floor")
                        {
                            spellRay = i;
                        }
                    }
                }
            }
            dashDir = rayCastHitCache[spellRay].point - transform.position;
            currentState = ChampionStates.Dash;
        }
        else
        {
            Debug.Log("spell 3 is on cooldown");
        }
    }

    void Spell_4()
    {
        Debug.Log("Spell 4");
    }

    void Dash()
    {
        transform.position += dashDir.normalized * dashSpeed * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(new Vector3(dashDir.normalized.x, 0.0f, dashDir.normalized.z));
    }

    void OnTriggerEnter(Collider other)
    {
        if (currentState.Equals(ChampionStates.Dash))
        {
            if (other.gameObject.tag == "Wall")
            {
                currentState = ChampionStates.Idle;
            }
        }

    }
}
