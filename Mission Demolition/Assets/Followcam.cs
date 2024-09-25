using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Followcam : MonoBehaviour
{
    static public GameObject POI; // point of interest 

    [Header("Inscriped")]
    public float easing = 0.05f;

    [Header("Dynamic")]
    public float camZ;

    void Awake()
    {
        camZ = this.transform.position.z;    
    }

    private void FixedUpdate()
    {
        if (POI == null) { return; }
        Vector3 destination = POI.transform.position;
        destination = Vector3.Lerp(transform.position, destination, easing);
        destination.z = camZ;
        //set the camera's postion
        transform.position = destination;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
