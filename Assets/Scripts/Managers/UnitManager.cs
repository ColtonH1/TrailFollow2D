using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;

    private List<ScriptableUnit> units;

    public BaseHero selectedHero;

    [SerializeField] private int stopsMovementLayerNum;

    //make singleton
    private void Awake()
    {
        Instance = this;

        units = Resources.LoadAll<ScriptableUnit>("Units").ToList();
    }

    public void SpawnHeroes()
    {
        var heroCount = 1;

        //for every hero there is, spawn them
        for(int i = 0; i < heroCount; i++)
        {
            var randomPrefab = GetRandomUnit<BaseHero>(Faction.Hero);
            var spawnedHero = Instantiate(randomPrefab);
            var randomSpawnTile = GridManager.Instance.GetHeroSpawnTile();

            //randomSpawnTile.SetUnit(spawnedHero);
            spawnedHero.OccupiedTile = randomSpawnTile;
            randomSpawnTile.occupiedUnit = spawnedHero;
            spawnedHero.transform.position = randomSpawnTile.transform.position;
            randomSpawnTile.GetComponent<SpriteRenderer>().color = Color.red;
            randomSpawnTile.gameObject.layer = stopsMovementLayerNum;
        }

        //update the game state to do the next task, which is to spawn enemies
        GameManager.Instance.ChangeState(GameManager.GameState.SpawnEnemies);
    }

    public void SpawnEnemies()
    {
        var enemyCount = 1;

        //for every enemy there is, spawn them
        for (int i = 0; i < enemyCount; i++)
        {
            var randomPrefab = GetRandomUnit<BaseEnemy>(Faction.Enemy);
            var spawnedEnemy = Instantiate(randomPrefab);
            var randomSpawnTile = GridManager.Instance.GetEnemySpawnTile();

            //randomSpawnTile.SetUnit(spawnedEnemy);
            spawnedEnemy.OccupiedTile = randomSpawnTile;
            randomSpawnTile.occupiedUnit = spawnedEnemy;
            spawnedEnemy.transform.position = randomSpawnTile.transform.position;
        }

        //update the game state to do the next task, which is to let the player take their turn
        GameManager.Instance.ChangeState(GameManager.GameState.HeroesTurn);
    }

    //get a random hero
    private T GetRandomUnit<T>(Faction faction) where T : BaseUnit
    {
        return (T)units.Where(u => u.faction == faction).OrderBy(o=>Random.value).First().UnitPrefab;
    }

    public void SetSelectedHero(BaseHero hero)
    {
        selectedHero = hero; //set the selected hero equal to the one found on the selected square
        //MenuManager.Instance.ShowSelectedHero(hero); //display the name of the selected hero
    }
}
