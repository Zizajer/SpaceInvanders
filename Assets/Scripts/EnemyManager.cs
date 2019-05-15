using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float MovementSpeedOfEnemies;
    public float MaxMovementSpeedOfEnemies;
    public float GoDownStep;
    public Transform boundaryLeft;
    public Transform boundaryRight;
    private List<Enemy> enemies;
    private float SpeedUpMusicStep;
    private float SpeedChangeStep;

    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<Enemy>(GetComponentsInChildren<Enemy>());
        SpeedUpMusicStep = 0.4f / enemies.Count;
        SpeedChangeStep = (MaxMovementSpeedOfEnemies - MovementSpeedOfEnemies) / enemies.Count;
    }

    private void Update()
    {
        enemies = new List<Enemy>(GetComponentsInChildren<Enemy>());
        foreach (Enemy enemy in enemies)
        {
            enemy.transform.position += new Vector3(MovementSpeedOfEnemies * Time.deltaTime, 0, 0);
            if (enemy.transform.position.x < boundaryLeft.position.x || enemy.transform.position.x > boundaryRight.position.x)
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
    }
}
