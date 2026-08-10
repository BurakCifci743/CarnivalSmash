using System.Text;
using UnityEngine;

public class CanGroup : MonoBehaviour
{
    private CanKnockdownDetector[] cans;

    private void Awake()
    {
        RefreshCans();
    }

    public void RefreshCans()
    {
        cans = GetComponentsInChildren<CanKnockdownDetector>(true);
    }

    public int GetKnockedDownCount()
    {
        if (cans == null)
        {
            RefreshCans();
        }

        int knockedDownCount = 0;

        for (int i = 0; i < cans.Length; i++)
        {
            if (cans[i] != null && cans[i].IsKnockedDown)
            {
                knockedDownCount++;
            }
        }

        return knockedDownCount;
    }

    public int GetTotalCanCount()
    {
        if (cans == null)
        {
            RefreshCans();
        }

        return cans.Length;
    }

    public string GetStandingCanReport()
    {
        if (cans == null)
        {
            RefreshCans();
        }

        StringBuilder report = new StringBuilder();

        for (int i = 0; i < cans.Length; i++)
        {
            if (cans[i] == null) continue;

            if (!cans[i].IsKnockedDown)
            {
                report.AppendLine(
                    $"{cans[i].name} | Angle: {cans[i].CurrentAngleFromUp:F1}"
                );
            }
        }

        return report.ToString();
    }
}