public readonly struct GameResult
{
    public readonly int KnockedCount;
    public readonly int TotalCount;
    public readonly int AttemptsUsed;
    public readonly int MaxAttempts;
    public readonly int FinalScore;

    public bool IsPerfect => KnockedCount >= TotalCount;

    public GameResult(
        int knockedCount,
        int totalCount,
        int attemptsUsed,
        int maxAttempts,
        int finalScore)
    {
        KnockedCount = knockedCount;
        TotalCount = totalCount;
        AttemptsUsed = attemptsUsed;
        MaxAttempts = maxAttempts;
        FinalScore = finalScore;
    }
}