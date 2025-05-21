using UnityEngine;
using UnityEngine.EventSystems;

public class BucketClick : MonoBehaviour, IPointerClickHandler
{
    private GameManager gameManager;
    public int bucketIndex;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Bucket clicked!");
        gameManager.currentBucket = bucketIndex;
        gameManager.MoveBuckets();
    }
}
