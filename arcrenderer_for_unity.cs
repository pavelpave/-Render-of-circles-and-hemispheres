using UnityEngine;

[ExecuteInEditMode]
public class ArcRenderer : MonoBehaviour
{
    public enum AlignType
    {
        Center,
        Right
    }

    [Tooltip("Requred field to draw arc")][SerializeField] LineRenderer lineRenderer;
    [Tooltip("Optional filed to add edge collider")][SerializeField] EdgeCollider2D edgeCollider2D;
    [Tooltip("Location relavite to the object with LineRenderer component")]public AlignType align;
    [Range(1, 1000)] public  int polygonsCount;
    [Tooltip("Arc size in degrees")][Range(1, 360)] public float arcSize;

    private float deltaAngle;

    public void Update()
    {
        deltaAngle = arcSize * Mathf.Deg2Rad / polygonsCount;
        lineRenderer.positionCount = polygonsCount + 1;

        SetLineRenderer(polygonsCount + 1);
    }

    private void SetLineRenderer(int size)
    {
        float currentAngle = 0f;
        switch (align)
        {
            case AlignType.Center:
                currentAngle = -arcSize / 2f * Mathf.Deg2Rad;
                break;
            case AlignType.Right:
                currentAngle = 0f;
                break;
        }

        for (int i = 0; i < size; i++)
        {
            lineRenderer.SetPosition(i, new Vector3(Mathf.Sin(currentAngle), Mathf.Cos(currentAngle), 0));
            currentAngle += deltaAngle;
        }

        try
        {
            SetEdgeCollider2D();
        }
        catch { Debug.Log("U can add edge collider to this arc. To do this set field 'edgeCollider2D' "); }
    }

    private void SetEdgeCollider2D()
    {
        var newVerticies = new Vector2[lineRenderer.positionCount];
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            newVerticies[i] = lineRenderer.GetPosition(i);
        }

        edgeCollider2D.points = newVerticies;
    }
}