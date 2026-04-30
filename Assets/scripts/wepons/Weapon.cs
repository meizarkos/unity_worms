using System;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected float power;
    public float maxPower;

    public float minAngle;
    public float maxAngle;
    public float startingAngle;

    public int dotCount = 5;

    public Transform firePoint;
    public float timeStep;

    protected float smooth = 30f;

    protected float currentAngle;

    public abstract void Fire();

    public float GetAngle()
    {
        return currentAngle;
    }

    public float GetPower()
    {
        return power;
    }

    public void AddAngle()
    {
        currentAngle += smooth * Time.deltaTime * 10;
        currentAngle = Mathf.Clamp(currentAngle , minAngle, maxAngle);
    }

    public void DecreaseAngle()
    {
        currentAngle -= smooth * Time.deltaTime * 10;
        currentAngle = Mathf.Clamp(currentAngle , minAngle, maxAngle);
    }

    public void AddPower()
    {
        power += maxPower / 20;
        power = Mathf.Clamp(power ,0.1f, maxPower);
    }

    public void DecreasePower()
    {
        power -= maxPower / 20;
        power = Mathf.Clamp(power ,0.1f, maxPower);
    }

    public void ChangeAngle()
    {
        if (Input.GetKey(KeyCode.X)) {
            currentAngle += smooth * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.C)) {
            currentAngle -= smooth * Time.deltaTime;
        }
        currentAngle = Mathf.Clamp(currentAngle , minAngle, maxAngle);
    }

    public void ChangePower()
    {
        if (Input.GetKey(KeyCode.V)) {
             power += maxPower / 20;
        }
        if (Input.GetKey(KeyCode.B)) {
            power -= maxPower / 20;
        }
        power = Mathf.Clamp(power ,0.1f, maxPower);
    }
}