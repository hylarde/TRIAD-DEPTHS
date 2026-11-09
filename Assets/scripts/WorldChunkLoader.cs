using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldChunkLoader : MonoBehaviour
{
  [Header("Referências")]
    public Transform player;
    public GameObject chunkPrefab;

    [Header("Configuração")]
    public float chunkSize = 20f;
    public int visibleRadius = 1;

    private Dictionary<Vector2Int, GameObject> loadedChunks =
        new Dictionary<Vector2Int, GameObject>();

    private Vector2Int currentPlayerChunk;

    private void Start()
    {
        UpdateChunks(true);
    }

    private void Update()
    {
        Vector2Int newPlayerChunk = GetPlayerChunk();

        if (newPlayerChunk != currentPlayerChunk)
        {
            currentPlayerChunk = newPlayerChunk;
            UpdateChunks(false);
        }
    }

    private Vector2Int GetPlayerChunk()
    {
        int x = Mathf.FloorToInt(player.position.x / chunkSize);
        int z = Mathf.FloorToInt(player.position.z / chunkSize);

        return new Vector2Int(x, z);
    }

    private void UpdateChunks(bool firstLoad)
    {
        Vector2Int playerChunk = GetPlayerChunk();
        currentPlayerChunk = playerChunk;

        HashSet<Vector2Int> chunksNecessarios =
            new HashSet<Vector2Int>();

        // Carrega os chunks ao redor do jogador.
        // Com visibleRadius = 1, cria as 8 direções + o centro.
        for (int x = -visibleRadius; x <= visibleRadius; x++)
        {
            for (int z = -visibleRadius; z <= visibleRadius; z++)
            {
                Vector2Int chunkCoord =
                    new Vector2Int(playerChunk.x + x, playerChunk.y + z);

                chunksNecessarios.Add(chunkCoord);

                if (!loadedChunks.ContainsKey(chunkCoord))
                {
                    LoadChunk(chunkCoord);
                }
            }
        }

        // Remove chunks que ficaram muito distantes.
        List<Vector2Int> chunksParaRemover =
            new List<Vector2Int>();

        foreach (Vector2Int chunkCoord in loadedChunks.Keys)
        {
            if (!chunksNecessarios.Contains(chunkCoord))
            {
                chunksParaRemover.Add(chunkCoord);
            }
        }

        foreach (Vector2Int chunkCoord in chunksParaRemover)
        {
            UnloadChunk(chunkCoord);
        }
    }

    private void LoadChunk(Vector2Int coordinate)
    {
        Vector3 position = new Vector3(
            coordinate.x * chunkSize,
            0f,
            coordinate.y * chunkSize
        );

        GameObject newChunk =
            Instantiate(chunkPrefab, position, Quaternion.identity);

        newChunk.name =
            "Chunk_" + coordinate.x + "_" + coordinate.y;

        loadedChunks.Add(coordinate, newChunk);
    }

    private void UnloadChunk(Vector2Int coordinate)
    {
        if (loadedChunks.TryGetValue(coordinate, out GameObject chunk))
        {
            Destroy(chunk);
            loadedChunks.Remove(coordinate);
        }
    }
}
