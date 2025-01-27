using System;
using UnityEngine;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyProperties enemyProperties;
        [SerializeField] private Rigidbody2D myRigidbody;

        private void FixedUpdate()
        {
            Move();
        }

        private float GetPlayerDirection()
        {
            return Mathf.Sign(transform.localScale.x);
        }

        private void Move()
        {
            myRigidbody.velocity = new Vector2(GetPlayerDirection() * enemyProperties.speed, myRigidbody.velocity.y);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }
}