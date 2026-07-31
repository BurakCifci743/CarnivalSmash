using UnityEngine;

public class CanGroup : MonoBehaviour
{
    private CanKnockdownDetector[] cans;

    private void Awake()
    {
        cans = GetComponentsInChildren<CanKnockdownDetector>();
    }

    public int GetKnockedDownCount()
    {
        int count = 0;

        foreach (CanKnockdownDetector can in cans)
        {
            if (can.IsKnockedDown)
            {
                count++;
            }
        }

        return count;
    }

    public int GetTotalCanCount()
    {
        return cans.Length;
    }
}