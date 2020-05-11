using UnityEngine;

[ExecuteInEditMode]
public class CircleGroup : MonoBehaviour
{
    public float circleSize;
    public bool childsLookAtParent;

    private float deltaAngle;

    private void Update()
    {
        deltaAngle = 360 * Mathf.Deg2Rad / transform.childCount;

        SetChildsPos(transform.childCount);
    }

    private void SetChildsPos(int size)
    {
        float currentAngle = 0f;

        for (int i = 0; i < size; i++)
        {
            transform.GetChild(i).transform.position = transform.position + new Vector3(Mathf.Sin(currentAngle), 0, Mathf.Cos(currentAngle)) * circleSize;
            if(childsLookAtParent)transform.GetChild(i).eulerAngles = new Vector3(0, currentAngle * Mathf.Rad2Deg, 0);

            currentAngle += deltaAngle;
        }
    }
}