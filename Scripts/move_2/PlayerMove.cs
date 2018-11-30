using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    [SerializeField] private string horizontalInputName= "Horizontal";
    [SerializeField] private string verticalInputName= "Vertical";
    [SerializeField] float movementSpeed= 10;

    [SerializeField] private AnimationCurve jumpFallOff;
    [SerializeField] private float jumpMultiplier=10;
    [SerializeField] private KeyCode jumpKey;

    private bool isJumping;
    private CharacterController  charControler;

    private void Awake()
    {
        charControler = GetComponent<CharacterController>();
    }

    private void Update() {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        float horizInput = Input.GetAxis(horizontalInputName);
        float vertInput = Input.GetAxis(verticalInputName);

        Vector3 forwardMovement = transform.forward * vertInput;
        Vector3 rightdMovement = transform.right * horizInput;

        charControler.SimpleMove( Vector3.ClampMagnitude(forwardMovement + rightdMovement, 1.0f) * movementSpeed);

        JumpInput();
    }

    private void JumpInput()
    {
        if(Input.GetKeyDown(jumpKey) && !isJumping)
        {
            isJumping = true;
            StartCoroutine(JumpEvent());
        }
    }

    private IEnumerator JumpEvent()
    {

        charControler.slopeLimit = 90.0f;
        float timeInAir = 0.0f;



        do
        {
            float jumpForce = jumpFallOff.Evaluate(timeInAir);
            charControler.Move(Vector3.up * jumpForce * jumpMultiplier * Time.deltaTime);
            timeInAir += Time.deltaTime;
            yield return null;
        } while (!charControler.isGrounded && charControler.collisionFlags != CollisionFlags.Above);

        charControler.slopeLimit = 45.0f;
        isJumping = false;
  
    }
}
