using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public float StartMovementSpeedOfEnemies;
    private float movementSpeedOfEnemies;
    public float MaxMovementSpeedOfEnemies;
    public float StartAnimationSpeedOfEnemies;
    private float animationSpeedOfEnemies;
    public float MaxAnimationSpeedOfEnemies;
    public float GoDownStep;
    public Transform BoundaryLeft;
    public Transform BoundaryRight;
    public Transform UFOSpawnPoint;
    public float UFOSpawnProbability;
    public GameObject UFO;
    private List<Enemy> enemies;
    private float speedUpMusicStep;
    private float speedChangeStep;
    private float speedUpAnimationStep;
    

    void Start()
    {
        enemies = new List<Enemy>(GetComponentsInChildren<Enemy>());

        speedUpMusicStep = 0.4f / enemies.Count;

        movementSpeedOfEnemies = StartMovementSpeedOfEnemies;
        speedChangeStep = (MaxMovementSpeedOfEnemies - StartMovementSpeedOfEnemies) / enemies.Count;

        animationSpeedOfEnemies = StartAnimationSpeedOfEnemies;
        speedUpAnimationStep = (MaxAnimationSpeedOfEnemies - StartAnimationSpeedOfEnemies) / enemies.Count;

        foreach (Enemy enemy in enemies)
        {
            enemy.spriteRenderer.color = GetColor();
            enemy.animator.speed = animationSpeedOfEnemies;
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
            enemy.transform.position += new Vector3(movementSpeedOfEnemies * Time.deltaTime, 0, 0);
            if (enemy.transform.position.x < BoundaryLeft.position.x + 0.5f || enemy.transform.position.x > BoundaryRight.position.x - + 0.5f)
            {
               ChangeDirection();
               break;
            }
        }

        
    }

    public void SpeedUpGame()
    {
        if (movementSpeedOfEnemies < 0)
            movementSpeedOfEnemies -= speedChangeStep;
        else
            movementSpeedOfEnemies += speedChangeStep;

        animationSpeedOfEnemies += speedUpAnimationStep;

        Camera.main.GetComponent<AudioSource>().pitch += speedUpMusicStep; 
    }

    public void ChangeDirection()
    {
        movementSpeedOfEnemies = -(movementSpeedOfEnemies);
        transform.position += new Vector3(0, GoDownStep, 0);

        foreach (Enemy enemy in enemies)
        {
            enemy.spriteRenderer.color = GetColor();
            enemy.transform.position += new Vector3(movementSpeedOfEnemies * Time.deltaTime, 0, 0);
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
