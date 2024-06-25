using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingObject : FallingObject
{
    public float JumpForce;

    protected virtual void Jump() { GC.IsGrounded = false; VerticalVelocity = JumpForce; }
}
