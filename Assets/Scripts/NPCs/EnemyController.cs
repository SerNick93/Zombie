using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    float health;
    [SerializeField]
    float lookRaduis = 10f;
    Transform target;
    NavMeshAgent agent;



    // Start is called before the first frame update
    void Start()
    {
        target = PlayerMovement.MyInstance.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //Find player
        float distance = Vector3.Distance(target.position, transform.position);
        //Is player within the look radius?
        if (distance <= lookRaduis)
        {
            agent.SetDestination(target.position);
            if (distance <= agent.stoppingDistance)
            {
                FaceTarget();
            }
        }
    }
    //Face the player
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, -90, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRaduis);
    }
    public void TakeDamage(float damage)
    {

        if (health > 0)
        {
            Debug.Log("Enemy has taken: " + damage + " damage.");
            health -= damage;
        }
       else if (health <= 0)
        {
            Debug.Log("Enemy has died.");
            Destroy(gameObject);
        }
    }
}
