using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamaBeads : MonoBehaviour
{
    enum State
    {
        Wait,
        Catched,
        Released,
    }

    public bool isSensor = false;
    public string nameEntity;
    public string nameGuide;
    public float posGuideY;
    public Color32[] pallet = new Color32[8];
    public int color;
    /// <summary>
    /// 0: White
    /// 1: Red
    /// 2: Blue
    /// 3: Green
    /// 4: Yellow
    /// 5: Gray
    /// 6: Clear
    /// 7: Black
    /// </summary>

    State state;

    GameObject entity;
    GameObject guide;

    // Start is called before the first frame update
    void Start()
    {
        entity = transform.Find(nameEntity).gameObject;
        guide = transform.Find(nameGuide).gameObject;

        state = State.Wait;

        Renderer entityRenderer = entity.GetComponent<Renderer>();
        Renderer guideRenderer = guide.GetComponent<Renderer>();
        Color32 tempGuideColor;
        switch (color)
        {
            case 0:
                entityRenderer.material.color = pallet[0];
                tempGuideColor = pallet[0];
                tempGuideColor.a = 127;
                guideRenderer.material.color = tempGuideColor;
                break;
            case 1:
                entityRenderer.material.color = pallet[1];
                tempGuideColor = pallet[1];
                tempGuideColor.a = 127;
                guideRenderer.material.color = tempGuideColor;
                break;
            case 2:
                entityRenderer.material.color = pallet[2];
                tempGuideColor = pallet[2];
                tempGuideColor.a = 127;
                guideRenderer.material.color = tempGuideColor;
                break;
            case 3:
                entityRenderer.material.color = pallet[3];
                tempGuideColor = pallet[3];
                tempGuideColor.a = 127;
                guideRenderer.material.color = tempGuideColor;
                break;
            case 4:
                entityRenderer.material.color = pallet[4];
                tempGuideColor = pallet[4];
                tempGuideColor.a = 127;
                guideRenderer.material.color = tempGuideColor;
                break;
            case 5:
                entityRenderer.material.color = pallet[5];
                tempGuideColor = pallet[5];
                tempGuideColor.a = 127;
                guideRenderer.material.color = tempGuideColor;
                break;
            case 6:
                entityRenderer.material.color = pallet[6];
                tempGuideColor = pallet[6];
                tempGuideColor.a = 127;
                guideRenderer.material.color = tempGuideColor;
                break;
            case 7:
                entityRenderer.material.color = pallet[7];
                tempGuideColor = pallet[7];
                tempGuideColor.a = 127;
                guideRenderer.material.color = tempGuideColor;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isSensor == false)
        {
            Vector3 posGuide = entity.transform.position;
            posGuide.y = posGuideY;

            switch (state)
            {
                case State.Catched:
                    entity.GetComponent<Rigidbody>().isKinematic = true;
                    entity.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                    Vector3 mousePos = Input.mousePosition;
                    Vector3 destination = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 5.8f));
                    entity.transform.position = destination;

                    guide.SetActive(true);
                    guide.transform.position = posGuide;
                    guide.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    break;
                case State.Released:
                    entity.GetComponent<Rigidbody>().isKinematic = false;
                    guide.SetActive(false);
                    break;
            }
        }
        else
        {
            Vector3 myPos = entity.transform.position;
            Ray ray = new Ray(myPos, new Vector3(myPos.x, 8, 0));
            RaycastHit hit = new RaycastHit();
            int distance = 10;
            Debug.DrawLine(ray.origin, ray.direction * distance, Color.red);
            if(Physics.Raycast(ray, out hit, distance))
            {
                if(hit.collider.gameObject.GetComponentInParent<HamaBeads>().color == color)
                {
                    Debug.Log("OK!");
                }
            }
        }
    }

    public void onCatched()
    {
        state = State.Catched;
    }

    public void onReleased()
    {
        state = State.Released;
    }
}
