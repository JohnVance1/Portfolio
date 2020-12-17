using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Trenton Plager
/// Purpose: A class to update the position of the kitchen camera
/// Restrictions: This class shouldn't really have much beyond the TogglePosition method
/// The only reason that method is there is so that the camera can be instructed to lerp
/// to a different position with 1 button click
/// </summary>
public class KitchenCameraPositioning : MonoBehaviour
{
    #region Fields
    // The desired poistion of the camera on the left side of the kitchen
    [SerializeField]
    private Vector3 leftCameraPosition;

    // The desired position of the camera on the right side of the kitchen
    [SerializeField]
    private Vector3 rightCameraPosition;

    // The current target position of the camera
    // It should be either left or right camera position
    private Vector3 targetPosition;

    // A float value to make the camera lerp faster
    [SerializeField]
    private float lerpQuickener;
    #endregion

    #region Methods
    /// Author: Trenton Plager
    /// <summary>
    /// Purpose: Sets the default position of the camera to the left side of the kitchen
    /// Restrictions: None
    /// </summary>
    void Start()
    {
        targetPosition = leftCameraPosition;
    }

    /// Author: Trenton Plager
    /// <summary>
    /// Purpose: Lerps the camera's position from its current position to the target position
    /// According to delta time and modified by the quickening factor field
    /// Restrictions: None
    /// </summary>
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, lerpQuickener * Time.deltaTime);
    }

    /// Author: Trenton Plager
    /// <summary>
    /// Purpose: Switches the target position of the camera to the opposite target position
    /// Restrictions: None
    /// </summary>
    public void TogglePosition()
    {
        if (targetPosition == leftCameraPosition)
        {
            targetPosition = rightCameraPosition;
        }
        else
        {
            targetPosition = leftCameraPosition;
        }
    }
    #endregion
}
