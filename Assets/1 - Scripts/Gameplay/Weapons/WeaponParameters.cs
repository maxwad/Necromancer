using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StructManager;

public class WeaponParameters : MonoBehaviour
{
    public float speed;
    public float size;
    public bool flip;
    public float physicDamage;
    public float magicDamage;
    public float lifeTime = 0;

    public void SetParameters(WeaponSettings settings)
    {
        if(settings.speed != 0) speed = settings.speed;

        if(settings.size != 0) size = settings.size;

        if(settings.physicDamage != 0) physicDamage = settings.physicDamage;

        if(settings.magicDamage != 0) magicDamage = settings.magicDamage;

        if(settings.lifeTime != 0) lifeTime = settings.lifeTime;

        flip = settings.flip;
    }

}
