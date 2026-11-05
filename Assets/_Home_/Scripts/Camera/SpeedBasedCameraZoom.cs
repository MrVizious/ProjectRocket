using Unity.Cinemachine;
using UnityEngine;
using UtilityMethods;

public class SpeedBasedCameraZoom : MonoBehaviour
{
    public CinemachineCamera cam;
    public Rigidbody2D rb;

    public float minSpeed, maxSpeed;
    public float minOrthographicSize, maxOrthographicSize;
    public float cameraAdjustSpeed = 1f;


    private void Update()
    {
        float targetOrthographicSize = Math.Remap(rb.linearVelocity.magnitude, minSpeed, maxSpeed, minOrthographicSize, maxOrthographicSize, true);
        cam.Lens.OrthographicSize = Mathf.MoveTowards(cam.Lens.OrthographicSize, targetOrthographicSize, Time.deltaTime * cameraAdjustSpeed);
    }
}
