using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private float playerSpeed = 1f;
    [SerializeField] private GameInput gameInput;
    private float playerRadius = .6f;
    private float playerHeight = 2f;

    private bool isWalking;
    private void Update() {
        Vector2 inputVector = gameInput.GetMovementVector();

        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveSpeedDelta = playerSpeed * Time.deltaTime;
        bool isColliding = Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirection, moveSpeedDelta);

        isWalking = moveDirection != Vector3.zero;

        /*  Rotation    */
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);

        if (isColliding) {
            Vector3 moveDirectionX = new Vector3(moveDirection.x, 0f, 0f);
            isColliding = Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionX, moveSpeedDelta);
            if (!isColliding) {
                moveDirection = moveDirectionX;
            }
            else {
                Vector3 moveDirectionZ = new Vector3(0f, 0f, moveDirection.z);
                isColliding = Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionX, moveSpeedDelta);
                if (!isColliding) {
                    moveDirection = moveDirectionZ;
                }
            }
        }

        if (!isColliding) {
            transform.position += moveDirection * moveSpeedDelta;
        }

        // isWalking = moveDirection != Vector3.zero;

        /*  Rotation    */
        // вынеси это выше логики ходения
      /*  float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);*/
    }

    public bool IsWalking() { return isWalking; }
}
