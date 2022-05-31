using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public float startingTime;
    public float timeLeft;
    [SerializeField] TMP_Text timeText;

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = startingTime;
        timeText.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;

        timeText.text = "Restarting\n" + Mathf.Clamp(Mathf.Round(timeLeft), 0, 3);

        if(timeLeft <= 0)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}
