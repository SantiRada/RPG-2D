using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    [Header("Data")]
    public int value;

    [Header("Calls")]
    private CoinsManager manager;

    private void Start()
    {
        manager = FindObjectOfType<CoinsManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            manager.AddMoney(value);
            Destroy(gameObject);
        }
    }
}
