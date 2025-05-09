using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "UnityChess/Scriptable Objects/Skill")]
public class Skill : ScriptableObject
{
    [SerializeField] private int damage;
    [SerializeField] private ETeam team;

    public int Damage => damage;
    public ETeam Team => team;
}
