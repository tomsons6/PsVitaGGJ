using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour {


    [SerializeField]
    float speed = 12f;
    [SerializeField]
    float rotateSpeed = 10f;
    CharacterController controller;
    GameObject mainCamera;

    float xRotation = 0f;
	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
        mainCamera = Camera.main.gameObject;
        Cursor.lockState = CursorLockMode.Locked;
    }
	
	// Update is called once per frame
	void Update () {
        Movement(Inputs.Instance.GetThumbstickLeft);
        CameraLook(Inputs.Instance.GetThumbstickRight);

#if UNITY_EDITOR
        Movement(new Vector2(Input.GetAxis("Horizontal1"), Input.GetAxis("Vertical1")));
        CameraLook(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")));
#endif
    }

    void Movement(Vector2 leftStickInput)
    {
        Vector3 move = transform.right * leftStickInput.x + transform.forward * leftStickInput.y;
        controller.Move(move * speed * Time.deltaTime);
    }
    void CameraLook(Vector2 rightStickInput)
    {
        float xAxis = rightStickInput.x * rotateSpeed * Time.deltaTime;
        float yAxis = rightStickInput.y * rotateSpeed * Time.deltaTime;

        xRotation -= yAxis;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        mainCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * xAxis);
    }
}
