using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide : MonoBehaviour
{
    public GameObject objBead;
    public TextAsset textAsset;
    public string nameBeadEntity;

    // Start is called before the first frame update
    void Start()
    {
        string textLines = textAsset.text;
        string[] textLine = textLines.Split('\n');

        int GuideMapRow = textLine[0].Split('\t').Length;
        int GuideMapColumn = textLine.Length;

        int[,] GuideMap = new int[GuideMapRow, GuideMapColumn];

        for (int i = 0; i < GuideMapColumn; i++)
        {
            string[] value = textLine[i].Split('\t');
            for (int j = 0; j < GuideMapRow; j++)
            {
                GuideMap[i, j] = int.Parse(value[j]);
            }
        }

        for (int y = 0; y < GuideMapColumn; y++)
        {
            for (int x = 0; x < GuideMapRow; x++)
            {
                if (GuideMap[x, y] > 0)
                {
                    Vector3 pos = transform.position;
                    Vector3 scale = objBead.transform.GetChild(0).gameObject.GetComponent<Renderer>().bounds.size;
                    pos.x += scale.x * x;
                    pos.z += scale.z * y;
                    GameObject gameObject = GameObject.Instantiate(objBead, pos, Quaternion.identity) as GameObject;
                    gameObject.transform.parent = transform;
                    gameObject.transform.GetChild(0).GetComponent<Rigidbody>().useGravity = false;
                    HamaBeads hamaBeads = gameObject.GetComponent<HamaBeads>();
                    hamaBeads.color = GuideMap[x, y] - 1;
                    hamaBeads.isSensor = true;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
