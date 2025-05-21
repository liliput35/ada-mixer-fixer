using UnityEngine;
using UnityEngine.EventSystems;

public class BucketClick : MonoBehaviour, IPointerClickHandler
{
    private GameManager gameManager;
    public int bucketIndex;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        if (transform.childCount > 0)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Bucket clicked!");
        gameManager.currentBucket = bucketIndex;
        gameManager.MoveBuckets();
    }
}
