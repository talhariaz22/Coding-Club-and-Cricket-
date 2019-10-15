using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheelskid : MonoBehaviour
{
    [SerializeField] float intensityModifier = 1.5f;

    Skidmarks skidMarkcontroller;
    PlayerCar playerCar;

    int lastSkidId = -1;

    void Start ()
    {
        skidMarkcontroller = FindObjectOfType<Skidmarks>();
        playerCar = GetComponentInParent<PlayerCar>();
    }
    void LateUpdate ()
    {
        float intensity = playerCar.SideSlipAmount;
        if (intensity<0)
        {
            intensity = -intensity;
        }
        if (intensity > 0.2)
        {
            //show skidmarks
            lastSkidId = skidMarkcontroller.AddSkidMark(transform.position, transform.up, intensity * intensityModifier, lastSkidId);
        }
        else
        {
            //stop skidmarks
            lastSkidId = -1;
        }
    }
 
}
