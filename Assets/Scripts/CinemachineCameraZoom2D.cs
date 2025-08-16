using Unity.Cinemachine;
using UnityEngine;

public class CinemachineCameraZoom2D : MonoBehaviour
{
    private const float NORMAL_ORHTOGRAPHICSIZE = 10f;
    public static CinemachineCameraZoom2D Instance { get; private set; }


    [SerializeField] private CinemachineCamera cinemachineCamera;
    private float targetOrthographicSize = 10f;
    void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        cinemachineCamera.Lens.OrthographicSize = targetOrthographicSize;
    }

    public void SetTargetOrthographicSize(float targetOrthographicSize)
    {
        float zoomSpeed = 2f;
        this.targetOrthographicSize = Mathf.Lerp(cinemachineCamera.Lens.OrthographicSize, targetOrthographicSize, Time.deltaTime * zoomSpeed);
    }
    public void SetNormalOrthographicSize()
    {
        SetTargetOrthographicSize(NORMAL_ORHTOGRAPHICSIZE);
    }
}
