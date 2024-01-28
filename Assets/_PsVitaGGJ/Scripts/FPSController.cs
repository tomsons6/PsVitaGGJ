using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour {


    [SerializeField]
    float speed = 8f;
    [SerializeField]
    float rotateSpeed = 10f;
    CharacterController controller;
    GameObject mainCamera;
    public float whyPosition = 0.83f;


    private bool isGrounded = false;
    private Vector3 playerVelocity;
    private float gravityValue = -9.81f;

    private MainGamePlayLogic mainScript;

    float xRotation = 0f;
	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
        mainCamera = Camera.main.gameObject;
        mainScript = FindObjectOfType<MainGamePlayLogic>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!mainScript.gameRunning) return;

        Movement(Inputs.Instance.GetThumbstickLeft);
        CameraLook(Inputs.Instance.GetThumbstickRight);

#if UNITY_EDITOR
        Movement(new Vector2(Input.GetAxis("Horizontal1"), Input.GetAxis("Vertical1")));
        CameraLook(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")));
#endif
    }

    void Movement(Vector2 leftStickInput)
    {
        isGrounded = controller.isGrounded;
        if(isGrounded && playerVelocity.y < 0f)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = transform.right * leftStickInput.x + transform.forward * leftStickInput.y;
        controller.Move(move * speed * Time.deltaTime);

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
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
