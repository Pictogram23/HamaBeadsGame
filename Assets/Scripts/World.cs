using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    enum State
    {
        Prepare,
        Play,
    }

    public GameObject objBead;
    public int countBeads;
    public int countColor;
    public float span;
    public string tagNameBeads;
    public Vector2[] posBeads;

    float timer;
    int counter;
    State state;
    Ray ray;
    RaycastHit hit;
    bool isCatched = false;

    // Start is called before the first frame update
    void Start()
    {
        state = State.Prepare;
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case State.Prepare:
            timer += Time.deltaTime;
            if (counter >= countBeads)
            {
                state = State.Play;
            }
            else
            {
                if (timer >= span)
                {
                    timer = 0;
                    for(int i = 0; i < countColor; i++)
                    {
                        Vector3 pos = new Vector3(posBeads[i].x, 2f, posBeads[i].y);
                        GameObject gameObject = Instantiate(objBead, pos, Random.rotation) as GameObject;
                        HamaBeads hamaBeads = gameObject.GetComponent<HamaBeads>();
                        hamaBeads.color = i;
                    }
                    counter++;
                }
            }
                break;
            case State.Play:
                if(Input.GetMouseButtonDown(0))
                {
                    ray = new Ray();
                    hit = new RaycastHit();
                    ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                    if(Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
                    {
                        if(hit.collider.gameObject.CompareTag(tagNameBeads))
                        {
                            if (isCatched == false)
                            {
                                hit.collider.gameObject.transform.parent.gameObject.GetComponent<HamaBeads>().onCatched();
                            }
                            else
                            {
                                hit.collider.gameObject.transform.parent.gameObject.GetComponent<HamaBeads>().onReleased();
                            }
                            isCatched = !isCatched;
                        }
                    }
                }
                break;
        }
    }
}
