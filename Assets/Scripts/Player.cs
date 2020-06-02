using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Header("General")]
    [Tooltip("In ms^-1")][SerializeField] float controlSpeed = 35f;
    [Tooltip("In m")] [SerializeField] float xRange = 17f;
    [Tooltip("In m")] [SerializeField] float yRange = 12f;
    [SerializeField] GameObject[] guns;

    [Header("Screen-position Based")]
    [SerializeField] float positionPitchFactor = -1.5f;
    [SerializeField] float positionYawFactor = 1.5f;

    [Header("Control-throw Based")]
    [SerializeField] float controlPitchFactor = -20f;
    [SerializeField] float controlYawFactor = 20f;

    float xThrow;
    float yThrow;
    bool isAlive = true;

    void OnDeath()
    {
        isAlive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            ProcessTranslation();
            ProcessRotation();
            ProcessFire();
        }
    }

    void ProcessFire()
    {
        if (CrossPlatformInputManager.GetButton("Fire"))
        {
            SetGunsActive(true);
        }
        else
        {
            SetGunsActive(false);
        }
    }

    private void SetGunsActive(bool isActive)
    {
        foreach (GameObject gun in guns)
        {
            var emisiionModule = gun.GetComponent<ParticleSystem>().emission;
            emisiionModule.enabled = isActive;
        }
    }
   

    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrow * controlSpeed * Time.deltaTime;
        float yOffset = yThrow * controlSpeed * Time.deltaTime;

        float rawNewXpos = transform.localPosition.x + xOffset;
        float clampXpos = Mathf.Clamp(rawNewXpos, -1 * xRange, xRange);

        float rawNewYpos = transform.localPosition.y + yOffset;
        float clampYpos = Mathf.Clamp(rawNewYpos, -1 * yRange, yRange);

        transform.localPosition = new Vector3(clampXpos, clampYpos, transform.localPosition.z);
    }

    private void ProcessRotation()
    {
        float pitch = transform.localPosition.y * positionPitchFactor + yThrow * controlPitchFactor;
        //print(transform.localRotation.y);
        float yaw = transform.localPosition.x * positionYawFactor + xThrow * controlYawFactor;
        float roll = 0f;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }
}
