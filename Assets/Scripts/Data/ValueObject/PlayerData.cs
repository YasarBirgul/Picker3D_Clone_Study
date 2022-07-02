using System;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class PlayerData
    {
        public PlayerMovementData PlayerMovementData;
        public PlayerThrowForceData PlayerThrowForceData;
    }
    [Serializable]
    public class PlayerMovementData
    {
        public float ForwardSpeed=5;
        public float SidewaysSpeed = 2;
        public float ForwardForceSpeed = 3;
    }
    [Serializable]
    public class PlayerThrowForceData
    {
        public Vector2 ThrowForce = new Vector2(2,2);
    }
}