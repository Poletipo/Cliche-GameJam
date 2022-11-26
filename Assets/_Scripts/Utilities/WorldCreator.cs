using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class WorldCreator : MonoBehaviour
{
    public Texture2D map;

    public GameObject[] WorldObjects;
    public Color[] MapColors;

    public GameObject floor;

    Color[] pixelColors;

    // Start is called before the first frame update
    public void Generate()
    {

        List<GameObject> toDestroy = new List<GameObject>();

        for (int i = 0; i < transform.childCount; i++)
        {
            toDestroy.Add(transform.GetChild(i).gameObject);
        }

        pixelColors = map.GetPixels();

        int width = map.width;
        int height = map.height;

        GameObject quad = Instantiate(floor, new Vector3(width / 2, 0, height / 2), Quaternion.identity);
        quad.transform.localScale = Vector3.one * (width+1);
        quad.transform.parent = transform;

        for (int i = 0; i < pixelColors.Length; i++)
        {
            Color color = pixelColors[i];

            for (int j = 0; j < MapColors.Length; j++)
            {
                //Debug.Log(color + " : " + MapColors[j]);
                if (color == MapColors[j])
                {

                    int x = i % width;
                    int z = Mathf.FloorToInt(i / width);

                    Vector3 pos = new Vector3(x, 0, z);
                    GameObject gmobj = Instantiate(WorldObjects[j], pos, Quaternion.identity);
                    gmobj.transform.parent = transform;

                    LockedDoor door = gmobj.GetComponentInChildren<LockedDoor>();
                    if (door != null && pixelColors[i+1] == Color.white)
                    {
                        door.Orientation(90);
                    }
                    break;
                }
            }
        }



        foreach (GameObject item in toDestroy)
        {
            DestroyImmediate(item);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
