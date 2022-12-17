using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorProxy : MonoBehaviour
{
    [SerializeField] private BasicAttackingAI attackingAI;
    void AttackFinished() { attackingAI.AttackFinished(); }
    void AttackClimax() { attackingAI.AttackClimax(); }

}
