using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalCollector : MonoBehaviour
{
    int crystalPoint = 1; 
    public float timeValue = 2.0f; // time added to timer
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.AddScore(crystalPoint);
            player.AddTime(timeValue);
            // play sound
            AudioManager.instance.PlaySFX("CrystalCollect");
            Destroy(gameObject,0.1f);
        }
    }
}
