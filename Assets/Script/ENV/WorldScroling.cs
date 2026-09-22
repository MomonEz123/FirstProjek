using UnityEngine;

public class WorldScroling : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Vector2Int playerTilePosition;

    [Header("Tile")]
    [SerializeField] private float tileSize = 20f;
    [SerializeField] private int terrainTilesHorizontalCount = 3;
    [SerializeField] private int terrainTilesVerticalCount = 3;

    [Header("Field of Vision")]
    [SerializeField] private int fieldofVisionHight = 3;
    [SerializeField] private int fieldofVisionWidth = 3;

    private Vector2Int currentTilePosition = new Vector2Int(0, 0);
    private Vector2Int onTileGridPlayerPosition;
    private Vector2Int currentPlayerTilePosition;

    private GameObject[,] terrainTiles;

    private void Awake()
    {
        terrainTiles = new GameObject[
            terrainTilesHorizontalCount,
            terrainTilesVerticalCount
        ];
    }

    private void Start()
    {
        if (playerTransform == null)
        {
            Debug.LogError("Player Transform belum diatur pada WorldScroling!");
            return;
        }

        UpdatePlayerTilePosition();
        currentTilePosition = playerTilePosition;
        UpdateTilesOnScreen();
    }

    private void Update()
    {
        if (playerTransform == null)
            return;

        UpdatePlayerTilePosition();

        if (currentTilePosition != playerTilePosition)
        {
            currentTilePosition = playerTilePosition;
            currentPlayerTilePosition = playerTilePosition;

            onTileGridPlayerPosition.x =
                CalculatePositionOnAxis(playerTilePosition.x, true);

            onTileGridPlayerPosition.y =
                CalculatePositionOnAxis(playerTilePosition.y, false);

            UpdateTilesOnScreen();
        }
    }

    private void UpdatePlayerTilePosition()
    {
        playerTilePosition.x =
            Mathf.FloorToInt(playerTransform.position.x / tileSize);

        playerTilePosition.y =
            Mathf.FloorToInt(playerTransform.position.y / tileSize);
    }

    private void UpdateTilesOnScreen()
    {
        int halfWidth = fieldofVisionWidth / 2;
        int halfHeight = fieldofVisionHight / 2;

        for (int pov_x = -halfWidth; pov_x <= halfWidth; pov_x++)
        {
            for (int pov_y = -halfHeight; pov_y <= halfHeight; pov_y++)
            {
                int worldX = playerTilePosition.x + pov_x;
                int worldY = playerTilePosition.y + pov_y;

                int tileToUpdateX =
                    CalculatePositionOnAxis(worldX, true);

                int tileToUpdateY =
                    CalculatePositionOnAxis(worldY, false);

                GameObject tile =
                    terrainTiles[tileToUpdateX, tileToUpdateY];

                if (tile != null)
                {
                    tile.transform.position =
                        CalculateTilePosition(worldX, worldY);
                }
            }
        }
    }

    private Vector3 CalculateTilePosition(int x, int y)
    {
        return new Vector3(
            x * tileSize,
            y * tileSize,
            0f
        );
    }

    private int CalculatePositionOnAxis(
        int currentValue,
        bool horizontal)
    {
        int tileCount = horizontal
            ? terrainTilesHorizontalCount
            : terrainTilesVerticalCount;

        if (tileCount <= 0)
            return 0;

        int result = currentValue % tileCount;

        if (result < 0)
        {
            result += tileCount;
        }

        return result;
    }

    public void Add(
        GameObject tileGameObject,
        Vector2Int tilePosition)
    {
        if (tileGameObject == null)
            return;

        int x = CalculatePositionOnAxis(
            tilePosition.x,
            true
        );

        int y = CalculatePositionOnAxis(
            tilePosition.y,
            false
        );

        terrainTiles[x, y] = tileGameObject;
    }
}