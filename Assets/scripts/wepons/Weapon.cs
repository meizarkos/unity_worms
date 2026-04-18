using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public float maxPower;
    public float minAngle;
    public float maxAngle;

    public int dotCount = 5;

    public float timeStep = 0.03f;
    public float disableDots = 1.5f;

    protected float smooth = 30f;
    public abstract void Fire();
}