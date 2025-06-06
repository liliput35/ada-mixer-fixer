using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class MachineClick : MonoBehaviour, IPointerClickHandler
{
    private GameManager gameManager;

    public GameObject[] flowPrefabs;     // Assign 7 prefabs in Inspector
    public Transform spawnPoint;         // Optional: where to spawn the effect
    public int[] indexSequence = { 2, 1, 5, 6, 0, 4, 3 }; // Custom bucket order

    public float activationDelay = 1.0f;
    public Sprite finalSprite; // ✅ Assign this in the Inspector

    private int currentSequenceIndex = 0;
    private GameObject currentFlowInstance;
    private bool isFlowing;

    public GameObject panel;
    public Sprite emptyPanel;

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

                Transform bucketTransform = gameManager.bucketHolder.transform.GetChild(bucketIndex);
                if (bucketTransform.childCount > 0)
                {
                    StartCoroutine(ActivateChildAfterDelay(bucketTransform.GetChild(0).gameObject));
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

            // Move to next step in sequence
            currentSequenceIndex++;

            //Check if sequence is finished
            if (currentSequenceIndex >= indexSequence.Length)
            {
                currentSequenceIndex = 0;

                //Change the sprite
                SpriteRenderer sr = GetComponent<SpriteRenderer>();
                if (sr != null && finalSprite != null)
                {
                    sr.sprite = finalSprite;
                    panel.GetComponent<SpriteRenderer>().sprite = emptyPanel;
                    gameManager.machineEmpty = true;
                }
            }

            isFlowing = false;
        }
    }

    private IEnumerator ActivateChildAfterDelay(GameObject child)
    {
        yield return new WaitForSeconds(activationDelay);
        child.SetActive(true);
    }
}
