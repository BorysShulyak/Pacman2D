
using UnityEngine;

public class PowerPallete : Pallete
{
    public float effectDuration = 8.0f;

    protected override void Eat()
    {
        FindObjectOfType<GameManager>().EatPowerPallete(this);
    }
}
