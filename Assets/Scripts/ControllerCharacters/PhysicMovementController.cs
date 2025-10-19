using UnityEngine;

[System.Serializable]
public class PhysicMovementController
{
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float reverseMultiplier = 2f;
    [SerializeField] private Transform cameraTransform;

    [SerializeField] private Rigidbody _rb;

    public void Move(Vector2 input)
    {
        if (input.magnitude < 0.1f) return;

        Vector3 moveDirection = (cameraTransform.forward * input.y + cameraTransform.right * input.x).normalized;
        Vector3 currentVelocity = _rb.linearVelocity;
        currentVelocity.y = 0f;

        float dotProduct = Vector3.Dot(currentVelocity.normalized, moveDirection);
        float speedMultiplier = 1f;

        if (dotProduct < -0.3f)
        {
            speedMultiplier = 1f + (reverseMultiplier * Mathf.Abs(dotProduct));
        }

        Vector3 force = moveDirection * (movementSpeed * speedMultiplier);
        _rb.AddForce(force, ForceMode.VelocityChange);
    }
}