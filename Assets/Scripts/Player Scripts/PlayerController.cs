using Joystick_Pack.Scripts.Joysticks;
using UnityEngine;

namespace Player_Scripts
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float playerSpeed;
        [SerializeField] private FloatingJoystick movementStick;
        [SerializeField] private CharacterController characterController;

        private void Awake()
        {
        }

        private void Update()
        {
            var horizontalMovement = 0f;
            if (Input.GetKey(KeyCode.A))
            {
                horizontalMovement = -1f;
            }

            if (Input.GetKey(KeyCode.D))
            {
                horizontalMovement = 1f;
            }

            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
            {
                horizontalMovement = 0f;
            }

            if (movementStick.Horizontal != 0)
            {
                horizontalMovement = movementStick.Horizontal;
            }

            Vector3 direction = new Vector3(horizontalMovement, 0, 0).normalized;
            if ((transform.position.x - 0.1 < -3.8 && horizontalMovement < 0) ||
                (transform.position.x + 0.1 > 3.8 && horizontalMovement > 0))
            {
                direction = Vector3.zero;
            }

            characterController.Move(direction * (Time.fixedDeltaTime * playerSpeed));
        }
    }
}