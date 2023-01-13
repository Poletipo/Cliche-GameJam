using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class WorldCreator : MonoBehaviour {
    public Texture2D map;

    public GameObject[] WorldObjects;
    public GameObject floor;
    public Color[] MapColors;
    public int nextLevel = 0;

    private Color[] _pixelColors;
    private Color _currenColor;

    // Start is called before the first frame update
    public void Generate() {

        List<GameObject> toDestroy = new List<GameObject>();

        for (int i = 0; i < transform.childCount; i++) {
            toDestroy.Add(transform.GetChild(i).gameObject);
        }

        _pixelColors = map.GetPixels();

        int width = map.width;
        int height = map.height;

        GameObject quad = Instantiate(floor, new Vector3(width / 2, 0, height / 2), Quaternion.identity);
        quad.transform.localScale = Vector3.one * (width * 2);
        quad.transform.parent = transform;

        for (int i = 0; i < _pixelColors.Length; i++) {
            _currenColor = _pixelColors[i];

            for (int j = 0; j < MapColors.Length; j++) {
                if (_currenColor == MapColors[j]) {

                    int x = i % width;
                    int z = Mathf.FloorToInt(i / width);

                    Vector3 pos = new Vector3(x, 0, z);
                    GameObject gmobj = Instantiate(WorldObjects[j], pos, Quaternion.identity);
                    gmobj.transform.parent = transform;

                    CheckSpecialCase(gmobj, i);

                    break;
                }
            }
        }

        foreach (GameObject item in toDestroy) {
            DestroyImmediate(item);
        }

    }

    private void CheckSpecialCase(GameObject gameObject, int index) {
        LockedDoor door = gameObject.GetComponentInChildren<LockedDoor>();
        if (door != null && _pixelColors[index + 1] == Color.white) {
            door.Orientation(90);
        }
        LevelEnd levelEnd = gameObject.GetComponent<LevelEnd>();
        if (levelEnd != null) {
            levelEnd.NextLevelIndex = nextLevel;
        }
    }

}
