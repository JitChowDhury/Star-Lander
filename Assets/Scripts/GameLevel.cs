using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class GameLevel : MonoBehaviour
{
    [SerializeField] private int levelNumber;
    [SerializeField] private Transform landerStartPosition;
    [SerializeField] private Transform cameraStartTargetTransform;
    [SerializeField] private float zoomedOutOrthographicSize;

    public int GetLevelNumber()
    {
        return levelNumber;
    }
    // ---------------------------------------------------------
    public Vector3 GetLanderStartPosition()
    {
        return landerStartPosition.position;
    }

    public Transform GertCameraStartTargetTrasform()
    {
        return cameraStartTargetTransform;
    }

    public float GetZoomedOutOrthographicSize()
    {
        return zoomedOutOrthographicSize;
    }

}
