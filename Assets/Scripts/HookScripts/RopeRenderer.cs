using UnityEngine;

public class RopeRenderer : MonoBehaviour
{
    private LineRenderer lineRenderer;

    [SerializeField]
    private Transform startPosition;

    private float lineWidth = 0.05f;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = lineWidth;
        lineRenderer.enabled = false;
    }

    public void RenderLine(Vector3 endPosition, bool enableRenderer)
    {
        // ���ݴ��ε�������
        if(enableRenderer)
        {
            if(!lineRenderer.enabled)
            {
                lineRenderer.enabled = true;
            }

            lineRenderer.positionCount = 2;
        }
        else
        {
            lineRenderer.positionCount = 0;

            if (lineRenderer.enabled)
            {
                lineRenderer.enabled = false;
            }
        }

        // ��� lineRenderer enabled = true ����Ⱦ lineRenderer
        if(lineRenderer.enabled)
        {
            // ���� Z �����ԭ����Ϊ���� lineRenderer ���ᱻ������ס
            Vector3 temp = startPosition.position;
            temp.z = -1f;

            startPosition.position = temp;

            temp = endPosition;
            temp.z = 0;

            endPosition = temp;

            lineRenderer.SetPosition(0, startPosition.position);
            lineRenderer.SetPosition(1, endPosition);
        }
    }
}
