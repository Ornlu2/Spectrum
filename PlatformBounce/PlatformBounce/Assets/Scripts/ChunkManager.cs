using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    /* ChunkManager is responsible for holding references to chunks and instantiating new ones */

    public List<GameObject> chunkSourceList;
    private Queue<GameObject> placedChunksQueue;

    private float chunkDistance = 40f;
    private float currentPlacementPosition = 0f;
    private int numberOfChunksAhead = 50;

    public static ChunkManager instance;
    private void Awake()
    {
        instance = this;
        placedChunksQueue = new Queue<GameObject>();
    }

    private void Start()
    {
        for (int i=0; i < chunkSourceList.Count; i++)
        {
            chunkSourceList[i].SetActive(false);
        }

        for (int i=0; i < numberOfChunksAhead; i++)
        {
            PlaceNextChunk();
        }
    }

    [ContextMenu("Place Chunk")]
    public void PlaceNextChunk()
    {
        GameObject nextChunkSource = chunkSourceList[Random.Range(0, chunkSourceList.Count)];
        GameObject nextChunk = Instantiate(nextChunkSource, new Vector3(0f, currentPlacementPosition-40f,0f ), Quaternion.identity);
        nextChunk.SetActive(true);

        placedChunksQueue.Enqueue(nextChunk);

        if (placedChunksQueue.Count > numberOfChunksAhead + 7)
        {
            GameObject oldestChunk = placedChunksQueue.Dequeue();
            
            
        }
        
        currentPlacementPosition -= chunkDistance;
    }
   
}
