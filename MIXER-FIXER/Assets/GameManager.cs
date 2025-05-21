using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject bucketHolder;
    public int currentBucket;
    public Transform centerPoint;

    public float moveSpeed = 5f;

    private Vector3 targetPosition;

    void Start()
    {
        targetPosition = bucketHolder.transform.position;
    }

    void Update()
    {
        bucketHolder.transform.position = Vector3.Lerp(
            bucketHolder.transform.position,
            targetPosition,
            Time.deltaTime * moveSpeed
        );
    }

    public void MoveBuckets()
    {
        // Get selected bucket
        Transform selectedBucket = bucketHolder.transform.GetChild(currentBucket);

        // World space difference between centerPoint and selectedBucket
        Vector3 worldOffset = centerPoint.position - selectedBucket.position;

        // Move the whole holder so that selected bucket aligns with centerPoint
        targetPosition = bucketHolder.transform.position + worldOffset;
    }
}
