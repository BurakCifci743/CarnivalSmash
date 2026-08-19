public readonly struct GameResult
{
    public int KnockedCount { get; }
    public int TotalCount { get; }
    public int AttemptsUsed { get; }
    public int MaxAttempts { get; }
    public int FinalScore { get; }
    public bool IsSuccess { get; }

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

        IsSuccess = knockedCount >= totalCount;
    }
}