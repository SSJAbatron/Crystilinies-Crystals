using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowHighScore : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerController player;

    public TMP_Text finalScore;
    void Start()
    {
        AudioManager.instance.PlaySFX("HighScore");
        finalScore.text = player.scoreText.text;
    }

}
