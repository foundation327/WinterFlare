using UnityEngine;

public class ViewmodelSway : MonoBehaviour
{
    public float swayAmount = 0.1f; // How much the viewmodel sways
    public float swaySmooth = 2.0f; // How smooth the sway is
    public float tiltAmount = 1.0f; // Amount of tilt when moving

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Start()
    {
        // Store the initial position and rotation of the viewmodel
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation;
    }

    void Update()
    {
        // Get input from the player
        float horizontal = Input.GetAxis("Mouse X");
        float vertical = Input.GetAxis("Mouse Y");

        // Calculate the sway offset based on input
        Vector3 swayOffset = new Vector3(-horizontal * swayAmount, -vertical * swayAmount, 0);
        Quaternion tiltRotation = Quaternion.Euler(-vertical * tiltAmount, horizontal * tiltAmount, 0);

        // Apply sway and tilt to the viewmodel
        transform.localPosition = Vector3.Lerp(transform.localPosition, initialPosition + swayOffset, Time.deltaTime * swaySmooth);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, initialRotation * tiltRotation, Time.deltaTime * swaySmooth);
    }
}