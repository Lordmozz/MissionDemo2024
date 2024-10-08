using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    [Header("Inscribed")]
    public GameObject projectilePrefab;
    public float velocityMult = 10f;
    public GameObject projlinePrefab;

    [Header("Dynamic")]
    public GameObject launchPoint;
    public Vector3 launchPos;
    public GameObject projectile;
    public bool aimingMode;

    private void Awake()
    {
        Transform launchPointTrans = transform.Find("LaunchPoint");
        launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive(false);
        launchPos = launchPointTrans.position;
    }

    void OnMouseEnter()
    {
       // print("SlingShot:OnMouseEnter()");
        launchPoint.SetActive(true);
    }


     void OnMouseExit() {
        //print("SlingShot:OnMouseExit()");
        launchPoint.SetActive(false);
    }

     void OnMouseDown()
    {
        aimingMode = true;
        projectile = Instantiate(projectilePrefab) as GameObject;
        projectile.transform.position = launchPos;
        projectile.GetComponent<Rigidbody>().isKinematic = true;

    }

     void Update()
    {
       if (!aimingMode) { return; }
       
       //get the current mouse postion
       Vector3 mousePos2d = Input.mousePosition;
        mousePos2d.z = -Camera.main.transform.position.z;
        Vector3 mousepPos3d = Camera.main.ScreenToWorldPoint(mousePos2d);

        //find the delta from the launch pos
        Vector3 mouseDelta = mousepPos3d - launchPos;
        //limit mousedelta to the radius
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;

        if (mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }
        //move our projectile to this new postion
        Vector3 projPos = launchPos + mouseDelta;
        projectile.transform.position = projPos;

        if (Input.GetMouseButtonUp(0))
        {
            aimingMode = false;
            Rigidbody projRb = projectile.GetComponent<Rigidbody>();
            projRb.isKinematic = false;
            projRb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            projRb.velocity = -mouseDelta * velocityMult;
            Followcam.POI = projectile;
            Instantiate<GameObject>(projlinePrefab, projectile.transform);
            projectile = null;
        }
    }

}
