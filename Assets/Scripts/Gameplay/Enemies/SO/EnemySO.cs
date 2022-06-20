using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NameManager;

[CreateAssetMenu(fileName = "EnemyData", menuName = "EnemyItem")]

public class EnemySO : ScriptableObject
{
    public string unitName;
    public EnemiesTypes EnemiesType;
    public GameObject unitGO;
    public Sprite unitIcon;

    //battle parameters
    public float health;
    public float physicAttack;
    public float physicDefence;
    public float magicAttack;
    public float magicDefence;
    public float speedAttack;
    public float size;

    //cost parameters
    public int coinsPrice;
    public int foodPrice;
    public int woodPrice;
    public int ironPrice;
    public int stonePrice;
    public int magicPrice;
}
