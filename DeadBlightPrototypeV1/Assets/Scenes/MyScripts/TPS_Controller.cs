using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;

public class TPS_Controller : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private GameObject reticle;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;
    private Quaternion defualtPlayerRot;

    [SerializeField] private GameObject Gun;
    private Quaternion defualtGunRot;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform barrelTransform;
    [SerializeField] private Transform bulletParent;
    [SerializeField] private float boyah = 25f;

    //[SerializeField] private float rotationSpeed = 5f;

    //[SerializeField] private LayerMask aimCollider = new LayerMask();

    Vector3 mouseWorldPosition = Vector3.zero;

    private bool detectMouse;

    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssetsInput;

    void Awake()
    {
        //Retrieves components from other scripts at start-up
        starterAssetsInput = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
    }
    void Start()
    {
        // Sets camera sensitivity to defualt
        thirdPersonController.camSensitivity = normalSensitivity;
        defualtPlayerRot = transform.rotation;
        defualtGunRot = Gun.transform.rotation;
    }

    private void GetMousePos()
    {
        // Fires a raycast to and keeps track of mouse position -- May crash if no mouse is used
        
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            mouseWorldPosition = raycastHit.point;
            detectMouse = true;
            
        }
        else
        {
            detectMouse = false;
        }
    }

    private void ShootGun()
    {
        // spawns bullet prefab from gun barrel
        GameObject bullet = GameObject.Instantiate(bulletPrefab, barrelTransform.position, Quaternion.identity, bulletParent);
        BulletController bulletController = bullet.GetComponent<BulletController>();
        
        if (detectMouse)
        {               
            bulletController.target = mouseWorldPosition;
            bulletController.hit = true;
        }
        else
        {
            bulletController.target = transform.position + transform.forward * boyah;
            bulletController.hit = true;
        }

        thirdPersonController.SetRotateWhileMove(false);

        // Forces player to face toward mouse cursor
        Vector3 worldAimTarget = mouseWorldPosition;
        worldAimTarget.y = transform.position.y;
        Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

        transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 1000f);
    }

    // Update is called once per frame
    void Update()
    {
        GetMousePos();
        // When aim (right-click) is selected...
        if (starterAssetsInput.aim)
        {            
            // Toggles aim camera on and uses the 'ChangeSensitivity' to change camera sensitivity to aim camera
            aimVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.ChangeSensitivity(aimSensitivity);
            // Stops player object from facing the way its moving
            thirdPersonController.SetRotateWhileMove(false);

            // Forces player object to always face toward mouse cursor.
            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        }
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.ChangeSensitivity(normalSensitivity);
            thirdPersonController.SetRotateWhileMove(true);
        }

        if (starterAssetsInput.shoot)
        {
            ShootGun();            

            starterAssetsInput.shoot = false;
        }
        
        /*Vector3 gunWorldAimTarget = mouseWorldPosition;
        gunWorldAimTarget.y = Gun.transform.position.y;
        Vector3 gunAimDirection = (gunWorldAimTarget - Gun.transform.position).normalized;

        Gun.transform.forward = Vector3.Lerp(Gun.transform.forward, gunAimDirection, Time.deltaTime * 20f);*/

    }
    }
