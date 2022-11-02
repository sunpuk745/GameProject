using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfMan : Entity
{
    public G_WalkState walkState { get; private set; }
    public G_IdleState idleState { get; private set; }

    [SerializeField]private Data_MoveState walkStateData;
    [SerializeField]private Data_IdleState idleStateData;

    public override void Start()
    {
        base.Start();

        walkState = new G_WalkState(this, stateMachine, "move", walkStateData, this);
        idleState = new G_IdleState(this, stateMachine, "idle", idleStateData, this);
        
        stateMachine.Initialize(walkState);
    }
}
