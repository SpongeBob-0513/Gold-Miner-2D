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
        // 根据传参调整参数
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

        // 如果 lineRenderer enabled = true 则渲染 lineRenderer
        if(lineRenderer.enabled)
        {
            // 设置 Z 坐标的原因是为了让 lineRenderer 不会被背景挡住
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
