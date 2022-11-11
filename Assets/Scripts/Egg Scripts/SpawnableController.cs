using System;
using System.Collections.Generic;
using Player_Scripts;
using SO;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Egg_Scripts
{
    public class SpawnableController : MonoBehaviour
    {
        private float dropSpeed;
        [SerializeField] private Spawnable mySO;
        private Rigidbody2D rb;

        private void Start()
        {
            dropSpeed = Random.Range(mySO.minBaseSpeed, mySO.maxBaseSpeed);
            rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            var moveDirection = new Vector3(0, -1, 0);
            moveDirection = moveDirection.normalized * (dropSpeed * Time.fixedDeltaTime);
            rb.MovePosition(transform.position + moveDirection);
            
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            PoolingManager.Instance.ReleaseToPool(this);
            if (col.collider.GetComponent<PlayerController>()!=null)
            {
                CustomEventHandler<RaindropCaught>.Trigger?.Invoke();
            }
            else
            {
                CustomEventHandler<RaindropMissed>.Trigger?.Invoke();
            }
        }
        
    }
}
