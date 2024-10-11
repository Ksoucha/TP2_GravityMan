using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherry : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindAnyObjectByType<CherriesCollected>().CollectCherry();

        Player player = collision.gameObject.GetComponent<Player>();
        player.CollectCherry();
        Destroy(gameObject);
    }
}
