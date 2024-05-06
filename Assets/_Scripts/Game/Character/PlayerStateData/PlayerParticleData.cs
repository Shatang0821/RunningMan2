using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class PlayerParticleData
{
    [Header("==== PARTICLE ====")] 
    [FormerlySerializedAs("movementParticle")] public ParticleSystem MovementParticle;
    [FormerlySerializedAs("fallParticle")] public ParticleSystem FallParticle;
    [FormerlySerializedAs("jumpParticle")] public ParticleSystem JumpParticle;
    [FormerlySerializedAs("touchParticle")] public ParticleSystem TouchParticle;
}