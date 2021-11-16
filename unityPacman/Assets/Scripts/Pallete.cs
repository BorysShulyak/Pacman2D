using UnityEngine;

public class Pallete : MonoBehaviour
{
    public int points = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            Eat();
        }
    }

    protected virtual void Eat()
    {
        FindObjectOfType<GameManager>().EatPallete(this);
    }
}
