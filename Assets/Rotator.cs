////////////////////////////////////////////////////////////
// File:                 <Rotator.cs>
// Author:               <Jack Peedle>
// Date Created:         <24/03/2021>
// Brief:                <File responsible for rotating the planets>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <24/03/2021>
// Last Edit Brief:      <Planets rotating>
////////////////////////////////////////////////////////////

using UnityEngine;

public class Rotator : MonoBehaviour
{
    // Rotation speed
    [SerializeField] float rotationSpeed = 50f;

    // Bool for if the player is dragging a planet
    bool dragging = false;

    // Reference to rigidbody
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        // get the rigidbody component
        rb = GetComponent<Rigidbody>();
    }

    // On mouse drag
    void OnMouseDrag() {

        // Set dragging to true
        dragging = true;

    }

    // Update is called once per frame
    void Update()
    {
        // If the player releases the left mouse button
        if (Input.GetMouseButtonUp(0)) {

            // Set dragging to false
            dragging = false;
        }
        


    }

    // Fixed update for physics
    void FixedUpdate() {
        
        // If dragging is true
        if (dragging) {

            // x float = mouse x's axis * rotation speed * time
            float x = Input.GetAxis("Mouse X") * rotationSpeed * Time.fixedDeltaTime;

            // y float = mouse y's axis * rotation speed * time
            float y = Input.GetAxis("Mouse Y") * rotationSpeed * Time.fixedDeltaTime;

            // add torque to rigidbody for down
            rb.AddRelativeTorque(Vector3.down * x);

            // add torque to rigidbody for right
            rb.AddRelativeTorque(Vector3.right * y);

        }

    }

}
