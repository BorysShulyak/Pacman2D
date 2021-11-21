using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;
    public Pacman pacman;
    public Transform pallets;
    public int score { get; private set; }
    public int lives { get; private set; }
    public int ghostMultiplayer { get; private set; } = 1;

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
        ResetGhostMultiplayer();
        for (int i = 0; i < this.ghosts.Length; i++) {
            this.ghosts[i].ResetState();
        }
        this.pacman.ResetState();
    }

    private void ResetGhostMultiplayer()
    {
        this.ghostMultiplayer = 1;
    }

    private void GameOver() {
        for(int i = 0; i < this.ghosts.Length; i++) {
            this.ghosts[i].gameObject.SetActive(false);
        }
        this.pacman.gameObject.SetActive(false);
    }

    public void EatGhost(Ghost ghost) {
        int points = ghost.points * this.ghostMultiplayer;
        SetScore(this.score + points);
        this.ghostMultiplayer++;
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

    public void EatPallete(Pallete pallete)
    {
        pallete.gameObject.SetActive(false);
        SetScore(this.score + pallete.points);

        if(!HasRemainingPallets())
        {
            this.pacman.gameObject.SetActive(false);
            Invoke(nameof(StartNewRound), 3.0f);
        }
    }

    public void EatPowerPallete(PowerPallete powerPallete)
    {

        EatPallete(powerPallete);
        CancelInvoke();
        Invoke(nameof(ResetGhostMultiplayer), powerPallete.effectDuration);

        // TODO: change ghost state
    }

    private bool HasRemainingPallets()
    {
        foreach (Transform pallet in this.pallets)
        {
            if(pallet.gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }
}
