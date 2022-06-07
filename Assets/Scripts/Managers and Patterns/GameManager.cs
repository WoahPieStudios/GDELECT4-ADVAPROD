using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private int totemsToKill, dronesToKill, tanksToKill;
    public int TotemsToKill => totemsToKill;
    public int DronesToKill => dronesToKill;
    public int TanksToKill => tanksToKill;
}
