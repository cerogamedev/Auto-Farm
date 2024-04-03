using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform topLeftCorner;
    public Transform bottomRightCorner;

    public float zoomSpeed = 2f;
    public float minZoom = 2f;
    public float maxZoom = 10f;

    public float moveSpeed = 5f;

    private Camera cam;
    private void Start()
    {
        cam = GetComponent<Camera>();
    }
    void LateUpdate()
    {
        // Kameranýn x ve z konumunu sýnýrlayarak güncelle
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, topLeftCorner.position.x, bottomRightCorner.position.x),
            transform.position.y,
            Mathf.Clamp(transform.position.y, bottomRightCorner.position.y, topLeftCorner.position.y)
        );
    }
    public void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f ) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);

        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        float newSize = cam.orthographicSize - scrollInput * zoomSpeed;

        // Zoom sýnýrlarýný kontrol etme
        newSize = Mathf.Clamp(newSize, minZoom, maxZoom);
        cam.orthographicSize = newSize;
    }
}
