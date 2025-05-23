using UnityEngine;
using UnityEngine.EventSystems;

public class BucketClick : MonoBehaviour, IPointerClickHandler
{
    private GameManager gameManager;
    public int bucketIndex;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();


        // Make sure the child is off when game starts
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

        // If machine is empty, turn off the child (slush) after bucket is clicked
        if (gameManager.machineEmpty && transform.childCount > 0)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            gameManager.panelSr.sprite = gameManager.panelSprites[bucketIndex];

            if(bucketIndex == 6)
            {
                gameManager.sortedComplete = true;
                gameManager.showCheck();
            }
        }
    }
}
