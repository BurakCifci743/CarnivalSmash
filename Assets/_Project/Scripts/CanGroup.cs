using System.Text;
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

    public string GetStandingCanReport()
    {
        StringBuilder builder = new StringBuilder();

        foreach (CanKnockdownDetector can in cans)
        {
            if (!can.IsKnockedDown)
            {
                builder.AppendLine($"{can.name} | Angle: {can.CurrentAngleFromUp:F1}");
            }
        }

        return builder.ToString();
    }
}