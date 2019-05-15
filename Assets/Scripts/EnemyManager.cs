using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float MovementSpeedOfEnemies;
    public float SpeedChangeStep;
    public float GoDownStep;
    private List<Enemy> enemies;

    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<Enemy>(GetComponentsInChildren<Enemy>());
        foreach(Enemy enemy in enemies)
        {
            enemy.MovementSpeed = MovementSpeedOfEnemies;
        }
    }

    private void Update()
    {
        enemies = new List<Enemy>(GetComponentsInChildren<Enemy>());
    }

    public void SpeedUpGame()
    {
        MovementSpeedOfEnemies = -(MovementSpeedOfEnemies);
        if (MovementSpeedOfEnemies < 0)
            MovementSpeedOfEnemies -= SpeedChangeStep;
        else
            MovementSpeedOfEnemies += SpeedChangeStep;

        foreach (Enemy enemy in enemies)
        {
            enemy.MovementSpeed = MovementSpeedOfEnemies;
            enemy.GoDown(GoDownStep);
        }
    }


}
