using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public float MovementSpeedOfEnemies;
    public float MaxMovementSpeedOfEnemies;
    public float GoDownStep;
    public Transform boundaryLeft;
    public Transform boundaryRight;
    public Transform UFOSpawnPoint;
    public float UFOSpawnProbability;
    public GameObject UFO;
    private List<Enemy> enemies;
    private float SpeedUpMusicStep;
    private float SpeedChangeStep;

    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<Enemy>(GetComponentsInChildren<Enemy>());
        SpeedUpMusicStep = 0.4f / enemies.Count;
        SpeedChangeStep = (MaxMovementSpeedOfEnemies - MovementSpeedOfEnemies) / enemies.Count;
        foreach (Enemy enemy in enemies)
        {
            enemy.spriteRenderer.color = GetColor();
        }
        foreach (Text text in GameObject.FindGameObjectWithTag("UI").GetComponentsInChildren<Text>())
        {
            text.color = GetColor();
        }

        Invoke("CheckForUFO", 1f);
    }

    private void Update()
    {
        enemies = new List<Enemy>(GetComponentsInChildren<Enemy>());
        if(enemies.Count == 0)
        {
            GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>().SetGameWon();
        }

        foreach (Enemy enemy in enemies)
        {
            enemy.transform.position += new Vector3(MovementSpeedOfEnemies * Time.deltaTime, 0, 0);
            if (enemy.transform.position.x < boundaryLeft.position.x + 0.5f || enemy.transform.position.x > boundaryRight.position.x - + 0.5f)
            {
               ChangeDirection();
               break;
            }
        }

        
    }

    public void SpeedUpGame()
    {
        if (MovementSpeedOfEnemies < 0)
            MovementSpeedOfEnemies -= SpeedChangeStep;
        else
            MovementSpeedOfEnemies += SpeedChangeStep;
        
        Camera.main.GetComponent<AudioSource>().pitch += SpeedUpMusicStep; 
    }

    public void ChangeDirection()
    {
        MovementSpeedOfEnemies = -(MovementSpeedOfEnemies);
        transform.position += new Vector3(0, GoDownStep, 0);
        foreach (Enemy enemy in enemies)
        {
            enemy.spriteRenderer.color = GetColor();
        }

        foreach(Text text in GameObject.FindGameObjectWithTag("UI").GetComponentsInChildren<Text>())
        {
            text.color = GetColor();
        }
    }

    private void CheckForUFO()
    {
        float randomNumber = Random.Range(0f, 1f);
        if (randomNumber < UFOSpawnProbability)
        {
            Instantiate(UFO, UFOSpawnPoint.position, new Quaternion(0, 0, 0, 0));
        }
        Invoke("CheckForUFO", 1f);
    }

    private Color GetColor()
    {
        float randomNumber =  Random.Range(1,5);
        switch (randomNumber)
        {
            case 1:
                return Color.blue;
            case 2:
                return Color.red;
            case 3:
                return Color.yellow;
            case 4:
                return Color.green;
            case 5:
                return Color.magenta;
            default:
                return Color.white;
        }
    }
}
