using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private const float PlayerDistanceSpawnLevelPart = 50f;
    [SerializeField] public Transform LevelPartStart;
    [SerializeField] public  List<Transform> GrassLevelPartList;
    [SerializeField] public List<Transform> DesertLevelPartList;
    [SerializeField] public List<Transform> SnowLevelPartList;
    public int Seed = 0;
    public bool randomSeed = false;
    public GameObject player;
    private Vector3 lastEndPosition;
    private int number;
    //float num;


    private void Start()
    {
        //num = GameObject.Find("SNinja_Atk 1").GetComponent<PlayerMovement>().number;
        
        //number = number - num;
        //Debug.Log("number in start: " + number);
    }

    /*void SaveList()
    {
        int snow;
        for (var i : float = 0; i < SnowLevelPartList.length; i++){

            snow = SnowLevelPartList[i];

            PlayerPrefs.SetFloat(SnowLevelPartList[i], snow);
            //PlayerPrefs.SetFloat("SnowLevelPartList", SnowLevelPartList);
        }
    }*/

    void Awake()
    {
        
        lastEndPosition = LevelPartStart.Find("EndPosition").position;
        




        Seed = PlayerPrefs.GetInt("seed", randomSeed ? Random.Range(0, 9999) : Seed);
            PlayerPrefs.SetInt("seed", Seed);
        
        Random.InitState(Seed);
        int StartingSpawnLevelParts = 1;
        for (int i = 0; i < StartingSpawnLevelParts; i++)
        {
            GreenSpawnLevelPart();
        }
        
    }

    void Update()
    {
        number = Mathf.RoundToInt(player.transform.position.x);
        number %= 1000;
       
        //Debug.Log("number value: " + number);
        while (lastEndPosition.x - player.transform.position.x < PlayerDistanceSpawnLevelPart)
        {
            if ( number < 300f)
            {
                //spawn another level part
                GreenSpawnLevelPart();
            }
            else if ( number > 300f && number < 700f)
            {
                YelowSpawnLevelPart();
            }
            else if ( number > 700f && number < 1000f)
            {
                BlueSpawnLevelPart();
            }
        }

        //Debug.Log("number %= " + number + "PlayerDistanceSpawnLevelPart: "+ PlayerDistanceSpawnLevelPart + "lastEndPosition.x: ");
    }

    private void GreenSpawnLevelPart()
    {
        Transform chosenLevelPart = GrassLevelPartList[Random.Range(0, GrassLevelPartList.Count)];
        Transform lastLevelPartTransform = SpawnLevelPart(chosenLevelPart,lastEndPosition);
        lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
    }
    private void YelowSpawnLevelPart()
    {
        Transform chosenLevelPart = DesertLevelPartList[Random.Range(0, DesertLevelPartList.Count)];
        Transform lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, lastEndPosition);
        lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
    }
    private void BlueSpawnLevelPart()
    {
        Transform chosenLevelPart = SnowLevelPartList[Random.Range(0, SnowLevelPartList.Count)];
        Transform lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, lastEndPosition);
        lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
    }
    private Transform SpawnLevelPart(Transform levelPart,Vector3 spawnPossition)
    {
        Transform levelPartTransform = Instantiate(levelPart, spawnPossition, Quaternion.identity);
        return levelPartTransform;
    }

}
