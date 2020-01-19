using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField]
    float lifetime;
    private void Update()
    {
        lifetime = lifetime + 1 * Time.deltaTime;
        if (lifetime >= 100f)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.otherCollider.tag != "Bullet" || collision.otherCollider.tag != "Player")
        {

            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}
