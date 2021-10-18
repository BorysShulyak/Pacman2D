using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] ghosts;
    public GameObject pacman;
    public Transform pallets;
    public int score { get; private set; }
    public int lives { get; private set; }

    private void Start() {
        StartNewGame();
    }

    private void Update() {
        if(this.lives == 0 && Input.anyKeyDown) {
            StartNewGame();
        }
    }

    private void StartNewGame() {
        SetScore(0);
        SetLives(3);
        StartNewRound();
    }

    private void SetScore(int score) {
        this.score = score;
    }

    private void SetLives(int lives) {
        this.lives = lives;
    }

    private void StartNewRound() {
        foreach (Transform pallet in this.pallets) {
            pallet.gameObject.SetActive(true);
        }
        ResetHeroesState();
    }

    private void ResetHeroesState() {
        for(int i = 0; i < this.ghosts.Length; i++) {
            this.ghosts[i].gameObject.SetActive(true);
        }
        this.pacman.gameObject.SetActive(true);
    }

    private void GameOver() {
        for(int i = 0; i < this.ghosts.Length; i++) {
            this.ghosts[i].gameObject.SetActive(false);
        }
        this.pacman.gameObject.SetActive(false);
    }

    public void EatGhost(Ghost ghost) {
        SetScore(this.score + ghost.points);
    }

    public void EatPacman() {
        this.pacman.gameObject.SetActive(false);
        SetLives(this.lives - 1);
        if(this.lives > 0) {
            Invoke(nameof(ResetHeroesState), 3.0f);
        }
        else {
            GameOver();
        }
    }
}
