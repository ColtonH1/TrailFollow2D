using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState gameState;
    private int score;

    [SerializeField] GameObject timeManager;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text lostText;
    [SerializeField] TMP_Text wonText;
    [SerializeField] TMP_Text wonScoreText;
    [SerializeField] TMP_Text instructionsText;

    //for observer; start listening to invoke method
    private void OnEnable()
    {
        BaseHero.gameOverResults += TurnOnCountdown;
        BaseHero.playerMove += UpdateScore;
        Tile.wonGame += WonGame;
    }

    //for observer; stop listening to invoke method
    private void OnDisable()
    {
        BaseHero.gameOverResults -= TurnOnCountdown;
        BaseHero.playerMove -= UpdateScore;
        Tile.wonGame -= WonGame;
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ChangeState(GameState.GenerateGrid);
        score = 0;
        scoreText.text = "Score: " + score.ToString();
        StartCoroutine(Instructions());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void ChangeState(GameState newState)
    {
        gameState = newState;

        switch (newState)
        {
            case GameState.GenerateGrid:
                GridManager.Instance.GenerateGrid();
                break;
            case GameState.SpawnHeroes:
                UnitManager.Instance.SpawnHeroes();
                break;
            case GameState.SpawnEnemies:
                UnitManager.Instance.SpawnEnemies();
                break;
            case GameState.HeroesTurn:
                break;
            case GameState.EnemiesTurn:
                //not relevant for this version
                break;
            default:
                break;
        }
    }

    //countdown to starting over since player lost
    public void TurnOnCountdown()
    {
        timeManager.SetActive(true);
        lostText.gameObject.SetActive(true);
    }

    //increase score
    public void UpdateScore(Tile tile, BaseUnit baseUnit)
    {
        score++;
        scoreText.text = "Score: " + score.ToString();
    }

    IEnumerator Instructions()
    {
        yield return new WaitForSeconds(3);
        instructionsText.gameObject.SetActive(false);
    }

    //what happens when player wins
    public void WonGame()
    {
        timeManager.SetActive(true);
        wonText.gameObject.SetActive(true);
        wonScoreText.text = "You had a score of " + score;
        wonScoreText.gameObject.SetActive(true);
    }


    public enum GameState
    {
        GenerateGrid = 0,
        SpawnHeroes = 1,
        SpawnEnemies = 2,
        HeroesTurn = 3,
        EnemiesTurn = 4
    }

}
