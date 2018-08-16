using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyWalk : MonoBehaviour
{
    public GameObject des;
    // public GameObject argoTo;
    public float speed;
    public float dis;
    public NavMeshAgent agent;
    float recharge;
    public bool argro = false;
    public Animator enemy;
    // Use this for initialization
    void Start()
    {
        des = GameObject.FindGameObjectWithTag("Goal");
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        //enemy.speed = 1;
        enemy.SetInteger("speed", 1);
        //gameObject.GetComponent<EnemyStats>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (gameObject.GetComponent<EnemyStats>().currentTarget == null)
        //{
        //    setupattacker();
        //}

        Vector3 raycastDir;
        RaycastHit hit;
        Ray workingray;
        switch (gameObject.GetComponent<EnemyStats>().myAIMode)
        {
            case EnemyStats.eAIMode.push:
                if (des != null)
                {
                    // if (Vector3.Distance(transform.position, des.transform.position) < 1.0f)
                    //{
                    //    Destroy(gameObject);

                    //}
                    agent.SetDestination(des.transform.position);

                    raycastDir = des.transform.position - transform.position;
                    //raycastDir.y = raycastDir.y + 1.05f;

                    workingray = new Ray(transform.position, raycastDir);

                    if (Physics.Raycast(workingray, out hit))
                    {
                        if (hit.distance <= dis)
                        {
                            if (hit.transform.tag == "Goal")
                            {
                                endzone.health--;
                                RoundSystem.g_iAliveUnits--;
                                Destroy(gameObject);
                            }
                            //print("check point");
                        }
                    }
                    raycastDir = raycastDir.normalized;
                    Debug.DrawRay(transform.position, raycastDir * dis, Color.blue);
                }
                break;

            case EnemyStats.eAIMode.attack:
                agent.stoppingDistance = 2;
                agent.SetDestination(gameObject.GetComponent<EnemyStats>().currentTarget.transform.position);

                raycastDir = gameObject.GetComponent<EnemyStats>().currentTarget.transform.position - transform.position;

                workingray = new Ray(transform.position, raycastDir);
                if (Physics.Raycast(workingray, out hit))
                {
                    if (hit.distance <= gameObject.GetComponent<EnemyStats>().attackrange)
                    {
                        // print("working");
                        if (hit.transform.tag == "Turret")
                        {
                            if (gameObject.tag == "BombEnemy")
                            {
                                hit.transform.SendMessage("TurretShot", gameObject.GetComponent<EnemyStats>().attackdamage);
                                Destroy(gameObject);
                            }
                            else
                            {
                                agent.speed = 0;
                                if (recharge == 0)
                                {
                                   


                                    if (hit.transform.GetComponent<TurretAggro>().f_TurretHealth == 0)
                                    {
                                        gameObject.GetComponent<EnemyStats>().Targets.Clear();
                                        gameObject.GetComponent<EnemyStats>().priority.Clear();
                                        //print("c");
                                        gameObject.GetComponent<EnemyStats>().currentTarget = null;
                                        gameObject.GetComponent<EnemyStats>().myAIMode = EnemyStats.eAIMode.push;

                                        gameObject.GetComponent<enemyWalk>().setupattacker();
                                        gameObject.GetComponent<enemyWalk>().agent.speed = 2;

                                        enemy.SetInteger("speed", 1);
                                        setupattacker();
                                    }
                                    else
                                    {
                                        print("aa");
                                        enemy.GetComponent<setattack>().att = true;
                                        //agent.SetDestination(gameObject.GetComponent<EnemyStats>().currentTarget.transform.position);
                                        //enemy.SetBool("Attack", true);
                                        print("ab");
                                        hit.transform.SendMessage("TurretShot", gameObject.GetComponent<EnemyStats>().attackdamage);
                                        print("ac");// beguin attacking
                                    }
                                    recharge = 90;
                                }
                                else
                                //enemy.SetBool("attack", false);
                                {
                                    recharge--;

                                }
                            }

                        }
                        if (hit.transform.tag == "Barriers")
                        {
                            if (gameObject.tag == "BombEnemy")
                            {
                                hit.transform.SendMessage("TurretShot", gameObject.GetComponent<EnemyStats>().attackdamage);
                                Destroy(gameObject);
                            }
                            else
                            {
                                agent.speed = 0;
                                if (recharge == 0)
                                {
                                    //recharge = 90;

                                    if (hit.transform.GetComponent<barrier>().deffendsHP == 0)
                                    {
                                        gameObject.GetComponent<EnemyStats>().Targets.Clear();
                                        gameObject.GetComponent<EnemyStats>().priority.Clear();
                                        //print("c");
                                        gameObject.GetComponent<EnemyStats>().currentTarget = null;
                                        gameObject.GetComponent<EnemyStats>().myAIMode = EnemyStats.eAIMode.push;

                                        gameObject.GetComponent<enemyWalk>().setupattacker();
                                        gameObject.GetComponent<enemyWalk>().agent.speed = 2;

                                        enemy.SetInteger("speed", 1);
                                        setupattacker();
                                    }
                                    else
                                    {
                                        print("aa");
                                        enemy.GetComponent<setattack>().att = true;
                                        //enemy.SetBool("Attack", true);
                                        //enemy.SetInteger("speed", 1);
                                        //enemy.SetBool("Attack",true);
                                        print("ab");
                                        hit.transform.GetComponent<barrier>().barrierShot(gameObject.GetComponent<EnemyStats>().attackdamage, gameObject);
                                        print("ac");
                                        // hit.transform.SendMessage("barrierShot", gameObject.GetComponent<EnemyStats>().attackdamage,gameObject);
                                    }
                                    recharge = 90;
                                     
                                }
                                else
                                {
                                    //enemy.SetBool("attack", false);
                                    recharge--;
                                }
                            }
                        }
                    }
                }
                //
                break;
            default:
                break;
        }



    }
    public void setupattacker()
    {
        if (gameObject.GetComponent<EnemyStats>().Targets == null)
        {
            gameObject.GetComponent<EnemyStats>().currentTarget = null;
            gameObject.GetComponent<EnemyStats>().myAIMode = EnemyStats.eAIMode.push;
        }
        else
        {

            int best = 0;
            int slot;

            print("d");
            for (int i = 0; i < gameObject.GetComponent<EnemyStats>().Targets.Count; i++)
            {
                if (gameObject.GetComponent<EnemyStats>().priority[i] == best)
                {
                    Vector3 raycastDir = gameObject.GetComponent<EnemyStats>().currentTarget.transform.position - transform.position;

                    float closes;
                    RaycastHit hit;
                    Ray workingray = new Ray(transform.position, raycastDir);
                    Physics.Raycast(workingray, out hit);
                    closes = hit.distance;

                    raycastDir = gameObject.GetComponent<EnemyStats>().Targets[i].transform.position - transform.position;

                    workingray = new Ray(transform.position, raycastDir);
                    Physics.Raycast(workingray, out hit);

                    if (closes > hit.distance)
                    {
                        gameObject.GetComponent<EnemyStats>().currentTarget = gameObject.GetComponent<EnemyStats>().Targets[i];
                        best = gameObject.GetComponent<EnemyStats>().priority[i];
                        slot = i;
                    }

                }
                print("e");
                if (gameObject.GetComponent<EnemyStats>().priority[i] > best)
                {
                    gameObject.GetComponent<EnemyStats>().currentTarget = gameObject.GetComponent<EnemyStats>().Targets[i];
                    best = gameObject.GetComponent<EnemyStats>().priority[i];
                    slot = i;
                }

            }
        }



    }



    void attackchange()
    {

        int fornow = 1;
        switch (gameObject.GetComponent<EnemyStats>().currentTarget.tag)
        {
            case "Barriers":
                if (gameObject.GetComponent<EnemyStats>().currentTarget.GetComponent<barrier>().deffendsHP == 0)
                {

                }
                break;

            default:
                fornow = 1;
                break;
        }


        if (fornow == 0)
        {
            print("end");
            gameObject.GetComponent<EnemyStats>().myAIMode = EnemyStats.eAIMode.push;
        }
        else
        {


        }
    }


    void attacking()
    {
        enemy.SetBool("Attack", true);
    }
}