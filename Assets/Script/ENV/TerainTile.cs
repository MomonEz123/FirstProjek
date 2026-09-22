using System;
using Unity.VisualScripting;
using UnityEngine;

public class TerainTile : MonoBehaviour
{

    [SerializeField] private Vector2Int tilePosition;

    private WorldScroling worldScroling;

    private void Start()
    {
        worldScroling = GetComponentInParent<WorldScroling>();

        if (worldScroling == null)
        {
            Debug.LogError(
                $"WorldScroling tidak ditemukan pada parent dari {gameObject.name}!"
            );

            return;
        }

        // Daftarkan tile ke WorldScroling
        worldScroling.Add(gameObject, tilePosition);

        // Sembunyikan tile sampai dibutuhkan
        transform.position = new Vector3(-100f, -100f, 0f);
    }

}
