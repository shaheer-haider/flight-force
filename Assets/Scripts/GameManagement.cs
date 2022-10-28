using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagement : MonoBehaviour
{
    public TextMesh scoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void IncreaseScore(int score)
    {
        scoreText.text = "Score: " + score;
    }
}
