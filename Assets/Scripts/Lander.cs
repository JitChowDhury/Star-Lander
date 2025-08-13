using UnityEngine;

public class Lander : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Debug.Log("Up");
        }
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("Up");
        }
        if (Input.GetKey(KeyCode.S))
        {
            Debug.Log("Up");
        }
        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("Up");
        }
    }
}
