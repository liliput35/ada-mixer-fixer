using UnityEngine;
using UnityEngine.EventSystems;

public class MachineClick : MonoBehaviour, IPointerClickHandler
{
    private GameManager gameManager;

    public GameObject[] flowPrefabs;     // Assign 7 prefabs in Inspector
    public Transform spawnPoint;         // Optional: where to spawn the effect
    public int[] indexSequence = { 2, 1, 5, 6, 0, 4, 3 }; // Custom bucket order

    private int currentSequenceIndex = 0;
    private GameObject currentFlowInstance;
    private bool isFlowing;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isFlowing)
        {
            Debug.Log("Machine clicked!");

            int bucketIndex = indexSequence[currentSequenceIndex];

            if (bucketIndex >= 0 && bucketIndex < flowPrefabs.Length)
            {
                currentFlowInstance = Instantiate(
                    flowPrefabs[bucketIndex],
                    spawnPoint != null ? spawnPoint.position : transform.position,
                    Quaternion.identity
                );

                isFlowing = true;

                //  Activate child of corresponding bucket
                Transform bucketTransform = gameManager.bucketHolder.transform.GetChild(bucketIndex);
                if (bucketTransform.childCount > 0)
                {
                    bucketTransform.GetChild(0).gameObject.SetActive(true);
                }
            }
            else
            {
                Debug.LogWarning($"Invalid bucketIndex: {bucketIndex}");
            }
        }
        else
        {
            if (currentFlowInstance != null)
            {
                Destroy(currentFlowInstance);
            }

            currentSequenceIndex = (currentSequenceIndex + 1) % indexSequence.Length;
            isFlowing = false;
        }
    }

}
