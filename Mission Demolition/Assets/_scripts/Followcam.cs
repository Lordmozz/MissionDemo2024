using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Followcam : MonoBehaviour
{
    static private Followcam S;
    static public GameObject POI; // point of interest 

    public enum eView { none, slingshot ,castle, both };

    [Header("Inscriped")]
    public float easing = 0.05f;
    public Vector2 minXY = Vector3.zero;
    public GameObject viewBothGo;

    [Header("Dynamic")]
    public float camZ;
    public eView nextView = eView.slingshot;

    void Awake()
    {
        camZ = this.transform.position.z;
        S = this;
    }

    private void FixedUpdate()
    {
        Vector3 destination = Vector3.zero;
        if (POI != null)
        {
            //if the POI has a rigidbody, check to see if it's sleeping
            Rigidbody poiRigid = POI.GetComponent<Rigidbody>();
            if ((poiRigid != null) && poiRigid.IsSleeping())
            {
                POI = null;
            }
        }

        if (POI != null)
        {
            destination = POI.transform.position;
        }

        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);
        destination = Vector3.Lerp(transform.position, destination, easing);
        destination.z = camZ;
        //set the camera's postion
        transform.position = destination;
        Camera.main.orthographicSize = destination.y + 10;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SwitchView(eView newView)
    {
        if (newView == eView.none)
        {
            newView = nextView;
        }
        switch (newView) {
            case eView.slingshot:
                POI = null;
                nextView = eView.castle;
                break;
            case eView.castle:
                POI = MissionDemolation.GET_CASTLE();
                nextView = eView.both;
                break;
            case eView.both:
                POI = viewBothGo;
                nextView = eView.slingshot;
                break;
        }
    } 
    public void SwitchView()
    {
        SwitchView(eView.none);
    }
    static public void Switch_view(eView newView) {
        S.SwitchView(newView);
    }

}
