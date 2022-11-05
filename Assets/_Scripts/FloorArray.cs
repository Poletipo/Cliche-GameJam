using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FloorArray : MonoBehaviour
{

    //public Vector2 roomSize = new Vector2(1, 1);
    public GameObject Tile;
    public GameObject Walls;

    public Vector2Int RoomSize;
    private Vector2Int _roomSizeTemp;


    private int _tileSizeTemp = 1;
    public int TileSize = 1;




    // Start is called before the first frame update
    void Start()
    {
        _roomSizeTemp = RoomSize;
    }

    // Update is called once per frame
    void Update()
    {

        if (_roomSizeTemp == RoomSize && _tileSizeTemp == TileSize) {
            return;
        }

        if (RoomSize.x <= 1) {
            RoomSize.x = 1;
        }
        if (RoomSize.y <= 1) {
            RoomSize.y = 1;
        }

        List<GameObject> toDestroy = new List<GameObject>();

        for (int i = 0; i < transform.childCount; i++) {
            toDestroy.Add(transform.GetChild(i).gameObject);
        }

        float xMin = -((RoomSize.x * TileSize * .5f) - (TileSize *.5f));
        float yMin = -((RoomSize.y * TileSize * .5f) - (TileSize * .5f));


        for (int i = -1; i < RoomSize.y+1; i++) {

            for (int j = -1; j < RoomSize.x+1; j++) {

                Vector3 pos = new Vector3(xMin + ((float)j * (TileSize)) , 0, yMin + ((float)i * TileSize));

                GameObject obj;
                if (i==-1 || i == RoomSize.y || j == -1 || j == RoomSize.x)
                {
                    obj = Instantiate(Walls, pos, Quaternion.identity);
                }
                else
                {
                    obj = Instantiate(Tile, pos,Quaternion.identity);

                }
                obj.transform.localScale = new Vector3(TileSize, 1, TileSize);
                obj.transform.parent = transform;
            }

        }

        foreach (GameObject item in toDestroy) {
            DestroyImmediate(item);
        }


        _roomSizeTemp = RoomSize;
        _tileSizeTemp = TileSize;
        Debug.Log("RoomUpdated");
    }


    private void UpdateRoom()
    {
       
    }

}
