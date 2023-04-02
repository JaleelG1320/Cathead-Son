
using UnityEngine;

public abstract class EnemyBaseState
{
    public abstract void EnterState(FieldOfViewScript enemy);
    public abstract void UpdateState(FieldOfViewScript enemy);
    public abstract void ExitState(FieldOfViewScript enemy);
    public abstract void HandleSight(FieldOfViewScript enemy);
    public abstract void HandleAudio(FieldOfViewScript enemy);
}
