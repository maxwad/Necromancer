using UnityEngine;

public class GlobalCamera : MonoBehaviour
{
    private GameObject globalPlayer;

    public float minZOffset = -250;
    public float maxZOffset = -45;
    private float zoom = 1f;

    public float moveSpeedMin = 300f;
    public float moveSpeedMax = 50f;

    public float rotationSpeed = 180f;
    private float rotationAngle;

    [SerializeField] private Transform swivel;
    [SerializeField] private Transform stick;

    [SerializeField] private HexGrid hexGrid;

    private void Update()
    {
        if(MenuManager.isGamePaused != true && MenuManager.isMiniPause != true)
        {
            float zoomDelta = Input.GetAxis("Mouse ScrollWheel");
            if(zoomDelta != 0f) ChangeZoom(zoomDelta);

            float deltaRotation = Input.GetAxisRaw("Rotation");
            if(deltaRotation != 0f) ChangeRotation(deltaRotation);

            float deltaX = Input.GetAxisRaw("Horizontal");
            float deltaY = Input.GetAxisRaw("Vertical");
            if(deltaX != 0 || deltaY != 0) ChangePosition(deltaX, deltaY);
        }
    }

    private void ChangeRotation(float deltaRotation)
    {
        rotationAngle += deltaRotation * rotationSpeed * Time.deltaTime;

        if(rotationAngle < 0f) rotationAngle += 360f;
        else if(rotationAngle >= 360) rotationAngle -= 360;

        transform.localRotation = Quaternion.Euler(0f, 0f, rotationAngle);
    }

    private void ChangePosition(float deltaX, float deltaY)
    {
        float distance = Mathf.Lerp(moveSpeedMin, moveSpeedMax, zoom) * Time.deltaTime;
        Vector2 direction = transform.localRotation * new Vector3(deltaX, deltaY, 0).normalized;

        Vector2 position = transform.localPosition;
        position += direction * distance;
        transform.localPosition = position;

        transform.localPosition = ClampPosition(position);
    }

    private Vector3 ClampPosition(Vector2 position)
    {
        float maxX = (hexGrid.partCountX * HexMetrics.partSizeX - 0.5f) * (2f * HexMetrics.innerRadius);
        position.x = Mathf.Clamp(position.x, 0f, maxX);
         
        float maxY = (hexGrid.partCountY * HexMetrics.partSizeY - 1f) * (1.5f * HexMetrics.outerRadius);
        position.y = Mathf.Clamp(position.y, 0f, maxY);

        return position;
    }

    private void ChangeZoom(float zoomDelta)
    {
        zoom = Mathf.Clamp01(zoom + zoomDelta);

        float distance = Mathf.Lerp(minZOffset, maxZOffset, zoom);
        stick.localPosition = new Vector3(0f, 0f, distance);
    }

    public void SetGlobalCamera(Vector3 startPosition, Vector3 startRotation, GameObject player)
    {
        globalPlayer = player;
        
        Camera.main.orthographic = false;
        if(startPosition != Vector3.zero) transform.position = startPosition;
        if(startRotation != Vector3.zero) transform.eulerAngles = startRotation;

    }


}
