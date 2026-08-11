public static class SelectedLevelStore
{
    public static bool HasSelectedLevel { get; private set; }
    public static int SelectedLevelIndex { get; private set; }

    public static void SelectLevel(int levelIndex)
    {
        SelectedLevelIndex = levelIndex;
        HasSelectedLevel = true;
    }

    public static void ClearSelection()
    {
        SelectedLevelIndex = 0;
        HasSelectedLevel = false;
    }
}