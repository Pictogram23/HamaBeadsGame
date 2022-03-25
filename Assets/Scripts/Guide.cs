using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide : MonoBehaviour
{
    public GameObject objBead;
    public GameObject objPlate;
    public TextAsset textAsset;
    public string nameBeadEntity;
    public string nameTagSensor = "BeadsSensor";
    public bool flagClear;
    public float BeadScale = 1f;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 newBeadScale = new Vector3();

        string textLines = textAsset.text;
        string[] textLine = textLines.Split('\n');

        int GuideMapRow = textLine[0].Split('\t').Length;
        int GuideMapColumn = textLine.Length;

        int[,] GuideMap = new int[GuideMapRow, GuideMapColumn];

        for (int y = 0; y < GuideMapColumn; y++)
        {
            string[] value = textLine[y].Split('\t');
            for (int x = 0; x < GuideMapRow; x++)
            {
                GuideMap[x, y] = int.Parse(value[x]);
            }
        }

        for (int y = 0; y < GuideMapColumn; y++)
        {
            for (int x = 0; x < GuideMapRow; x++)
            {
                if (GuideMap[x, y] > 0)
                {
                    Vector3 scale = objBead.transform.GetChild(0).gameObject.GetComponent<Renderer>().bounds.size;
                    float alignZ = (GuideMapRow - 1) * scale.z * BeadScale / 2;
                    float alignX = (GuideMapColumn - 1) * scale.x * BeadScale / 2;
                    Vector3 pos = transform.position;
                    pos.z = pos.z - alignZ + scale.z * BeadScale * x;
                    pos.x = pos.x - alignX + scale.x * BeadScale * y;
                    GameObject gameObject = GameObject.Instantiate(objBead, pos, Quaternion.identity) as GameObject;
                    newBeadScale = gameObject.transform.localScale;
                    newBeadScale *= BeadScale;
                    gameObject.transform.localScale = newBeadScale;
                    gameObject.transform.parent = transform;
                    gameObject.transform.GetChild(0).GetComponent<Rigidbody>().useGravity = false;
                    HamaBeads hamaBeads = gameObject.GetComponent<HamaBeads>();
                    hamaBeads.color = GuideMap[x, y] - 1;
                    hamaBeads.isSensor = true;
                    hamaBeads.tag = nameTagSensor;
                    hamaBeads.BeadScale = BeadScale;
                }
            }
        }
        Vector3 newPlatePos = objPlate.transform.position;
        newPlatePos.y *= BeadScale;
        objPlate.transform.position = newPlatePos;
    }

    // Update is called once per frame
    void Update()
    {
        flagClear = true;

        GameObject[] BeadsSensors = GameObject.FindGameObjectsWithTag(nameTagSensor);
        for (int i = 0; i < BeadsSensors.Length; i++)
        {
            if (BeadsSensors[i].GetComponent<HamaBeads>().hasSensorDetected == false)
            {
                flagClear = false;
            }
        }

        if (flagClear == true)
        {
            //Debug.Log("ÉNÉäÉA!");
        }
    }
}
