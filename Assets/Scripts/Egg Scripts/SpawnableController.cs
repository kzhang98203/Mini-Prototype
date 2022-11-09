using System;
using System.Collections.Generic;
using SO;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Egg_Scripts
{
    public class SpawnableController : MonoBehaviour
    {
        private float dropSpeed;
        [SerializeField] private Spawnable mySO;
        private Rigidbody rb;

        private void Start()
        {
            dropSpeed = Random.Range(mySO.minBaseSpeed, mySO.maxBaseSpeed);
            rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            var moveDirection = new Vector3(0, -1, 0);
            moveDirection = moveDirection.normalized * (dropSpeed * Time.fixedDeltaTime);
            rb.MovePosition(transform.position + moveDirection);
            
        }


        private void OnCollisionEnter(Collision collision)
        {
            //Destroy(gameObject);
            PoolingManager.Instance.ReleaseToPool(this);
        }
    }
}
