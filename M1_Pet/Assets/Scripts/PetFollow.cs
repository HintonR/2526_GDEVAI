using UnityEngine;

public class PetFollow : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float followSpeed;
    [SerializeField] float stopDistance;
    [SerializeField] float followDelay;
    [SerializeField] float rotationSpeed;
    [SerializeField] float acceleration;   

    float followTimer;

    void Update()
    {
        Vector3 toPlayer = player.position - transform.position;

        if (toPlayer.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(toPlayer.normalized, Vector3.up);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime);
        }

        float distance = toPlayer.magnitude;

        if (distance > stopDistance)
        {
            followTimer += Time.deltaTime;

            if (followTimer >= followDelay)
            {
                transform.position = Vector3.Lerp(
                    transform.position, 
                    player.position, 
                    acceleration * Time.deltaTime);
            }
        }
        else
        {
            followTimer = 0f;
        }
    }
}