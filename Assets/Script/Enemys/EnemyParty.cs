using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyParty : MonoBehaviour
{
    [SerializeField] List<Enemy> enemys;

    private void Start()
    {
        foreach(var enemy in enemys)
        {
            enemy.Init();
        }
    }

    public Enemy GetHealthyEnemy()
    {
        return enemys.Where(x => x.HP > 0).FirstOrDefault();
    }

}
