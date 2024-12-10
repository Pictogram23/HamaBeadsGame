using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public GameObject objBead;
    public float RandomTimeRangeA = 0;
    public float RandomTimeRangeB = 0;
    public float BeadXRangeA = 0;
    public float BeadXRangeB = 0;
    public float BeadY = 0;
    public int CountBeadColor = 0;
    float time = 0;
    float limit = 0;

    public string strGameScene1;
    
    // Start is called before the first frame update
    void Start()
    {
        limit = Random.Range(RandomTimeRangeA, RandomTimeRangeB);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time >= limit)
        {
            HamaBeadCreate();
            limit = Random.Range(RandomTimeRangeA, RandomTimeRangeB);
            time = 0;
        }
    }

    void HamaBeadCreate()
    {
        Vector3 pos = new Vector3();
        pos.x = Random.Range(BeadXRangeA, BeadXRangeB);
        pos.y = BeadY;
        pos.z = 0;
        Quaternion quaternion = Quaternion.Euler(0, 0, Random.Range(-180, 180));
        GameObject Created = GameObject.Instantiate(objBead, pos, quaternion);
        HamaBeads CreatedScript = Created.GetComponent<HamaBeads>();
        CreatedScript.mode = HamaBeads.Mode.Decoration;
        CreatedScript.color = (int)Random.Range(1f, (float)CountBeadColor);
    }

    public void GameStart()
    {
        SceneManager.LoadScene(strGameScene1);
    }
    
}
