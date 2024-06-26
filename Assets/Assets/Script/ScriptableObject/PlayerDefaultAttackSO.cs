using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDefaultAttackSO", menuName = "TopDownController/Attacks/PlayerAttack", order = 2)]
public class PlayerDefaultAttackSO : AttackSo
{
    [Header("Ranged Attack Info")]
    public string NameTag;
}