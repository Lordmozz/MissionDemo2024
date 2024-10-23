using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameMode
{
    idle,
    playing,
    levelEnd
}

public class MissionDemolation : MonoBehaviour
{
    static private MissionDemolation s; // a private singleton


    [Header("Inscribed")]
    public TMPro.TMP_Text utiLevel;
    public TMPro.TMP_Text uitShots;
    public Vector3 castlePos;
    public GameObject[] castles;

    [Header("Dynamic")]
    public int level;
    public int levelMax;
    public int shotsTaken;
    public GameObject castle;
    public GameMode mode = GameMode.idle;
    public string showing = "Show Slingshot"; //FollowCam
    // Start is called before the first frame update
    void Start()
    {
        s = this;
        level = 0;
        shotsTaken = 0;
        levelMax = castles.Length;
        StartLevel();
    }

    void StartLevel()
    {
        if (castle != null) { Destroy(castle); }
            Projectile.DESTORY_PROJECTILES();
            castle = Instantiate<GameObject>(castles[level]);
            castle.transform.position = castlePos;
            Goal.goalMet = false;
            UpdateGui();
            mode = GameMode.playing;
             Followcam.Switch_view(Followcam.eView.both);
    }

    void UpdateGui()
    {
        utiLevel.text = "Level: " + (level + 1) + " of " + levelMax;
        uitShots.text = "Shots Taken:" + shotsTaken;
    }
    // Update is called once per frame
    void Update()
    {
        UpdateGui(); 
        if ((mode == GameMode.playing) && Goal.goalMet){
            mode = GameMode.levelEnd;
            Invoke("NextLevel", 2f);
            Followcam.Switch_view(Followcam.eView.both);
        }
    }

    void NextLevel()
    {
        level++;
        if(level == levelMax) 
            {
                level= 0;
            shotsTaken = 0;
            }
        StartLevel();
    }
    static public void SHOT_FIRED()
    {
        s.shotsTaken++;
    }

    static public GameObject GET_CASTLE()
    {
        return s.castle;
    }
}
