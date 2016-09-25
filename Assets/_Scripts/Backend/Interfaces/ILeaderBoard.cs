namespace _Scripts.Backend.Interfaces
{
    public interface ILeaderBoard
    {
        int GetRank();

        void RegisterScore(int score);
    }
}
