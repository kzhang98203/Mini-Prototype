using System;
using Joystick_Pack.Scripts.Joysticks;
using UnityEngine;
using UnityEngine.UI;

namespace Player_Scripts
{
    public class PlayerController : MonoBehaviour
    {
        private Animator animator;
        [SerializeField] private float playerSpeed;
        [SerializeField] private FloatingJoystick movementStick;
        [SerializeField] private Button leftButton;
        [SerializeField] private Button rightButton;
        private Rigidbody2D rb;
        private float horizontalMovement;
        
        private static readonly int triggerLeanLeft = Animator.StringToHash("Trigger Lean Left");
        private static readonly int triggerLeanRight = Animator.StringToHash("Trigger Lean Right");
        private static readonly int triggerRestoreFromLeft = Animator.StringToHash("Trigger Restore From Left");
        private static readonly int triggerRestoreFromRight = Animator.StringToHash("Trigger Restore From Right");

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            CustomEventHandler<RaindropCaught>.Register(OnCatchRainDrop);
        }

        private void OnDisable()
        {
            CustomEventHandler<RaindropCaught>.Unregister(OnCatchRainDrop);
        }

        private void Update()
        {
            //horizontalMovement = 0f;
            if (Input.GetKey(KeyCode.A)||movementStick.Horizontal <-0.1)
            {
                horizontalMovement = -1f;
            }

            if (Input.GetKey(KeyCode.D)||movementStick.Horizontal >0.1)
            {
                horizontalMovement = 1f;
            }

            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
            {
                horizontalMovement = 0f;
            }
            

            Vector3 direction = new Vector3(horizontalMovement, 0, 0).normalized;
            if ((transform.position.x - 0.1 < -1.8 && horizontalMovement < 0) ||
                (transform.position.x + 0.1 > 1.8 && horizontalMovement > 0))
            {
                direction = Vector3.zero;
            }
            
            rb.velocity = direction * (Time.fixedDeltaTime * playerSpeed);
        }

        public void OnCatchRainDrop()
        {
            animator.SetTrigger("Trigger Catch Rain Drop");
        }

        public void LeftButton()
        {
            horizontalMovement = -1;
            animator.SetTrigger(triggerLeanLeft);
        }

        public void LeftButtonRelease()
        {
            horizontalMovement = 0;
            animator.SetTrigger(triggerRestoreFromLeft);
        }

        public void RightButton()
        {
            horizontalMovement = 1;
            animator.SetTrigger(triggerLeanRight);
        }

        public void RightButtonRelease()
        {
            horizontalMovement = 0;
            animator.SetTrigger(triggerRestoreFromRight);
        }

        public void EnterMovingRight()
        {
            // if (animator.GetCurrentAnimatorStateInfo(0).IsName("Lean Right"))
            // {
            //     animator.Play("Moving Right");
            // }
        }

        public void EnterMovingLeft()
        {
            // if (animator.GetCurrentAnimatorStateInfo(0).IsName("Lean Left"))
            // {
            //     animator.Play("Moving Left");
            // }
            // animator.Play("Moving Left");
        }

    }
}